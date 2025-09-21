using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    private Image image;
    [SerializeField] private int fadeTime = 15;

    private void Start()
    {
        image = GetComponent<Image>();
        image.color = new(0, 0, 0, 0);

        //StartCoroutine(Fade());
    }


    public IEnumerator Fade()
    {
        float alpha = image.color.a;
        for (int i = 0; i < fadeTime; i++)
        {
            alpha += 1f / fadeTime;
            image.color = new(0, 0, 0, alpha);
            yield return new WaitForSeconds(0.05f);
        }
        image.color = Color.black;
    }
}
