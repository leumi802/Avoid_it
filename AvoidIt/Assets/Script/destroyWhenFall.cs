using UnityEngine;

public class AutoDestroyWhenFall : MonoBehaviour
{
    public float destroyYThreshold = -10f;

    void Update()
    {
        if (transform.position.y < destroyYThreshold)
        {
            Destroy(gameObject);  
        }
    }
}