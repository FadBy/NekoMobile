using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float offset;
    public float speed;
    private Transform target;
    

    private void Start()
    {
        target = player.transform;
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, target.position.y + offset, speed * Time.deltaTime), transform.position.z);
    }
}
