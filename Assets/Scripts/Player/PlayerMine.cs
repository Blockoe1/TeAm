using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMine : MonoBehaviour
{
    [SerializeField] private float range = 5f;
    [SerializeField] private Transform mouseDirectionPoint;
    private PlayerInput pInput;
    private InputAction mine;

    private bool isMining = false;

    public bool IsMining { get => isMining; set => isMining = value; }

    private void Start()
    {
        pInput = GetComponent<PlayerInput>();

        mine = pInput.currentActionMap.FindAction("Mine");
        mine.started += Mine_started;
    }

    private void Mine_started(InputAction.CallbackContext obj)
    {
        isMining = true;
    }
    public void Mine()
    {

        Vector2 direction = mouseDirectionPoint.position - transform.position;
        direction.Normalize();
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, range, 1 << LayerMask.NameToLayer("Ground"));

        if (!hit)
        {
            return;
        }

        if (hit.collider.gameObject.GetComponent<BreakableObject>())
        {
            Debug.Log("Mine");
            hit.collider.gameObject.GetComponent<BreakableObject>().BreakObject();
        }
    }

    public void CollectBitCoin()
    {
        StartCoroutine(GetBitCoin());
    }
    private IEnumerator GetBitCoin()
    {
        while (true)
        {
            ScoreScript.TimerSinceGettingBitcoin += 1;
            yield return new WaitForSeconds(1);
        }
    }
}
