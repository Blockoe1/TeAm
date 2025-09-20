using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFiring : MonoBehaviour
{
    [SerializeField] private Vector3 _launchPosRelative;
    [SerializeField] private float _visDistance;
    [SerializeField, Range(0,360)] private int _degrees;
    private int initDegrees;
    private float _rad;
    private Vector3 playerPos;
    [SerializeField, Required] private GameObject canary;
    private bool _canLaunch = true;
    Vector2 launchPos;
    [SerializeField] private int _maxHealth;

    private PlayerInput pInput;

    private InputAction shoot;

    private bool isSpitting = false;

    public Vector2 LaunchPos { get => launchPos; set => launchPos = value; }
    public bool IsSpitting { get => isSpitting; set => isSpitting = value; }
    private float dir;

    void Start()
    {
        _rad = Mathf.Deg2Rad * _degrees;
        initDegrees = _degrees;
        pInput = GetComponent<PlayerInput>();
        shoot = pInput.currentActionMap.FindAction("Shoot");

        shoot.performed += Shoot_performed;
        PlayerStatScript.RemainingHealth = _maxHealth;
    }

    private void Shoot_performed(InputAction.CallbackContext obj)
    {
        if (_canLaunch)
        {
            isSpitting = true;
            _canLaunch = false;
        }

    }

    public void SwitchDirections(float d)
    {
        if (d < 0)
        {
            _degrees = 180 - initDegrees;
        }
        else
        {
            _degrees = initDegrees;
        }
        dir = d;
        Debug.Log(_degrees);
        _rad = Mathf.Deg2Rad * _degrees;
    }
    public void Shoot()
    {
        Debug.Log("Shoot");
        playerPos = transform.position;
        _rad = Mathf.Deg2Rad * _degrees;
        Vector2 _launchPos = playerPos + _launchPosRelative;
        GameObject _can = Instantiate(canary, new Vector3(_launchPos.x, _launchPos.y, 0f), Quaternion.identity);

        StartCoroutine(_can.GetComponent<CanaryBehavior>().Launch(_rad, _visDistance, this));

    }
    public void ResetCanary()
    {
        _canLaunch = true;
    }


    public void HurtCanary()
    {
        // do something if we want
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
