using System.Collections;
using UnityEngine;

public class CanaryBehavior : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private PlayerFiring pf;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public IEnumerator Launch(float rad, float dist, PlayerFiring pRef)
    {
        if(pf == null)
            pf = pRef;

        rb2d.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(.5f);
        Vector2 force = new Vector2(pf.LaunchPos.x + Mathf.Cos(rad) * dist, pf.LaunchPos.y + Mathf.Sin(rad) * dist) * .5f;
        rb2d.AddForce(force, ForceMode2D.Impulse);
    }
}
