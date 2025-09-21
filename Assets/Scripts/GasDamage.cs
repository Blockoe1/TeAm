using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class GasDamage : MonoBehaviour
{
    [SerializeField] private float shownAlpha = 0.25f;
    [SerializeField] private float alphaLerpSpeed = 0.5f;
    [SerializeField] private float alphaCutoff = 0.05f;
    [SerializeField] private float damageCooldown = 0.5f;

    private Tilemap tilemap;

    private Coroutine showRoutine;
    private readonly List<Damageable> targets = new();
    private bool hitCooldown;

    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
        tilemap.color = SetAlpha(tilemap.color, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Damageable damageable))
        {
            targets.Add(damageable);
            if(!hitCooldown)
            {
                DealDamage();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Damageable damageable) && targets.Contains(damageable))
        {
            targets.Remove(damageable);
        }
    }

    private void DealDamage()
    {
        if (targets.Count > 0)
        {
            foreach (Damageable damageable in targets)
            {
                damageable.TakeDamage(1);
            }

            if (showRoutine != null)
            {
                StopCoroutine(showRoutine);
                showRoutine = null;
            }
            showRoutine = StartCoroutine(ShowRoutine(shownAlpha));

            StartCoroutine(DamageCooldown(damageCooldown));
        }
    }

    private IEnumerator ShowRoutine(float targetAlpha)
    {
        tilemap.color = SetAlpha(tilemap.color, shownAlpha);

        while (Mathf.Abs(tilemap.color.a) > alphaCutoff)
        {
            float step = 1 - Mathf.Pow(0.5f, alphaLerpSpeed * Time.deltaTime);
            float targetA = Mathf.Lerp(tilemap.color.a, 0, step);
            //Debug.Log(step);
            tilemap.color = SetAlpha(tilemap.color, targetA);
            yield return null;
        }
        tilemap.color = SetAlpha(tilemap.color, 0);

    }

    private static Color SetAlpha(Color col, float a)
    {
        col.a = a;
        return col;
    }

    private IEnumerator DamageCooldown(float duration)
    {
        hitCooldown = true;
        yield return new WaitForSeconds(duration);
        hitCooldown = false;
        DealDamage();
    }
}
