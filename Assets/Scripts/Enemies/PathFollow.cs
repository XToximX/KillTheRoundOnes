using UnityEngine;

public class PathFollow : MonoBehaviour
{
    private Vector3 currentStart; 
    private Vector3 currentTarget;
    private int targetNumber = 1;

    private float speed = 5f;

    private float distanceTravelled = 0f;
    public float DistanceTravelled => distanceTravelled;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, currentTarget) > 0.01f)
        {
            Vector2 lastPos = transform.position;
            transform.position = Vector2.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
            distanceTravelled += Vector2.Distance(transform.position, lastPos);
        }
        else if (targetNumber != Path.I.pathPoints.Count)
        {
            currentTarget = Path.I.pathPoints[targetNumber++].position;
        }
        else
            GetComponent<Enemy>().DestroyEnemy();
    }

    private void FixedUpdate()
    {
        
    }

    public void StartFollowingPath(SO_Enemy enemyData)
    {
        speed = enemyData.Speed;

        currentStart = Path.I.pathPoints[0].position;
        currentTarget = Path.I.pathPoints[1].position;

        transform.position = currentStart;
    }
}
