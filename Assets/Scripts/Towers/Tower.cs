using UnityEngine;

public class Tower : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUpTower(SO_Tower towerData)
    {
        spriteRenderer.enabled = true;
        spriteRenderer.color = towerData.Color;
    }

    public void ClearTower()
    {
        spriteRenderer.enabled = false;
        return;
    }    
}
