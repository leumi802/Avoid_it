using UnityEngine;

public class AutoDestroyWhenFall : MonoBehaviour
{
    public float destroyXThreshold = -20f;  //투사체가 남아있을 공간의 좌표
    public float destroyXThreshold2 = 40f;
    public float destroyZThreshold2 = 20f;
    public float destroyZThreshold = -40f;
    void Update()
    {
        if (transform.position.x < destroyXThreshold || transform.position.x > destroyXThreshold2)  // 투사체가 해당 구간을 벗어나면 사라지게 함
        {
            Destroy(gameObject);
        }
        if (transform.position.z < destroyZThreshold || transform.position.z > destroyZThreshold2)  // 투사체가 해당 구간을 벗어나면 사라지게 함
        {
            Destroy(gameObject);  
        }
    }
}