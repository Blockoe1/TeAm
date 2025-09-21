using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private bool updatePlayerHealth;
    [SerializeField] private UnityEvent<int> OnDamageEvent;
    [SerializeField] private UnityEvent OnDeathEvent;

    private int health;

    private void Awake()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        OnDamageEvent?.Invoke(health);
        if (updatePlayerHealth)
        {
            PlayerStatScript.RemainingHealth = health;
        }
        if (health <= 0)
        {
            OnDeathEvent?.Invoke();
        }
    }
}
