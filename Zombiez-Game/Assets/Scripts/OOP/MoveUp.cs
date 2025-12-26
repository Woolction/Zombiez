using UnityEngine;

public class MoveUp : MonoBehaviour
{
    private float t;
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    
    void Update()
    {
        t += Time.deltaTime / 3f;
        
        rectTransform.position += Vector3.up * t;

        if (t > 1f)
        {
            Destroy(gameObject);
        }
    }
}
