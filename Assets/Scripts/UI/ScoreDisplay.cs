using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    private TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
        ScoreScript.OnScoreChanged += UpdateScoreText;
    }

    private void OnDestroy()
    {
        ScoreScript.OnScoreChanged -= UpdateScoreText;
    }

    private void UpdateScoreText(int score)
    {
        text.text = "Score: " + score.ToString();
    }
}
