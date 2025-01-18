using UnityEngine;

public class PlayerCustomization : MonoBehaviour
{
    public Animator animator;

    public SpriteRenderer bodyRenderer;
    public SpriteRenderer headRenderer;
    public SpriteRenderer coatRenderer;
    public SpriteRenderer shirtRenderer;
    public SpriteRenderer legsRenderer;
    public SpriteRenderer feetRenderer;

    public Sprite[] headSprites;
    public Sprite[] coatSprites;
    public Sprite[] shirtSprites;
    public Sprite[] legsSprites;
    public Sprite[] feetSprites;


    public void LoadShirt(string shirtName)
    {
        shirtSprites = Resources.LoadAll<Sprite>($"Clothes/Shirt/{shirtName}");
    }

    public void Loadlegs(string legsName)
    {
        legsSprites = Resources.LoadAll<Sprite>($"Clothes/legs/{legsName}");
    }

    void Update()
    {
        // Body 애니메이션의 현재 프레임 인덱스 가져오기
        int currentFrame = GetCurrentFrameIndex();

        headRenderer.sprite = headSprites[currentFrame];
        coatRenderer.sprite = coatSprites[currentFrame];
        shirtRenderer.sprite = shirtSprites[currentFrame];
        legsRenderer.sprite = legsSprites[currentFrame];
        feetRenderer.sprite = feetSprites[currentFrame];
    }

    int GetCurrentFrameIndex()
    {
        // 현재 애니메이션의 진행 상태를 기반으로 프레임 인덱스 계산
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float normalizedTime = stateInfo.normalizedTime % 1; // 현재 진행률
        int totalFrames = shirtSprites.Length; // 총 프레임 수
        return Mathf.FloorToInt(normalizedTime * totalFrames);
    }

}
