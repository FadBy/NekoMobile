using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.UIElements;

public class BackGround : MonoBehaviour
{
    public float viewZone;
    private Transform camTr;
    private Transform[] queue;
    private float heightBG;
    private float widthBG;
    private int upper;
    private int lower;

    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
        camTr = cam.transform;
        queue = new Transform[transform.childCount];
        heightBG = cam.orthographicSize * 2f;
        widthBG = heightBG * Screen.width / Screen.height;
        for (int i = 0; i < queue.Length; i++)
        {
            queue[i] = transform.GetChild(i);
            queue[i].localScale = new Vector3(widthBG, heightBG, 0f);
            queue[i].position = camTr.position + new Vector3(0f, heightBG * i, queue[i].parent.position.z);
        }
        upper = queue.Length - 1;
        lower = 0;
    }

    private void Update()
    {
        if (camTr.position.y > queue[upper].position.y - viewZone)
        {
            queue[lower].position += new Vector3(0f, 3 * heightBG, 0f);
            upper = lower;
            lower++;
            if (lower > queue.Length - 1)
                lower = 0;
        }
        else if (camTr.position.y < queue[lower].position.y + viewZone)
        {
            queue[upper].position -= new Vector3(0f, 3 * heightBG, 0f);
            lower = upper;
            upper--;
            if (upper < 0)
                upper = queue.Length - 1;
        }
    }


}
