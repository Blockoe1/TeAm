using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using NaughtyAttributes;

public class PlayerFiring : MonoBehaviour
{
    [SerializeField] private Vector3 _launchPosRelative;
    [SerializeField] private float _visDistance;
    [SerializeField, Range(0,360)] private int _degrees;
    private float _rad;
    private Vector3 playerPos;
    [SerializeField, Required] private GameObject _canary;
    [SerializeField] private bool _canLaunch;
    Vector2 launchPos;

    private PlayerInput pInput;

    private InputAction shoot;

    public Vector2 LaunchPos { get => launchPos; set => launchPos = value; }

    void Start()
    {
        _rad = Mathf.Deg2Rad * _degrees;
        pInput = GetComponent<PlayerInput>();
        shoot = pInput.currentActionMap.FindAction("Shoot");

        shoot.performed += Shoot_performed;
    }

    private void Shoot_performed(InputAction.CallbackContext obj)
    {
        if (_canLaunch)
        {
            // start animation
            playerPos = transform.position;
            _rad = Mathf.Deg2Rad * _degrees;
            Vector2 _launchPos = playerPos + _launchPosRelative;
            GameObject canary = Instantiate(_canary, new Vector3(_launchPos.x, _launchPos.y, 0f), Quaternion.identity);
            StartCoroutine(canary.GetComponent<CanaryBehavior>().Launch(_rad, _visDistance, this));
            _canLaunch = false;
        }

    }

    public void ResetCanary()
    {
        _canLaunch = true;
    }



#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        playerPos = transform.position;
        _rad = Mathf.Deg2Rad * _degrees;
        Gizmos.color = Color.red;
        Vector2 _launchPos = playerPos + _launchPosRelative;
        Gizmos.DrawSphere(_launchPos, .2f);
        launchPos = new Vector2(_launchPos.x + Mathf.Cos(_rad) * _visDistance, _launchPos.y + Mathf.Sin(_rad) * _visDistance);
        Gizmos.DrawLine(_launchPos, launchPos);
    }
#endif
}
