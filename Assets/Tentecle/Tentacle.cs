using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tentacle : MonoBehaviour
{
    public float speed;
    private Vector3 defaultPosition;
    private Vector2 touchCoor;
    private bool touch;
    private bool isMoving;
    private Transform touchLimit;
    private Transform endTrajectory;
    private Transform cam;
    private LineRenderer line;

    private float LeftBorder, RightBorder;
    private float xWall;

    private void Awake()
    {
        defaultPosition = transform.position;
        cam = Camera.main.transform;
        touchLimit = transform.Find("TouchLimit");
        endTrajectory = cam.Find("EndTrajectory");
        line = GetComponentInChildren<LineRenderer>();
        LeftBorder = cam.Find("LeftBorder").position.x;
        RightBorder = cam.Find("RightBorder").position.x;
    }

    public void Touch(Vector2 touchCoor)
    {
        this.touchCoor = touchCoor;
        if (!touch)
            touch = true;
    }

    public void Update()
    {
        SetTrajectory();
    }

    public void SetTrajectory()
    {
        if (!touch || isMoving)
            return;
        RaycastHit2D hit;
        if (touchCoor.y < touchLimit.position.y)
        {
            touchCoor = new Vector2(touchCoor.x, touchLimit.position.y);
        }
        List<Vector3> linePoses = new List<Vector3>();
        linePoses.Add(transform.position);
        float tan = (Mathf.Abs(touchCoor.y - transform.position.y) / Mathf.Abs(touchCoor.x - transform.position.x));
        if (touchCoor.x > transform.position.x)
            xWall = RightBorder;
        else
            xWall = LeftBorder;
        for (int i = 0; i < 10; i++)
        {
            Vector3 nextPoint = new Vector3(xWall, Mathf.Abs(xWall - linePoses[linePoses.Count - 1].x) * tan + linePoses[linePoses.Count - 1].y, 0f);
            hit = Physics2D.Raycast(linePoses[linePoses.Count - 1], nextPoint - linePoses[linePoses.Count - 1], (nextPoint - linePoses[linePoses.Count - 1]).magnitude);
            if (hit.collider != null)
            {
                linePoses.Add(hit.point);
                break;
            }
            linePoses.Add(nextPoint);
            if (nextPoint.y > endTrajectory.position.y)
                break;
            xWall = xWall == RightBorder ? LeftBorder : RightBorder;
            
        }
        line.positionCount = linePoses.Count;
        line.SetPositions(linePoses.ToArray());
    }

    public IEnumerator Move()
    {
        touch = false;
        isMoving = true;
        Vector3[] posesArr = new Vector3[line.positionCount];
        line.GetPositions(posesArr);
        List<Vector3> poses = new List<Vector3>(posesArr);
        if (line.positionCount == 0)
        {
            Debug.LogError("Траектория не задана");
            yield break;
        }
        while (true)
        {
            if (transform.position == line.GetPosition(1))
            {
                poses.RemoveAt(0);;
                line.positionCount = poses.Count;
                line.SetPositions(poses.ToArray());
                if (poses.Count == 1)
                {
                    transform.position = defaultPosition;
                    line.positionCount = 0;
                    break;
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, line.GetPosition(1), speed * Time.deltaTime);
                line.SetPosition(0, transform.position);
            }
            yield return null;
        }
        isMoving = false;
    }
}
