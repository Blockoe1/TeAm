using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    [SerializeField] private ScoreObject score;
    [SerializeField] private GameObject particles;

    private int hits = 0;
    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = score.Sprite;
        hits = score.HitsToBreak;
    }


    private void OnValidate()
    {
        GetComponent<SpriteRenderer>().sprite = score.Sprite;
    }

    public void BreakObject()
    {
        Instantiate(particles, transform.position, transform.rotation);
        if(hits > 0)
        {
            hits--;
            return;
        }

        if (score != null)
        {
            score.AddScore();
        }
        Destroy(gameObject);
    }
}
