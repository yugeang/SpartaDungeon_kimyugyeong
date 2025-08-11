using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
/*WASD 이동 (Rigidbody.velocity로 처리)
마우스 카메라 회전 (상하/좌우 분리 처리)
점프 (Raycast로 바닥 감지 후 위로 힘 가하기)
커서 잠금 & 시야 회전 on/off*/
{
    [Header("Movement")]
    public float moveSpeed;
    private Vector2 curMovementInput;  // 현재 입력된 WASD 값 저장
    public float jumpPower;
    public LayerMask groundLayerMask;  // 레이어 지정

    [Header("Look")]
    public Transform cameraContainer; //카메라의 상하 회전 담당할 오브젝트
    public float minXLook;  // 최소 시야각
    public float maxXLook;  // 최대 시야각
    private float camCurXRot; //현재 카메라 회전값 저장
    public float lookSensitivity; // 마우스 감도

    private Vector2 mouseDelta;  // 마우스 이동량

    [HideInInspector]
    public bool canLook = true; //true면 마우스 회전 허용

    private Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // 물리 연산
    private void FixedUpdate()
    {
        Move();
    }

    // 모든 연산 후 카메라 회전 처리
    private void LateUpdate()
    {
        if (canLook)
        {
            CameraLook();
        }
    }

    // 마우스 이동량 mouseDelta에 저장
    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    // 이동키 입력값 curMovementInput에 저장
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {
            rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
        }
    }

    private void Move()
    {
        // W/S → z축(앞/뒤), A/D → x축(좌/우) 방향 벡터 계산
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;  // 속도에 moveSpeed 곱
        dir.y = rigidbody.velocity.y;  // y값은 velocity(변화량)의 y 값을 넣어준다.

        rigidbody.velocity = dir;  // 최종 속도를 Rigidbody에 적용
    }

    void CameraLook()
    {
        // 마우스 움직임의 변화량(mouseDelta)중 y(위 아래)값에 민감도를 곱한다.
        // 카메라가 위 아래로 회전하려면 rotation의 x 값에 넣어준다.
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);
        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
        new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
        new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
        new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
        new Ray(transform.position + (-transform.right * 0.2f) +(transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }

    public void ToggleCursor(bool toggle)
        //toggle = true > 커서 보이게, 시야 회전 비활성
        //toggle = false > 커서 숨김, 시야 회전 활성
    {
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }
}