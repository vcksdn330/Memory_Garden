using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // 캐릭터 이동 속도
    public Animator animator;   // Animator 참조

    private Vector2 movement;

    private bool isMove;        // 이동 여부

    void Update()
    {
        // 이동 입력 받기 (WASD 또는 화살표 키)
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        isMove = movement.x != 0 || movement.y != 0;

        // Animator에 이동 여부 전달
        animator.SetBool("isMove", isMove);

        // 이동 방향을 Animator에 전달
        if (isMove)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
        }
    }

    void FixedUpdate()
    {
        // 캐릭터 이동 처리
        if (isMove)
        {
            Vector2 position = (Vector2)transform.position + movement.normalized * moveSpeed * Time.fixedDeltaTime;
            transform.position = position;
        }
    }
}
