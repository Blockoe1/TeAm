using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class CanaryBehavior : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb2d;
    private PlayerFiring pf;
    [SerializeField] private float timeBeforeFalling = .2f;
    [SerializeField] private float distanceModifier = 1.0f;
    [SerializeField] private float timeBeforeLaunch = .5f;
    [SerializeField] private float gravityScale = .5f;
    [SerializeField] private float returnSpeed = 0.6f;

    AudioManager am;
    private bool canCollide = false;
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        am = FindFirstObjectByType<AudioManager>();
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Launches the Canary after a period of time
    /// </summary>
    /// <param name="rad">Angle of launching, in radians</param>
    /// <param name="dist">Force Multiplier</param>
    /// <param name="pRef">Player Reference</param>
    /// <returns></returns>
    public IEnumerator Launch(float rad, float dist, PlayerFiring pRef)
    {
        if(rb2d == null)
            rb2d= GetComponent<Rigidbody2D>();
        rb2d.gravityScale = 0f;
        if(pf == null)
            pf = pRef;

        rb2d.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(timeBeforeLaunch);
        Vector2 force = new Vector2(pf.LaunchPos.x + Mathf.Cos(rad) * dist, pf.LaunchPos.y + Mathf.Sin(rad) * dist) * .05f * distanceModifier;
        rb2d.AddForce(force, ForceMode2D.Impulse);
        yield return new WaitForSeconds(.1f);
        canCollide = true;
        yield return new WaitForSeconds(timeBeforeFalling);
        if(rb2d!=null)
            rb2d.gravityScale = gravityScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (canCollide)
        {
            rb2d.linearVelocity = Vector2.zero;
            rb2d.gravityScale = 0f;
            if (!collision.gameObject.GetComponent<PlayerMovement>() && !animator.GetBool("HasKOed"))
            {
                ReturnCanary();
            }
            else if (collision.gameObject.GetComponent<PlayerMovement>())
            {
                pf.ResetCanary();
                // change canary sprite
                am.Play("Splat");
                Destroy(GetComponent<Collider>());
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9) //GAS
        {
            Debug.Log("Canary Hurt");
            collision.gameObject.GetComponent<CloudBehavior>().MakeVisible();
            rb2d.linearVelocity = rb2d.linearVelocity*.8f;
            rb2d.gravityScale = gravityScale;
            pf.HurtCanary();

            animator.SetBool("HasKOed", true);
            StartCoroutine(DeadCountdown());
        }
    }
    private void OnDestroy()
    {
        pf.CanLaunch = true;
    }
    public void ReturnCanary()
    {
        Debug.Log("Return");
        gameObject.layer = 8;

        Vector3 diff = (pf.gameObject.transform.position - transform.position) * .6f;
        if (GetComponent<Renderer>().isVisible)
            rb2d.AddForce(diff, ForceMode2D.Impulse);
        else
            Destroy(gameObject);

        animator.SetBool("HasCrashed", true);

    }
    IEnumerator DeadCountdown()
    {
        while (animator.GetInteger("DeadWaitTime") > 0)
        {
            yield return new WaitForSeconds(1);
            animator.SetInteger("DeadWaitTime", animator.GetInteger("DeadWaitTime") - 1);
            Debug.Log(animator.GetInteger("DeadWaitTime"));
        }
        ReturnCanary();
    }
}
