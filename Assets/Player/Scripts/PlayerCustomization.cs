using System.Collections.Generic;
using UnityEngine;

public class PlayerCustomization : MonoBehaviour
{
    public Animator animator;

    // �� ������ SpriteRenderer
    public SpriteRenderer headRenderer;
    public SpriteRenderer coatRenderer;
    public SpriteRenderer shirtRenderer;
    public SpriteRenderer legsRenderer;
    public SpriteRenderer feetRenderer;

    // ������ SpriteRenderer ��
    private Dictionary<string, SpriteRenderer> renderers;

    // ���º� ��������Ʈ ��
    private Dictionary<string, Dictionary<string, Sprite[]>> sprites;

    // ���� ����
    public string currentState;

    // ĳ���� �� ������
    public int bodyTotalFrames = 6;

    void Start()
    {
      
        // SpriteRenderer �ʱ�ȭ
        renderers = new Dictionary<string, SpriteRenderer>
        {
            { "head", headRenderer },
            { "coat", coatRenderer },
            { "shirt", shirtRenderer },
            { "legs", legsRenderer },
            { "feet", feetRenderer }
        };

        // ��������Ʈ �� �ʱ�ȭ
        sprites = new Dictionary<string, Dictionary<string, Sprite[]>>();

        // �ʱ� ���� ����
        currentState = "front";

    }

    void Update()
    {
        // ���� �ִϸ��̼� ���� Ȯ��
        string newState = GetCurrentAnimationState();

        if (newState != currentState)
        {
            currentState = newState;
        }

        // ���� ���¿� ���� ��� ���� ��������Ʈ ������Ʈ
        UpdateClothingSprites(GetCurrentFrameIndex());
    }

    // ���� �ִϸ��̼� ���� ��ȯ
    string GetCurrentAnimationState()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Idle_Front") || stateInfo.IsName("Walk_Front")) return "front";
        if (stateInfo.IsName("Idle_Back") || stateInfo.IsName("Walk_Back")) return "back";
        if (stateInfo.IsName("Idle_Left") || stateInfo.IsName("Walk_Left")) return "left";
        if (stateInfo.IsName("Idle_Right") || stateInfo.IsName("Walk_Right")) return "right";
        return currentState;
    }

    // ���� �ִϸ��̼� ���� ������ ��ȯ
    int GetCurrentFrameIndex()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float normalizedTime = stateInfo.normalizedTime % 1; // ���� �ִϸ��̼� ����� (0~1)

        // ����� Body �� ������ �� ���
        return Mathf.FloorToInt(normalizedTime * bodyTotalFrames);
    }


    // �� �ε� (���� �Լ�)
    public void LoadClothing(string part, string clothingName)
    {
        if (!sprites.ContainsKey(part))
        {
            sprites[part] = new Dictionary<string, Sprite[]>();
        }

        // �� ���⺰ ��������Ʈ �ε�
        sprites[part]["front"] = Resources.LoadAll<Sprite>($"Clothes/{part}/{clothingName}/front");
        sprites[part]["back"] = Resources.LoadAll<Sprite>($"Clothes/{part}/{clothingName}/back");
        sprites[part]["left"] = Resources.LoadAll<Sprite>($"Clothes/{part}/{clothingName}/left");
        sprites[part]["right"] = Resources.LoadAll<Sprite>($"Clothes/{part}/{clothingName}/right");
    }

    // ���� �����ӿ� ���� ��� ������ ��������Ʈ ������Ʈ
    void UpdateClothingSprites(int currentFrame)
    {
        foreach (var part in renderers.Keys)
        {
            if (sprites.ContainsKey(part))
            {
                // ���� ���� ��������Ʈ Ȯ��
                if (sprites[part].ContainsKey(currentState))
                {
                    Sprite[] currentSprites = sprites[part][currentState];

                    // ��������Ʈ�� ���� ��� ������ ��Ȱ��ȭ
                    if (currentSprites == null || currentFrame >= currentSprites.Length)
                    {
                        renderers[part].enabled = false; // ������ ��Ȱ��ȭ
                    }
                    else
                    {
                        renderers[part].enabled = true; // ������ Ȱ��ȭ
                        renderers[part].sprite = currentSprites[currentFrame];
                    }
                }
                else
                {
                    // �ش� ���� �̹����� ���� ��� ������ ��Ȱ��ȭ
                    renderers[part].enabled = false;
                }
            }
        }
    }
}
