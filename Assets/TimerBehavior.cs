using System.Collections;
using UnityEngine;

public class TimerBehavior : MonoBehaviour
{
    [SerializeField] private float initialSpeed;
    [SerializeField] private float timeBeforeSpeedup = 140f;
    [SerializeField] private int stepsPerSecond;
    [SerializeField] private float speedAfterSpeedup;
    Rigidbody2D rb2d;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        StartCoroutine(Gas());
    }

    private IEnumerator Gas()
    {
        for(int i=0; i<stepsPerSecond*timeBeforeSpeedup; i++)
        {
            rb2d.linearVelocity = new Vector2(0,-1) * initialSpeed;
            yield return new WaitForSeconds(1/stepsPerSecond);
        }
        //Debug.LogWarning("Switched");
        while(true)
        {
            rb2d.linearVelocity = new Vector2(0, -1) * speedAfterSpeedup;
            yield return new WaitForSeconds(1 / stepsPerSecond);
        }
    }
}
