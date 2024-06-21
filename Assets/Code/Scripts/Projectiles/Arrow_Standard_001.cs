using System;
using System.ComponentModel;
using UnityEngine;

public class Arrow_Standard_001 : MonoBehaviour
{
    #region Variables
    [Header("Arrow Projectile Properties")]
    [Description("Speed of the Arrow projectile")]
    [SerializeField] private float arrowSpeed = 10f; // Geschwindigkeit des Pfeils
    [Description("Amount of Time before the Arrow Projectile destroy itself, when it dont collide an Target")]
    [SerializeField] private float projectileLifespan;// Zeit bis sich der Pfeil ohne vorige Kollision von alleine zerstört
    [Description("Rigidbody2d Component of Arrow projectile")]
    [SerializeField] private Rigidbody2D rb; // Rigidbody2D-Komponente des Pfeils
    [Description("Game Object for Collision Effect when hitting the Target")]
    [SerializeField] private GameObject impactEffect; // Referenz auf ein Effekt-Objekt, das abgespielt wird, wenn der Pfeil einschlägt


    public float damage;
    public float penetration;
    public float duration;

    [SerializeField] private Operation operation;
    [SerializeField]
    private enum Operation
    {
        InstantDamage,
        DamageOverTime
    }


    private CharacterStatHandler _characterCharacterStatHandler;


    #endregion
    #region Unity Functions
    // Pfeil wird beim Start in eine Richtung geschossen
    void Start()
    {
        _characterCharacterStatHandler = FindObjectOfType<CharacterStatHandler>();
        damage = _characterCharacterStatHandler.combatStatistics.currentPhysicalDamage; //Vereinfacht
        penetration = _characterCharacterStatHandler.combatStatistics.currentPhysicalPenetration; //Vereinfacht

        rb.velocity = transform.right * arrowSpeed;
    }
    private void Update()
    {
        Destroy(this.gameObject, projectileLifespan);
    }
    // Wenn der Pfeil auf ein Objekt trifft, wird der Aufprall-Effekt abgespielt und der Pfeil wird zerstört
    void OnTriggerEnter2D(Collider2D collison)
    {
        if (collison.gameObject.tag == "Enemy") // Wenn der Pfeil ein Objekt mit dem Tag "Enemy" trifft
        {
            GameObject impactEffectGO = Instantiate(impactEffect, collison.gameObject.transform.localPosition, collison.gameObject.transform.localRotation); // Erzeuge den Aufprall-Effekt an der Position des Pfeils
            EnemyStatHandler enemyStatHandler = collison.GetComponent<EnemyStatHandler>();

            switch (operation)
            {
                case Operation.InstantDamage:
                {
                        if (enemyStatHandler != null)
                        {
                            enemyStatHandler.TakeInstantDamage(damage, penetration);
                            Destroy(impactEffectGO, 1f);
                            Destroy(gameObject); // Zerstöre den Pfeil
                        }
                        break;
                }

                case Operation.DamageOverTime:
                {
                        if (enemyStatHandler != null)
                        {
                            enemyStatHandler.TakeDamageOverTime(damage, penetration, duration);
                            Destroy(impactEffectGO, 3f);
                            Destroy(gameObject); // Zerstöre den Pfeil
                        }
                        break;
                }
            }
            
            //Destroy(impactEffectGO, 1f);
            //Destroy(gameObject); // Zerstöre den Pfeil
        }
    }

    #endregion
}

