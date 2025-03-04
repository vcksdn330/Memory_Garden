using System.Collections.Generic;
using UnityEngine;

public class PlayerCustomization : MonoBehaviour
{
    public Animator animator;

    // 각 부위의 SpriteRenderer
    public SpriteRenderer bodyRenderer;
    public SpriteRenderer headRenderer;
    public SpriteRenderer coatRenderer;
    public SpriteRenderer shirtRenderer;
    public SpriteRenderer legsRenderer;
    public SpriteRenderer feetRenderer;

    // 부위별 SpriteRenderer 맵
    private Dictionary<string, SpriteRenderer> renderers;

    // 상태별 스프라이트 맵
    private Dictionary<string, Dictionary<string, Sprite[]>> sprites;

    // 현재 상태
    public string currentState;

    // 캐릭터 총 프레임
    public int bodyTotalFrames = 6;

    void Start()
    {
      
        // SpriteRenderer 초기화
        renderers = new Dictionary<string, SpriteRenderer>
        {
            { "body", bodyRenderer },
            { "head", headRenderer },
            { "coat", coatRenderer },
            { "shirt", shirtRenderer },
            { "legs", legsRenderer },
            { "feet", feetRenderer }
        };

        // 스프라이트 맵 초기화
        sprites = new Dictionary<string, Dictionary<string, Sprite[]>>();

        // 초기 상태 설정
        currentState = "front";

    }

    void Update()
    {
        // 현재 애니메이션 상태 확인
        string newState = GetCurrentAnimationState();

        if (newState != currentState)
        {
            currentState = newState;
        }

        // 현재 상태에 따라 모든 부위 스프라이트 업데이트
        UpdateClothingSprites(GetCurrentFrameIndex());
    }

    // 현재 애니메이션 상태 반환
    string GetCurrentAnimationState()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Idle_Front") || stateInfo.IsName("Walk_Front")) return "front";
        if (stateInfo.IsName("Idle_Back") || stateInfo.IsName("Walk_Back")) return "back";
        if (stateInfo.IsName("Idle_Left") || stateInfo.IsName("Walk_Left")) return "left";
        if (stateInfo.IsName("Idle_Right") || stateInfo.IsName("Walk_Right")) return "right";
        return currentState;
    }

    // 현재 애니메이션 진행 프레임 반환
    int GetCurrentFrameIndex()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float normalizedTime = stateInfo.normalizedTime % 1; // 현재 애니메이션 진행률 (0~1)

        // 저장된 Body 총 프레임 수 사용
        return Mathf.FloorToInt(normalizedTime * bodyTotalFrames);
    }


    // 옷 로드 (공통 함수)
    public void LoadClothing(string part, string clothingName)
    {
        if (!sprites.ContainsKey(part))
        {
            sprites[part] = new Dictionary<string, Sprite[]>();
        }

        // 각 방향별 스프라이트 로드
        sprites[part]["front"] = Resources.LoadAll<Sprite>($"Clothes/{part}/{clothingName}/front");
        sprites[part]["back"] = Resources.LoadAll<Sprite>($"Clothes/{part}/{clothingName}/back");
        sprites[part]["left"] = Resources.LoadAll<Sprite>($"Clothes/{part}/{clothingName}/left");
        sprites[part]["right"] = Resources.LoadAll<Sprite>($"Clothes/{part}/{clothingName}/right");
    }

    // 현재 프레임에 따라 모든 부위의 스프라이트 업데이트
    void UpdateClothingSprites(int currentFrame)
    {
        foreach (var part in renderers.Keys)
        {
            if (sprites.ContainsKey(part))
            {
                // 현재 상태 스프라이트 확인
                if (sprites[part].ContainsKey(currentState))
                {
                    Sprite[] currentSprites = sprites[part][currentState];

                    // 스프라이트가 없을 경우 렌더링 비활성화
                    if (currentSprites == null || currentFrame >= currentSprites.Length)
                    {
                        renderers[part].enabled = false; // 렌더링 비활성화
                    }
                    else
                    {
                        renderers[part].enabled = true; // 렌더링 활성화
                        renderers[part].sprite = currentSprites[currentFrame];
                    }
                }
                else
                {
                    // 해당 방향 이미지가 없을 경우 렌더링 비활성화
                    renderers[part].enabled = false;
                }
            }
        }
    }
}
