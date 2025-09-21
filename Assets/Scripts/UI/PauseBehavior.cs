using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseBehavior : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuObject;
    [SerializeField] private GameObject extraInformation;
    [SerializeField] private TMP_Text extraInformationText;

    private InputAction pauseAction;
    private static bool isPaused;

    public static bool IsPaused
    {
        get { return isPaused; }
    }

    private void Awake()
    {
        if (TryGetComponent(out PlayerInput input))
        {
            pauseAction = input.currentActionMap.FindAction("Pause");

            pauseAction.performed += PauseAction_Performed;
        }
    }
    private void OnDestroy()
    {
        pauseAction.performed -= PauseAction_Performed;
    }

    private void PauseAction_Performed(InputAction.CallbackContext obj)
    {
        TogglePauseMenu(!isPaused);
    }

    public void TogglePauseMenu(bool isPaused)
    {
        PauseBehavior.isPaused = isPaused;
        pauseMenuObject.SetActive(isPaused);
        if (isPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void ShowExtraInformation(string t)
    {
        extraInformation.SetActive(true);
        extraInformationText.text = t;
    }

    public void HideExtraInformation()
    {
        extraInformation.SetActive(false);
    }

    public void OnQuitPressed()
    {
        ScoreScript.Score = 0;
        SceneManager.LoadScene("Title");
    }
}
