using UnityEngine;

public class CanaryOverGround : MonoBehaviour
{
    [SerializeField] private ParticleSystem _canaryParticles;

    private void FixedUpdate()
    {
        if (!GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("SPIN"))
            return;
        if (Physics2D.BoxCast(transform.position, Vector2.one * 1, 0, Vector2.down, 1f, 1 << LayerMask.NameToLayer("Ground")))
            _canaryParticles.Play();
        else
            _canaryParticles.Stop();
    }
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.gameObject.layer == 3)
    //    {
    //        if (collision.bounds.Contains(GetComponent<Collider2D>().bounds.min) && collision.bounds.Contains(GetComponent<Collider2D>().bounds.max))
    //            _canaryParticles.Play();
    //    }
    //    else 
    //        _canaryParticles.Stop();
    //}
}
