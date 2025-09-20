using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    [SerializeField] private ScoreObject score;

    private int hits = 0;
    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = score.Sprite;
        hits = score.HitsToBreak;
    }

    public void BreakObject()
    {
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
