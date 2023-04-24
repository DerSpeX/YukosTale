using System;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 10f; // Geschwindigkeit des Pfeils
    public Rigidbody2D rb; // Rigidbody2D-Komponente des Pfeils
    public GameObject impactEffect; // Referenz auf ein Effekt-Objekt, das abgespielt wird, wenn der Pfeil einschlägt
    public float projectileLifespan;// Zeit bis sich der Pfeil ohne vorige Kollision von alleine zerstört

    private void Update()
    {
        Destroy(this.gameObject, projectileLifespan);
    }

    // Pfeil wird beim Start in eine Richtung geschossen
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    // Wenn der Pfeil auf ein Objekt trifft, wird der Aufprall-Effekt abgespielt und der Pfeil wird zerstört
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.tag == "Enemy") // Wenn der Pfeil ein Objekt mit dem Tag "Enemy" trifft
        {
            //Instantiate(impactEffect, transform.position, transform.rotation); // Erzeuge den Aufprall-Effekt an der Position des Pfeils
            Destroy(gameObject); // Zerstöre den Pfeil
        }
        print("TRIGGER"+hitInfo.gameObject.ToString());
    }

    private void OnCollisionEnter2D(BoxCollider2D col)
    {
        print("COL"+col.gameObject.ToString());
    }
}

