using UnityEngine;

public class CollisionTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("트리거 충돌 감지됨: " + other.name);

            var spawner = FindAnyObjectByType<RandomSpawner>();
            if (spawner != null)
            {
                spawner.playerCollisionCount++;
                Debug.Log("충돌 카운트: " + spawner.playerCollisionCount);
                if(spawner.playerCollisionCount>2){
                    Debug.Log("3회 이상 충돌함: 프로그램 종료됨.");

                    #if UNITY_EDITOR
                    // 에디터에서는 플레이 모드 종료
                        UnityEditor.EditorApplication.isPlaying = false;
                    #else
                    // 빌드된 게임에서는 종료
                        Application.Quit();
                    #endif
                }
                
            }

            Destroy(gameObject); // 부딪힌 투사체는 제거된다.
        }
    }
}
