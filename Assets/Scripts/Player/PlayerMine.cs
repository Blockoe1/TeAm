using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMine : MonoBehaviour
{
    [SerializeField] private float range = 5f;
    [SerializeField] private Transform mouseDirectionPoint;
    private PlayerInput pInput;
    private InputAction mine;

    private void Start()
    {
        pInput = GetComponent<PlayerInput>();

        mine = pInput.currentActionMap.FindAction("Mine");
        mine.started += Mine_started;
    }

    private void Mine_started(InputAction.CallbackContext obj)
    {
        Vector2 direction = mouseDirectionPoint.position - transform.position;
        direction.Normalize();

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, range, 1 << LayerMask.NameToLayer("Ground"));
        print(hit);

        if (!hit)
        {
            return;
        }

        print(hit.collider);
    }
}
