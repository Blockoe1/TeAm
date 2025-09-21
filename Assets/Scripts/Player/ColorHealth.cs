using UnityEngine;

public class ColorHealth : MonoBehaviour
{
    [SerializeField] private Color deadColor = Color.green;

    private SpriteRenderer rend;
    private Color baseColor;
    private float maxHealth;

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        baseColor = rend.color;
        maxHealth = GetComponent<Damageable>().MaxHealth;
    }


    public void SetColors(int currentHealth)
    {
        float normalizedHealth = 1 - (float)currentHealth / maxHealth;
        rend.color = Color.Lerp(baseColor, deadColor, normalizedHealth);
    }
}
