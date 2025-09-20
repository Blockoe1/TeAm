using UnityEngine;

public class BackgroundMover : MonoBehaviour
{
    [SerializeField] private Vector2 levelMin;
    [SerializeField] private Vector2 levelMax;

    private Transform cameraTransform;
    private float backgroundWidth;
    private float backgroundHeight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        SpriteRenderer spriteRend = GetComponent<SpriteRenderer>();
        Sprite sprite = spriteRend.sprite;

        backgroundHeight = spriteRend.size.y;
        backgroundWidth = spriteRend.size.x;
        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector2 cameraPosition = new Vector2(Mathf.Clamp(cameraTransform.position.x, levelMin.x, levelMax.x),
            Mathf.Clamp(cameraTransform.position.y, levelMin.y, levelMax.y));
        Vector2 levelCenter = new Vector2((levelMax.x + levelMin.x) / 2, (levelMax.y + levelMin.y) / 2);
        Vector2 toCenter = cameraPosition - levelCenter;
        Debug.Log(levelCenter);

        float xPos = -CalculateBackgroundPosition(toCenter.x, levelMin.x, levelMax.x, backgroundWidth);
        float yPos = -CalculateBackgroundPosition(toCenter.y, levelMin.y, levelMax.y, backgroundHeight);

        transform.localPosition = new Vector2(xPos, yPos);
    }

    private float CalculateBackgroundPosition(float toCenter, float levelMin, float levelMax, 
        float backgroundDimension)
    {
        float ratio = toCenter / (levelMax - levelMin);
        float backPos = backgroundDimension * ratio;

        return backPos;
    }
}
