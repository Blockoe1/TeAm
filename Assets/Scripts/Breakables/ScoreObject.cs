using UnityEngine;

[CreateAssetMenu(fileName = "ScoreObject", menuName = "CanaryMurderer/ScoreObject")]
public class ScoreObject : ScriptableObject
{
    [SerializeField] private Sprite sprite = null;

    [SerializeField] private int scoreToAdd;

    [SerializeField] private int bitcoinToAdd;

    [SerializeField] private int hitsToBreak = 1;

    public Sprite Sprite { get => sprite; set => sprite = value; }
    public int HitsToBreak { get => hitsToBreak; set => hitsToBreak = value; }

    public void AddScore()
    {
        ScoreScript.Score += scoreToAdd;
        if (bitcoinToAdd > 0)
        {
            ScoreScript.Bitcoin += bitcoinToAdd;
            FindFirstObjectByType<PlayerMine>().CollectBitCoin();
        }
    }
}
