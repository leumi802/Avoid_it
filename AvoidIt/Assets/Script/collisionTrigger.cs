using UnityEngine;

public class collisionTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("트리거 충돌 감지됨: " + other.name);

            var spawner = FindObjectOfType<RandomSpawner>();
            if (spawner != null)
            {
                spawner.playerCollisionCount++;
                Debug.Log("충돌 카운트: " + spawner.playerCollisionCount);
                
            }

            Destroy(gameObject); // 부딪힌 투사체는 제거된다.
        }
    }
}
