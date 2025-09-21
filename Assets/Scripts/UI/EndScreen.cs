using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private GameObject _winCanvas;
    [SerializeField] private GameObject _loseCanvas;

    [SerializeField] private TMP_Text _scoreText;
    private void Start()
    {
        switch (StaticData.EndID)
        {
            case 0:
                _loseCanvas.SetActive(true);
                break;
            case 1:
                _winCanvas.SetActive(true);
                break;
        }
        UpdateScoreText();
    }
    private void UpdateScoreText()
    {
        _scoreText.text = "Score = " + ScoreScript.score;
        if (ScoreScript.Bitcoin > 0)
            _scoreText.text += " + " + ScoreScript.Bitcoin + " Bitcoin";
    }
    public void Button_TitleScreen()
    {
        SceneManager.LoadScene("Title");
    }
    public void Button_Quit()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }


}
