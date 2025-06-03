using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(4f, 2f, 0f); // 앞 + 약간 위쪽

    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + target.right * offset.x + target.up * offset.y + target.forward * offset.z;
            transform.LookAt(target); // 항상 플레이어 바라보도록
        }
    }
}
