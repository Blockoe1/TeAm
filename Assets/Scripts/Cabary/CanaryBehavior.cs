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

    private bool isDead = false;

    AudioManager am;

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
        //yield return new WaitForSeconds(timeBeforeLaunch);
        Vector2 force = new Vector2(Mathf.Cos(rad) * dist, Mathf.Sin(rad) * dist) * .25f * distanceModifier;
        rb2d.AddForce(force, ForceMode2D.Impulse);
        if (rb2d != null && rb2d.linearVelocityX < 0)
            GetComponent<SpriteRenderer>().flipX = true;
        yield return new WaitForSeconds(.1f);
        yield return new WaitForSeconds(timeBeforeFalling);
        if (rb2d != null && !isDead)
        {
            rb2d.gravityScale = gravityScale;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead)
        {
            return;
        }
        rb2d.linearVelocity = Vector2.zero;
        rb2d.gravityScale = 0f;
        if (!collision.gameObject.GetComponent<PlayerMovement>() && !animator.GetBool("HasKOed"))
        {
            //StartCoroutine(ReturnCanary());
            OnCanaryCrash();
        }
        else if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            pf.ResetCanary();
            // change canary sprite
            am.Play("Splat");
            Destroy(GetComponent<BoxCollider2D>());
            Destroy(gameObject);
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.layer == 9) //GAS
    //    {
    //        Debug.Log("Canary Hurt");
    //        //collision.gameObject.GetComponent<CloudBehavior>().MakeVisible();
    //        rb2d.linearVelocity = rb2d.linearVelocity*.8f;
    //        rb2d.gravityScale = gravityScale;
    //        pf.HurtCanary();

    //        animator.SetBool("HasKOed", true);
    //        StartCoroutine(DeadCountdown());
    //    }
    //}

    private void OnCanaryCrash()
    {
        animator.SetBool("HasCrashed", true);
        rb2d.linearVelocity = rb2d.linearVelocity * .8f;
        rb2d.gravityScale = gravityScale;
        StartCoroutine(CrashCountdown());
    }

    public void OnCanaryDeath()
    {
        rb2d.linearVelocity = rb2d.linearVelocity * .8f;
        rb2d.gravityScale = gravityScale;
        pf.HurtCanary();

        animator.SetBool("HasKOed", true);
        StartCoroutine(DeadCountdown());


        isDead = true;
    }

    private void OnDestroy()
    {
        pf.CanLaunch = true;
    }
    public IEnumerator ReturnCanary()
    {
        //animator.SetBool("HasCrashed", true);
        gameObject.layer = 8;
        rb2d.gravityScale = 0;
        rb2d.linearVelocity = Vector2.zero;

        //Vector3 diff = (pf.gameObject.transform.position - transform.position) * .6f;
        //if (GetComponent<Renderer>().isVisible)
        //    rb2d.AddForce(diff, ForceMode2D.Impulse);
        //else
        //    Destroy(gameObject);
        Destroy(GetComponent<Collider2D>());
        rb2d.simulated = false;

        while (Vector3.Distance(transform.position, pf.transform.position) > 1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, pf.transform.position, Time.deltaTime * returnSpeed);
            yield return null;
        }
        am.Play("Splat");
        Destroy(gameObject);
    }
    IEnumerator DeadCountdown()
    {
        while (animator.GetInteger("DeadWaitTime") > 0)
        {
            yield return new WaitForSeconds(1);
            animator.SetInteger("DeadWaitTime", animator.GetInteger("DeadWaitTime") - 1);
            //Debug.Log(animator.GetInteger("DeadWaitTime"));
        }
        StartCoroutine(ReturnCanary());
    }

    IEnumerator CrashCountdown()
    {
        while (animator.GetInteger("CrashWaitTime") > 0)
        {
            yield return new WaitForSeconds(1);
            animator.SetInteger("CrashWaitTime", animator.GetInteger("CrashWaitTime") - 1);
            //Debug.Log(animator.GetInteger("DeadWaitTime"));
        }
        StartCoroutine(ReturnCanary());
    }
}
