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
        // Body �ִϸ��̼��� ���� ������ �ε��� ��������
        int currentFrame = GetCurrentFrameIndex();

        headRenderer.sprite = headSprites[currentFrame];
        coatRenderer.sprite = coatSprites[currentFrame];
        shirtRenderer.sprite = shirtSprites[currentFrame];
        legsRenderer.sprite = legsSprites[currentFrame];
        feetRenderer.sprite = feetSprites[currentFrame];
    }

    int GetCurrentFrameIndex()
    {
        // ���� �ִϸ��̼��� ���� ���¸� ������� ������ �ε��� ���
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float normalizedTime = stateInfo.normalizedTime % 1; // ���� �����
        int totalFrames = shirtSprites.Length; // �� ������ ��
        return Mathf.FloorToInt(normalizedTime * totalFrames);
    }

}
