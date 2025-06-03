using UnityEngine;

public class AutoDestroyWhenFall : MonoBehaviour
{
    public float destroyXThreshold = -20f;
    public float destroyXThreshold2 = 40f;
    public float destroyZThreshold2 = 20f;
    public float destroyZThreshold = -40f;
    void Update()
    {
        if (transform.position.x < destroyXThreshold || transform.position.x > destroyXThreshold2)
        {
            Destroy(gameObject);
        }
        if (transform.position.z < destroyZThreshold || transform.position.z > destroyZThreshold2)
        {
            Destroy(gameObject);  
        }
    }
}