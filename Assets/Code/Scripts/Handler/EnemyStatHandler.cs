using System.Collections;
using TMPro;
using UnityEngine;

public class EnemyStatHandler : MonoBehaviour
{
    public float minHealth;
    public float maxHealth;
    public float currentHealth;
    public float defense;
    public TMP_Text hpText;

    private void Start()
    {
        currentHealth = maxHealth;
    }
    private void Update()
    {
        hpText.text = currentHealth.ToString("F1") + "/" + maxHealth.ToString("F1");
        if(currentHealth > 0.74f * maxHealth)
        {
           hpText.color = Color.green;
        }
        else if (currentHealth >= 0.50f * maxHealth && currentHealth <= 0.74f * maxHealth)
        {
            hpText.color = Color.yellow;
        }
        else if (currentHealth < 0.50f * maxHealth)
        {
            hpText.color = Color.red;
        }

    }
    public void TakeInstantDamage(float damage, float penetration)
    {
        if(defense > penetration)
        {
            //
        }
        else if(defense < penetration)
        {
            currentHealth -= damage;
            if(currentHealth <= 0f)
            {
                Die();
            }
        }
    }

    public void TakeDamageOverTime(float damage, float penetration, float duration)
    {

        if (penetration >= defense)
        {
            StartCoroutine(DamageOverTime(damage, duration));
        }
        else if (penetration < defense)
        {
            // No Penetration = No Damage... Bruh
        }
    }

    public IEnumerator DamageOverTime(float damage, float duration)
    {
        float timer = 0f;
        float damageAmountPerSecond = damage / duration;
        while (timer < duration)
        {
            if (currentHealth - damageAmountPerSecond > minHealth)
            {
                float amountToReduce = damageAmountPerSecond * Time.deltaTime;
                currentHealth -= amountToReduce;

                timer += Time.deltaTime;
            }
            else if (currentHealth - damageAmountPerSecond <= minHealth)
            {
                currentHealth = minHealth;
                Die();
            }
            yield return null;
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
