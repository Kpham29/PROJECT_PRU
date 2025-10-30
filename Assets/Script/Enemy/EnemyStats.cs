using System.Collections;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] protected float health;
    [SerializeField] protected bool playDefaultSounds = true;


    [Header("Flash")]
    [SerializeField] private float flashDuration;
    [SerializeField, Range(0, 1)] private float flashStrength;
    [SerializeField] private Color flashCol;
    [SerializeField] private Material flashMaterial;
    private Material defaultMaterial;
    [SerializeField] private SpriteRenderer spriter;

    protected Coroutine damageCoroutine;

    private void Start()
    {
        defaultMaterial = spriter.material;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        DamageProcess();
        if (damageCoroutine != null)
            StopCoroutine(Flash());
        damageCoroutine = StartCoroutine(Flash());

        
        // Play hurt sound (only if default sounds enabled)
        if (playDefaultSounds && AudioManager.Instance != null)
            AudioManager.Instance.PlayEnemyHurt();
            
        if (health <= 0)
        {
            DeathProcess();
            
            // Play death sound (only if default sounds enabled)
            if (playDefaultSounds && AudioManager.Instance != null)
                AudioManager.Instance.PlayEnemyDeath();

        if (health <= 0)
        {
            DeathProcess();

        }
    }

    protected virtual void DamageProcess() { }

    protected virtual void DeathProcess() { }

    private IEnumerator Flash()
    {
        spriter.material = flashMaterial;
        flashMaterial.SetColor("_FlashColor", flashCol);
        flashMaterial.SetFloat("_FlashAmount", flashStrength);
        yield return new WaitForSeconds(flashDuration);
        spriter.material = defaultMaterial;
        damageCoroutine = null;
    }


}
