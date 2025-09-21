using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private static PlayerAnimation instance;

    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerMine _playerMine;
    [SerializeField] private PlayerFiring _playerFiring;
    [SerializeField] private Damageable _playerDamage;
    [SerializeField] private Animator _playerAnimator;

    public static PlayerAnimation Instance { get => instance; set => instance = value; }

    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        UpdateAnimationParameters();
    }
    public void FlipSprite()
    {
        if (_playerMovement.MoveDirection < 0 )
            GetComponent<SpriteRenderer>().flipX = true;
        else if (_playerMovement.MoveDirection > 0 )
            GetComponent<SpriteRenderer>().flipX = false;
    }
    public void UpdateAnimationParameters()
    {
        _playerAnimator.SetBool("IsMoving", _playerMovement.IsMoving);
        _playerAnimator.SetBool("IsJumping", _playerMovement.IsJumping);
        _playerAnimator.SetBool("IsOnGround", _playerMovement.IsOnGround());
        _playerAnimator.SetBool("IsMining", _playerMine.IsMining);
        _playerAnimator.SetBool("IsSpitting", _playerFiring.IsSpitting);
        _playerAnimator.SetBool("IsDead", _playerDamage.IsDead);
    }
    public void EndMining()
    {
        _playerMine.IsMining = false;
    }
    public void EndSpit()
    {
        _playerFiring.IsSpitting = false;
    }
}
