using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TentacleController : MonoBehaviour
{
    public Tentacle tentacle;

    private SpriteRenderer spr;
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
        spr = tentacle.GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Began)
            {
                tentacle.Touch(cam.ScreenToWorldPoint(touch.position));
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                StartCoroutine(tentacle.Move());
            }
        }
    }
}
