using UnityEngine;

public class HeartController : MonoBehaviour
{
    public GameObject pointPrefab;
    public float koef;
    public int countPoints;
    public float distanceAfterDrop;
    public float time;

    private GameObject[] points;
    private Vector2 startTouch;
    private Vector2 startTrajectory;
    private Vector2 endTrajectory;
    private float a;
    private float b;
    private Vector3 lastPos;
    private float currentDistance;
    private float fixedDeltaTime;
    private Vector2 change;

    private float angle;
    private float force;

    private Camera cam;
    private Rigidbody2D rb;
    private Pooler pooler;

    private void Awake()
    {
        pooler = Pooler.Instance;
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        lastPos = transform.position;
        fixedDeltaTime = Time.fixedDeltaTime;
    }

    private void Start()
    {
        points = new GameObject[countPoints];
        for (int i = 0; i < countPoints; i++)
        {
            points[i] = Instantiate(pointPrefab, transform);
            points[i].SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            SetTrajectory(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Move();
        }
        if (currentDistance >= distanceAfterDrop)
        {
            DropHeart();
            currentDistance = 0;
        }
        else
        {
            currentDistance += Vector2.Distance(transform.position, lastPos);
        }
        lastPos = transform.position;
    }

    private void TokioTomore(float value)
    {
        Time.timeScale = value;
        Time.fixedDeltaTime = value * fixedDeltaTime;
    }

    private void DropHeart()
    {
        pooler.SpawnFromPull(pointPrefab, transform.position);
    }

    private void Move()
    {
        TokioTomore(1);
        rb.gravityScale = 1;
        if (angle < 0)
            force = -force;
        rb.velocity = Vector2.zero;
        rb.AddForce(force * new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)), ForceMode2D.Impulse);
        ClearTrajectory();
    }

    private void ClearTrajectory()
    {
        for (int i = 0; i < points.Length; i++)
        {
            points[i].SetActive(false);
        }
    }

    private void SetTrajectory(Vector2 touchCoor)
    {
        change = koef * (cam.ScreenToWorldPoint(touchCoor) - cam.ScreenToWorldPoint(startTouch));
        startTrajectory = transform.position;
        endTrajectory = startTrajectory + change;
        if (startTrajectory == endTrajectory)
            return;
        TokioTomore(time);
        CalculateParameters(touchCoor);
        for (int i = 0; i < countPoints; i++)
        {
            float y = (startTrajectory.y * (countPoints - i - 1) + endTrajectory.y * (i + 1)) / countPoints;
            Vector2 coor;
            if (-4 * a * (endTrajectory.y - y) < 0 || a == 0)
                return;
            if (endTrajectory.x > startTrajectory.x)
            {
                coor = new Vector2((-b + Mathf.Sqrt(-4 * a * (endTrajectory.y - y))) / (2 * a), y);
            }
            else
            {
                coor = new Vector2((-b - Mathf.Sqrt(-4 * a * (endTrajectory.y - y))) / (2 * a), y);
            }
            points[i].transform.position = coor;
            points[i].SetActive(true);
        }

    }

    private void CalculateParameters(Vector2 touchCoor)
    {
        angle = Mathf.Atan(2 * change.y / change.x);
        force = Mathf.Sqrt(2 * change.x * -Physics2D.gravity.y / Mathf.Sin(2 * angle)) * rb.mass;
        a = Mathf.Tan(angle) / (2 * startTrajectory.x - 2 * endTrajectory.x);
        b = -2 * a * endTrajectory.x;
    }
}
