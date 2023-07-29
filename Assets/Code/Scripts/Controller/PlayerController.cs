using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region Variables
    
    [SerializeField] private float movementSpeed;
    [SerializeField] private ContactFilter2D movementFilter;
    [SerializeField] private float collisionOffset;

    public GameObject arrowPrefab;
    public GameObject arrowPrefab2;
    public Transform firePoint;

    private Vector2 movementInput;
    private Rigidbody2D rb;
    private Animator animator;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    private bool canMove = true;
    private bool wasInteractPressed;
    
    
    #endregion
    #region Unity Functions
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        if (canMove)
        {
            if (movementInput != Vector2.zero)
            {
                bool success = TryMove(movementInput);

                if (!success)
                {
                    success = TryMove(new Vector2(movementInput.x, 0));

                    if (!success)
                    {
                        success = TryMove(new Vector2(0, movementInput.y));
                    }
                }
                animator.SetBool("isMoving", success);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }

            if (movementInput.x < 0)
            {
                //firePoint.localPosition = (new Vector3(-1.25f, -1.5f, 0));
                //firePoint.localRotation = (new Quaternion(0, -180, 0, 1));
                animator.SetBool("isMovingLeft", true);
                animator.SetInteger("LastDirection", 4);
            }
            else if (movementInput.x > 0)
            {
                //firePoint.localPosition = (new Vector3(1.25f, -1.5f, 0));
                //firePoint.localRotation = (new Quaternion(0, 0, 0, 1));
                animator.SetBool("isMovingRight", true);
                animator.SetInteger("LastDirection", 2);
            }
            else
            {
                animator.SetBool("isMovingLeft", false);
                animator.SetBool("isMovingRight", false);
            }

            if (movementInput.y < 0)
            {
                animator.SetBool("isMovingDown", true);
                animator.SetInteger("LastDirection", 3);
            }
            else if (movementInput.y > 0)
            {
                animator.SetBool("isMovingUp", true);
                animator.SetInteger("LastDirection", 1);
            }
            else
            {
                animator.SetBool("isMovingUp", false);
                animator.SetBool("isMovingDown", false);
            }
            
            // Rotation des Feuerpunkts, um in Richtung Mauszeiger zu zielen
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector2 direction = (mousePos - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            firePoint.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            OnRightFire();
        }
    }

    #endregion
    #region Custom Functions
    //Check some Conditions if player can move or not
    private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            int count = rb.Cast(movementInput, movementFilter, castCollisions,
                movementSpeed * Time.fixedDeltaTime + collisionOffset);

            if (count == 0)
            {
                rb.MovePosition(rb.position + movementInput * movementSpeed * Time.fixedDeltaTime);
                return true;
            }
            else
            {
                return false; 
            }
        }
        else
        {
            return false; 
        }
    }
    //OnMove == Trigger for new Input System // Moves the Player along the Inputs
    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }
    //OnFire == Trigger for new Input System // Let the Player use The Weapon
    void OnFire()
    {
        animator.SetTrigger("BowRangeAttack");
        Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
    }

    void OnRightFire() //Test kommt wieder weg!
    {
        animator.SetTrigger("BowRangeAttack");
        Instantiate(arrowPrefab2, firePoint.position, firePoint.rotation);
    }
    //Lock the Players Movement
    public void LockMovement()
    {
        canMove = false;
    }
    //Unlock the Players Movement
    public void UnlockMovement()
    {
        canMove = true;
    }
    
    #endregion
}