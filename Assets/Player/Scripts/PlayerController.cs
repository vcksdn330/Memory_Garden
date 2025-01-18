using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // ĳ���� �̵� �ӵ�
    public Animator animator;   // Animator ����

    private Vector2 movement;

    private bool isMove;        // �̵� ����

    void Update()
    {
        // �̵� �Է� �ޱ� (WASD �Ǵ� ȭ��ǥ Ű)
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        isMove = movement.x != 0 || movement.y != 0;

        // Animator�� �̵� ���� ����
        animator.SetBool("isMove", isMove);

        // �̵� ������ Animator�� ����
        if (isMove)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
        }
    }

    void FixedUpdate()
    {
        // ĳ���� �̵� ó��
        if (isMove)
        {
            Vector2 position = (Vector2)transform.position + movement.normalized * moveSpeed * Time.fixedDeltaTime;
            transform.position = position;
        }
    }
}
