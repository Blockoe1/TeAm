using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;

public class TitleBehavior : MonoBehaviour
{
    [SerializeField] private GameObject _extraInformation;
    [SerializeField] private TMP_Text _extraInformationText;
    [SerializeField] private int _gameScene;

    private void Start()
    {
        MusicManager.Instance.TitleMusic();
    }

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(_gameScene);
    }

    public void QuitGame()
    {
        Application.Quit();
        EditorApplication.isPlaying = false;
    }

    public void ShowExtraInformation(string t)
    {
        _extraInformation.SetActive(true);
        _extraInformationText.text = t;
    }

    public void HideExtraInformation()
    {
        _extraInformation.SetActive(false);
    }
}
