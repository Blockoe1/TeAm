using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    [SerializeField] private ScoreObject score;
    [SerializeField] private GameObject particles;
    [SerializeField] private Sprite[] breakStates = new Sprite[3];
    [SerializeField] private SpriteRenderer breakRenderer;

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
            float percent = hits/(float)score.HitsToBreak;
            if(percent > .5f && percent < .8f)
            {
                breakRenderer.sprite = breakStates[0];
            }
            else if (percent >= .3f)
            {
                breakRenderer.sprite = breakStates[1];
            }
            else if(percent < .3f)
            {
                breakRenderer.sprite = breakStates[2];
            }
            return;
        }

        if (score != null)
        {
            score.AddScore();
        }

        if (score.name.ToLower().Contains("bit"))
            FindFirstObjectByType<AudioManager>().Play("Coin");
        else if (score.name.ToLower().Contains("wall"))
            Debug.Log("");
        else
            FindFirstObjectByType<AudioManager>().Play("Bitcoin");
        Destroy(gameObject);
    }
}
