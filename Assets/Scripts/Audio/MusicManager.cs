using Unity.VisualScripting;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [SerializeField] private AudioClip titleMusic;
    [SerializeField] private AudioClip deadMusic;
    private AudioSource musicSource;
    private void Start()
    {
        if(Instance == null)
        {
            musicSource = GetComponent<AudioSource>();
            Instance = this;
            DontDestroyOnLoad(gameObject);

            return;
        }

        Destroy(gameObject);
    }

    public void TitleMusic()
    {
        musicSource.clip = titleMusic;
        musicSource.Play();
    }

    public void DeadMusic()
    {
        musicSource.clip = deadMusic;
        musicSource.Play();
    }
}
