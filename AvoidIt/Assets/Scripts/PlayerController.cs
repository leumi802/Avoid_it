using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    // 이동 관련 변수
    public float moveSpeed = 4f;
    public float sprintMultiplier = 1.5f;
    public float jumpForce = 10f; // 점프 힘 증가
    public int maxJumpCount = 2;  // 이중 점프 가능
    public float gravityMultiplier = 2.5f; // 중력 강화
    public float rotationSpeed = 700f; // 회전 속도

    // 애니메이션 관련
    public Animator animator;

    // 투사체 회전용 변수
    public Transform target;  // 투사체나 적을 바라볼 타겟

    private CharacterController controller;
    private Vector3 velocity;
    private int jumpCount = 0;
    private bool isGrounded;

    public Transform cameraTransform;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }

        if (animator == null)
        {
            animator = GetComponent<Animator>();  // 애니메이터 컴포넌트 가져오기
        }
    }

    void Update()
    {
        // 바닥에 닿았는지 확인
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            jumpCount = 0;  // 점프 횟수 초기화
        }

        // 이동 입력 받기 (Input.GetAxisRaw로 변경)
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        // 카메라 방향에 따른 이동
        Vector3 move = camForward * vertical + camRight * horizontal;

        // 스프린트 처리
        float speed = moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift))  // Shift 누르면 스프린트
            speed *= sprintMultiplier;

        // 이동
        controller.Move(move * speed * Time.deltaTime);

        // 점프 처리 (이중 점프 포함)
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y);
            jumpCount = 1;  // 첫 번째 점프
        }
        else if (!isGrounded && jumpCount < maxJumpCount && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y);  // 두 번째 점프
            jumpCount++;
        }

        // 중력 적용
        velocity.y += Physics.gravity.y * gravityMultiplier * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // 애니메이션 처리 (걷기 애니메이션)
        AnimatePlayer(horizontal, vertical);

        // 플레이어 회전 처리 (투사체나 타겟을 바라보는 기능)
        RotateTowardsTarget();
    }

    private void AnimatePlayer(float horizontal, float vertical)
    {
        // 이동 방향에 따른 애니메이션 상태 처리
        bool isWalking = horizontal != 0 || vertical != 0;
        animator.SetBool("isWalking", isWalking);  // 이동 중이면 걷기 애니메이션 활성화
    }

    private void RotateTowardsTarget()
    {
        if (target != null)
        {
            // 타겟(투사체나 적)을 향해 회전
            Vector3 direction = target.position - transform.position;
            direction.y = 0; // y축 회전은 제외
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            // 타겟이 없으면 이동 방향을 향해 회전
            Vector3 moveDirection = controller.velocity;
            moveDirection.y = 0;
            if (moveDirection.magnitude > 0)
            {
                Quaternion toRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }

    // 충돌 처리 (추가적으로 벽 충돌 시의 반응을 추가할 수 있음)
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("wall"))
        {
            // 벽에 부딪혔을 때 처리 (예: 충격 반응, 소리 등)
            Debug.Log("Player hit the wall!");
        }
    }
}
