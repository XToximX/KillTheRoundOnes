using UnityEngine;

public class PathFollow : MonoBehaviour
{
    private Vector3 currentStart; 
    private Vector3 currentTarget;
    private int targetNumber = 1;

    private float speed = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentStart = Path.I.pathPoints[0].position;
        currentTarget = Path.I.pathPoints[1].position;

        transform.position = currentStart;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != currentTarget)
        {
            transform.position = Vector2.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
        }
        else if (targetNumber != Path.I.pathPoints.Count)
        {
            currentTarget = Path.I.pathPoints[targetNumber++].position;
        }
        else
            GetComponent<Enemy>().DestroyEnemy();
    }
}
