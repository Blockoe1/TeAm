using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] float maxDistanceFromPlayer = 5f;
    [SerializeField] float dampening;
    [SerializeField] Transform player;
    private void Update()
    {
        Vector2 screenCenter = new Vector2(Screen.width, Screen.height) / 2;

        Vector2 mousePosition = Mouse.current.position.ReadValue();

        float pythagoranTheorum = Mathf.Sqrt((Screen.width ^ 2) +  (Screen.height ^ 2));

        Vector2 finalPos = (mousePosition - screenCenter) / (dampening * pythagoranTheorum);

        if(finalPos.magnitude > maxDistanceFromPlayer)
        {
            finalPos = finalPos.normalized;
            finalPos *= maxDistanceFromPlayer;
        }

        finalPos += (Vector2)player.position;

        transform.position = new(finalPos.x, finalPos.y);
    }
}
