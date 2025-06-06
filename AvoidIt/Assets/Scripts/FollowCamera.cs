using UnityEngine;

public class AdvancedCamera : MonoBehaviour
{
    public Transform target;
    public float distance = 2.0f;            // 기본 거리
    public float minDistance = 1.0f;         // 너무 가까이 붙지 않도록 최소 거리 제한
    public float maxDistance = 4.0f;         // 줌 최대 거리

    public float height = 1.6f;              // 캐릭터 머리 위에서 시점 유지
    public float rotationSpeed = 5.0f;
    public float zoomSpeed = 2.0f;
    public float minY = -30f;
    public float maxY = 60f;

    private float currentX = 0f;
    private float currentY = 10f;

    void LateUpdate()
    {
        if (target == null) return;

        // 마우스 회전 (우클릭 시에만 회전하고 싶다면 Input.GetMouseButton(1) 조건 추가)
        currentX += Input.GetAxis("Mouse X") * rotationSpeed;
        currentY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        currentY = Mathf.Clamp(currentY, minY, maxY);

        // 마우스 휠 줌
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance -= scroll * zoomSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        // 회전 계산
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 desiredOffset = rotation * new Vector3(0, 0, -distance);
        Vector3 targetPosition = target.position + Vector3.up * height;
        Vector3 desiredPosition = targetPosition + desiredOffset;

        // 충돌 방지: Raycast로 카메라 위치 조정
        RaycastHit hit;
        if (Physics.Linecast(targetPosition, desiredPosition, out hit))
        {
            // 충돌 지점으로 카메라 위치 제한
            transform.position = hit.point;
        }
        else
        {
            transform.position = desiredPosition;
        }

        // 항상 타겟 바라보기
        transform.LookAt(targetPosition);
    }
}
