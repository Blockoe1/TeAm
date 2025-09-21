using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class Damageable : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private bool updatePlayerHealth;
    [SerializeField] private Color damageFlashColor;
    [SerializeField] private float flashLerpSpeed;
    [SerializeField] private UnityEvent<int> OnDamageEvent;
    [SerializeField] private UnityEvent OnDeathEvent;

    private SpriteRenderer rend;
    private Coroutine flashRoutine;
    private int health;

    private bool isDead = false;

    public int MaxHealth => maxHealth;

    public bool IsDead { get => isDead; set => isDead = value; }

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (health <= 0) { return; }
        health -= damage;
        OnDamageEvent?.Invoke(health);

        // Play the damage flash.
        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
            flashRoutine = null;
        }
        flashRoutine = StartCoroutine(DamageFlash(damageFlashColor));

        if (updatePlayerHealth)
        {
            PlayerStatScript.RemainingHealth = health;
        }
        if (health <= 0)
        {
            isDead = true;
            //OnDeathEvent?.Invoke();
        }
    }
    public void Die()
    {
        OnDeathEvent?.Invoke();
    }

    private IEnumerator DamageFlash(Color targetColor)
    {
        Color baseColor = rend.color;
        rend.color = targetColor;

        while (Vector4.Distance(rend.color, baseColor) > 0.01)
        {
            float step = 1 - Mathf.Pow(0.5f, flashLerpSpeed * Time.deltaTime);
            rend.color = Vector4.Lerp(rend.color, baseColor, step);
            yield return null;
        }
        rend.color = baseColor;

    }
}
