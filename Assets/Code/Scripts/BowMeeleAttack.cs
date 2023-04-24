using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public class BowMeeleAttack : MonoBehaviour
{
    private Vector3 rightAttackOffset;
    private Collider2D bowCollider;
    
    private void Start()
    {
        bowCollider = GetComponent<Collider2D>();
        rightAttackOffset = transform.position;
    }
    
    public void AttackRight()
    {
        print("Attack Right!");
        bowCollider.enabled = true;
        transform.position = rightAttackOffset;
    }

    public void AttackLeft()
    {
        print("Attack left!");
        bowCollider.enabled = true;
        transform.position = new Vector3(rightAttackOffset.x * -1, rightAttackOffset.y);
    }

    public void StopAttack()
    {
        bowCollider.enabled = false;
    }
}
