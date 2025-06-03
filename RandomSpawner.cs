using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn;    //투사체 원본
    public GameObject Player;   //플레이어
    public int playerCollisionCount = 0;    //충돌 횟수
    public float minDistanceFromPlayer = 1f;    //플레이어와 투사체의 최소간격
    public float rangeZ = 10;   //투사체 소환 범위
    public float spawnInterval = 3f;  // 생성 간격
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
        if (spawnCount >= maxSpawnCount) return;

        int clonesToSpawn = spawnCount / 10 + 1;    //한번에 소환되는 투사체의 갯수
        float min = minLaunchForce*(clonesToSpawn/2);
        float max = maxLaunchForce*(clonesToSpawn/2);

        if (clonesToSpawn == 1)
        {
            min = maxLaunchForce;
            max = maxLaunchForce;
        }

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
        randomPosition = new Vector3(
            Random.Range(-5, 15),
            6f,
            Random.Range(-5, 15)
        );
        attempt++;
    }
    while (Vector3.Distance(Player.transform.position, randomPosition) < minDistanceFromPlayer && attempt < maxAttempts);

                GameObject obj = Instantiate(prefabToSpawn, randomPosition, Quaternion.identity);
                obj.transform.localScale = new Vector3(3f, 3f, 3f);
                Rigidbody rb = obj.GetComponent<Rigidbody>();
                obj.AddComponent<AutoDestroyWhenFall>();
                obj.AddComponent<collisionCount>();


                if (rb == null)
                {
                    rb = obj.AddComponent<Rigidbody>();
                }

                Vector2 random2D = Random.insideUnitCircle.normalized;
                Vector3 direction = new Vector3(random2D.x, 0f, random2D.y);
                rb.AddForce(direction * Random.Range(min,max), ForceMode.Impulse);

                spawnCount++;
            } 
    }   
}
