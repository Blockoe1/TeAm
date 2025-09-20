using UnityEngine;

public class BackgroundMover : MonoBehaviour
{
    [SerializeField] private float parralaxMultiplier;

    private Transform cameraTransform;
    private float backgroundWidth;
    float previousPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        SpriteRenderer spriteRend = GetComponent<SpriteRenderer>();

        backgroundWidth = spriteRend.sprite.texture.width / spriteRend.sprite.pixelsPerUnit;
        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float delta = cameraTransform.position.x - previousPos;

        float targetPos =  transform.localPosition.x + (delta * -parralaxMultiplier);
        // Loop the background when it exceeds it's maximum
        // If our position is outside of the camera's bounds, then we should loop.
        if (Mathf.Abs(targetPos) >= backgroundWidth)
        {
            float offsetPos = targetPos % backgroundWidth;
            Debug.Log(offsetPos);
            targetPos = -targetPos + offsetPos * 2;

        }
        transform.localPosition = new Vector3(targetPos, transform.localPosition.y, transform.localPosition.z);

        previousPos = cameraTransform.position.x;
    }
}
