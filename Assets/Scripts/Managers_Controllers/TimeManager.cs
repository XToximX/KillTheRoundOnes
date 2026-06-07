using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager I;
    private bool isPaused;

    private void Awake()
    {
        if (I != null && I != this)
        {
            Destroy(gameObject);
            return;
        }

        I = this;

        Time.timeScale = 1.0f;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if(!isPaused)
        {
            if (!hasFocus)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }

    public void PauseTime()
    {
        if (!isPaused)
        {
            Time.timeScale = 0f;
            isPaused = true;
        }
        else
        {
            isPaused = false;
            Time.timeScale = 1f;
        }
    }
}
