using UnityEngine;
using UnityEngine.UI;

public class CollisionUI : MonoBehaviour
{
    public Text collisionText;
    private RandomSpawner spawner;

    void Start()
    {
        spawner = FindObjectOfType<RandomSpawner>();
    }

    void Update()
    {
        collisionText.text = "충돌 횟수: " + spawner.playerCollisionCount;
    }
}