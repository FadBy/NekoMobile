using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : MonoBehaviour
{
    public float speed;

    private bool isCarring = false;

    private int direction = 1;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private int typeMoving = 0;
    private Vector3 startLeft, startRight;
    private Vector3 endLeft, endRight;

    private Rigidbody2D rb;
    private SpriteRenderer spr;
    private Camera cam;
    private Pooler pooler;
    private Storage storage;

    private GameObject carringItem = null;

    private Transform takingZone;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        cam = Camera.main;
        storage = Storage.Instance;
        pooler = Pooler.Instance;
        takingZone = transform.Find("TakingZone");
        startLeft = cam.transform.Find("TentacleLeftStart").position;
        startRight = cam.transform.Find("TentacleRightStart").position;
        endLeft = cam.transform.Find("TentacleLeftEnd").position;
        endRight = cam.transform.Find("TentacleRightEnd").position;
    }

    public void StartMove(float height, int direction)
    {
        if (typeMoving != 0)
            return;
        startPosition = new Vector3(direction > 0 ? startLeft.x : startRight.x, height);
        endPosition = new Vector3(direction > 0 ? endLeft.x : endRight.x, height);
        transform.position = startPosition;
        if (direction != this.direction)
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y);
        this.direction = direction;
        StartCoroutine(Movement());
    }

    private IEnumerator Movement()
    {
        typeMoving = 1;
        while (direction * transform.position.x < direction * endPosition.x && !isCarring)
        {
            transform.Translate(speed * direction * Vector3.right * Time.deltaTime);
            yield return null;
        }
        typeMoving = -1;
        while (direction * transform.position.x > direction * startPosition.x)
        {
            transform.Translate(speed * direction * Vector3.left * Time.deltaTime);
            yield return null;
        }
        typeMoving = 0;
        isCarring = false;
        if (carringItem != null)
        {
            carringItem.transform.SetParent(null);
            carringItem.SetActive(false);
            carringItem = null;
        }
    }

    private void TakeItem(GameObject item)
    {
        item.SetActive(false);
        GameObject gag = storage.patternComponents[item.transform.parent.gameObject].children[item].gag;
        carringItem = pooler.SpawnFromPull(gag, takingZone.position, transform);
        isCarring = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy" || other.tag == "Friend")
        {
            if (!isCarring && typeMoving == 1)
                TakeItem(other.gameObject);
        }
    }
}
