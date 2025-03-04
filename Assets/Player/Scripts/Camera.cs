using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform player; // ���� �÷��̾�
    public Vector2 minBounds; // ī�޶� �� �� �ִ� �ּ� ��ǥ
    public Vector2 maxBounds; // ī�޶� �� �� �ִ� �ִ� ��ǥ
    
    private void LateUpdate()
    {
        if (player == null) return;

        // ��ǥ ��ġ ���� (�÷��̾ ����)
        Vector3 targetPosition = new Vector3(player.position.x, player.position.y, transform.position.z);

        // ī�޶� �� �ٱ����� ������ �ʵ��� ����
        targetPosition.x = Mathf.Clamp(targetPosition.x, minBounds.x, maxBounds.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minBounds.y, maxBounds.y);

        transform.position = targetPosition;
    }
}
