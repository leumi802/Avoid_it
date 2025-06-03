using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;
    public float jumpForce = 5f;           // ← 살짝 점프만
    public float gravity = -15f;           // 중력을 조금 강하게 설정
    public int maxJumpCount = 2;           // 이중 점프용

    private CharacterController controller;
    private Vector3 velocity;
    private int jumpCount = 0;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // 바닥에 닿았는지 체크
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            jumpCount = 0; // 바닥에 닿으면 점프 횟수 초기화
        }

        // 이동 처리
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * currentSpeed * Time.deltaTime);

        // 점프 입력 처리
        if (Input.GetButtonDown("Jump") && jumpCount < maxJumpCount)
        {
            velocity.y = jumpForce;
            jumpCount++;
        }

        // 중력 적용
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
