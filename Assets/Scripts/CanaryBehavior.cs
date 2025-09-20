using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class CanaryBehavior : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private PlayerFiring pf;
    [SerializeField] private float timeBeforeFalling = .2f;
    [SerializeField] private float distanceModifier = 1.0f;
    [SerializeField] private float timeBeforeLaunch = .5f;
    [SerializeField] private float gravityScale = .5f;


    private bool canCollide = false;
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
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
            Debug.Log(collision.gameObject.name);
            rb2d.linearVelocity = Vector2.zero;
            rb2d.gravityScale = 0f;
            if (!collision.gameObject.GetComponent<PlayerMovement>())
            {
                ReturnCanary();
            }
            else
            {
                pf.ResetCanary();
                Destroy(gameObject);
            }
        }
    }

    private void ReturnCanary()
    {
        gameObject.layer = 8;

        Vector3 diff = pf.gameObject.transform.position - transform.position;
        rb2d.AddForce(diff, ForceMode2D.Impulse);
    }
}
