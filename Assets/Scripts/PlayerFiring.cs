using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFiring : MonoBehaviour
{
    [SerializeField] private Vector3 _launchPosRelative;
    [SerializeField] private float _visDistance;
    [SerializeField, Range(0,360)] private int _degrees;
    private float _rad;
    private Vector3 playerPos;

    private PlayerInput pInput;

    private InputAction shoot;

    void Start()
    {
        _rad = Mathf.Deg2Rad * _degrees;
        pInput = GetComponent<PlayerInput>();
        shoot = pInput.currentActionMap.FindAction("Shoot");
    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        playerPos = transform.position;
        _rad = Mathf.Deg2Rad * _degrees;
        Gizmos.color = Color.red;
        Vector2 _launchPos = playerPos + _launchPosRelative;
        Gizmos.DrawSphere(_launchPos, .2f);
        Gizmos.DrawLine(_launchPos, new Vector2(_launchPos.x + Mathf.Cos(_rad) * _visDistance, _launchPos.y + Mathf.Sin(_rad) * _visDistance));
    }
#endif
}
