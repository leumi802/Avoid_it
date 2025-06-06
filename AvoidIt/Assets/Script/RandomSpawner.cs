using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn;    //투사체 원본
    public GameObject Player;   //플레이어
    public int playerCollisionCount = 0;    //충돌 횟수
    public float minDistanceFromPlayer = 0.5f;    //플레이어와 투사체의 최소간격
    public float spawnInterval = 2f;  // 생성 간격
    public float minLaunchForce = 2f;   //최소 속도
    public float maxLaunchForce = 5f;   //최대 속도
    public int maxSpawnCount = 500;     //최대 투사체 수
    private int spawnCount = 0;     //투사체가 소환된 수



    void Start()
    {
        InvokeRepeating("SpawnAndLaunchObject", 0f, spawnInterval);
    }

    void SpawnAndLaunchObject()
    {
        if (spawnCount >= maxSpawnCount) return;    //종료조건(재설정 필요요)

        float clonesToSpawn = spawnCount / 10 + 1;    //한번에 소환되는 투사체의 갯수
        float min = minLaunchForce*(clonesToSpawn/2);
        float max = maxLaunchForce*(clonesToSpawn/2);

        if (clonesToSpawn > 10) //투사체는 최대 10개로 제한
        {
            clonesToSpawn = 10;
        }

        for (int i = 0; i < clonesToSpawn; i++)
            {

            Vector3 randomPosition;

            int attempt = 0;
            int maxAttempts = 10;

            do
            {
                //투사체의 위치를 무작위로 설정
                randomPosition = new Vector3(
                Random.Range(-9, 24),
                5f,
                Random.Range(7, -30)
            );
                attempt++;
            }
    //플레이어와 너무 가까운 위치에서 투사체가 등장하지 않도록 함함
    while (Vector3.Distance(Player.transform.position, randomPosition) < minDistanceFromPlayer && attempt < maxAttempts);

                GameObject obj = Instantiate(prefabToSpawn, randomPosition, Quaternion.identity);   // 투사체의 클론을 생성
                obj.transform.localScale = new Vector3(3f, 3f, 3f);    // 투사체 크기 확대
                Rigidbody rb = obj.GetComponent<Rigidbody>();   // 투사체에 rigidBody 속성 부여
                obj.AddComponent<AutoDestroyWhenFall>();    // 투사체가 일정 구간을 지나면 사라지도록 함
                obj.AddComponent<CollisionTrigger>();   // 플레이어와 투사체가 닿으면 카운트가 증가

            if (!obj.TryGetComponent<Collider>(out Collider col))   // 투사체에 collider가 설정되어 있지 않은경우 자동설정
            {
                col = obj.AddComponent<SphereCollider>();
            }
            // 반드시 Trigger 설정
            col.isTrigger = true;

            // 투사체에 rigidBody가 설정되어 있지 않은 경우에 자동설정
                if (rb == null)
            {
                rb = obj.AddComponent<Rigidbody>();
            }
            rb.useGravity = false;
            rb.linearDamping = 0f;
            // 투사체가 xz축의의 무작위 방향으로 날아가도록 함
            Vector2 random2D = Random.insideUnitCircle.normalized;
            Vector3 direction = new Vector3(random2D.x, 0f, random2D.y);
            rb.AddForce(direction * Random.Range(min,max), ForceMode.Impulse);

            // 투사체의 방향을을 날아가는 방향으로 설정
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction, Vector3.up);
                obj.transform.rotation = lookRotation * Quaternion.Euler(90f, 0f, 0f);
            }

               spawnCount++;    // 투사체 발사 갯수 카운트 증가
            } 
    }   
}
