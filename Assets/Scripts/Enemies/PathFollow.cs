using System;
using UnityEngine;

public class PathFollow : MonoBehaviour
{
    private Vector3 currentStart;
    private Vector3 currentTarget;
    private int targetNumber = 1;

    private float speed = 5f;

    public float distanceTravelled { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        distanceTravelled = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, currentTarget) > 0.01f)
        {
            float distance = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, currentTarget, distance);
            distanceTravelled += distance;
        }
        else if (targetNumber != Path.I.pathPoints.Count)
        {
            currentTarget = Path.I.pathPoints[targetNumber++].position;
            float distance = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, currentTarget, distance);
            distanceTravelled += distance;
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
