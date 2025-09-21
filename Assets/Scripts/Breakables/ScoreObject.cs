using UnityEngine;

[CreateAssetMenu(fileName = "ScoreObject", menuName = "CanaryMurderer/ScoreObject")]
public class ScoreObject : ScriptableObject
{
    [SerializeField] private Sprite sprite = null;

    [SerializeField] private int scoreToAdd;

    [SerializeField] private int bitcoinToAdd;

    [SerializeField] private int hitsToBreak = 1;

    [SerializeField] private Color lightColor;

    public Sprite Sprite { get => sprite; set => sprite = value; }
    public int HitsToBreak { get => hitsToBreak; set => hitsToBreak = value; }
    public Color LightColor { get => lightColor; set => lightColor = value; }

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
