using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private GameObject _winCanvas;
    [SerializeField] private GameObject _loseCanvas;
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
