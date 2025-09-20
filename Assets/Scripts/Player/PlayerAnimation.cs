using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Animator _playerAnimator;

    public void FlipSprite()
    {
        if (_playerMovement.MoveDirection < 0 )
            GetComponent<SpriteRenderer>().flipX = true;
        else if (_playerMovement.MoveDirection > 0 )
            GetComponent<SpriteRenderer>().flipX = false;
    }
    public void PlayOnGroundAnimation()
    {
        PlayIdle();
        PlayRun();
    }
    public void PlayIdle()
    {
        if (_playerMovement.IsOnGround() && !_playerMovement.IsMoving && !_playerMovement.IsJumping)
            _playerAnimator.Play("IDLE");
    }
    public void PlayRun()
    {
        if (_playerMovement.IsOnGround() && _playerMovement.IsMoving && !_playerMovement.IsJumping)
            _playerAnimator.Play("RUN");
    }
    public void PlayJump()
    {
        if (!_playerMovement.IsJumping)
            return;
        _playerAnimator.Play("JUMP");
    }
    public void PlayAirtime()
    {
        if (_playerMovement.IsOnGround())
            return;
        _playerAnimator.Play("AIRTIME");
    }
    public void PlayMine()
    {
        _playerAnimator.Play("MINE");
    }
    public void PlaySpit()
    {
        _playerAnimator.Play("SPIT");
    }
}
