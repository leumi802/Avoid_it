using UnityEngine;

public class collisionCount : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 충돌한 객체가 Player일 경우
            RandomSpawner spawner = FindObjectOfType<RandomSpawner>();
            if (spawner != null)
            {
                spawner.playerCollisionCount++;
                Debug.Log("플레이어와 충돌! 총 충돌 횟수: " + spawner.playerCollisionCount);
            }

             Destroy(gameObject);
        }
    }
}
