using UnityEngine;

public class CloudBehavior : MonoBehaviour
{
    [SerializeField] float newAlpha;
    public void MakeVisible()
    {
        Color c = GetComponent<SpriteRenderer>().color;
        c.a = newAlpha;
        GetComponent<SpriteRenderer>().color = c;
    }
}
