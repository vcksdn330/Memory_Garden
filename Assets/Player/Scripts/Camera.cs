using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform player; // 따라갈 플레이어
    public Vector2 minBounds; // 카메라가 갈 수 있는 최소 좌표
    public Vector2 maxBounds; // 카메라가 갈 수 있는 최대 좌표
    
    private void LateUpdate()
    {
        if (player == null) return;

        // 목표 위치 설정 (플레이어를 따라감)
        Vector3 targetPosition = new Vector3(player.position.x, player.position.y, transform.position.z);

        // 카메라가 맵 바깥으로 나가지 않도록 제한
        targetPosition.x = Mathf.Clamp(targetPosition.x, minBounds.x, maxBounds.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minBounds.y, maxBounds.y);

        transform.position = targetPosition;
    }
}
