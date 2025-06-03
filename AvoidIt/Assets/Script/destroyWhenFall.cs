using UnityEngine;

public class AutoDestroyWhenFall : MonoBehaviour
{
    public float destroyXThreshold = -20f;
    public float destroyXThreshold2 = 40f;
    public float destroyZThreshold = 20f;
    public float destroyZThreshold2 = -40f;
    void Update()
    {
        if (transform.position.x < destroyXThreshold)
        {
            Destroy(gameObject);  
        }
    }
}