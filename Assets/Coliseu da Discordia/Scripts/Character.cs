using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header ("Life Controller")]
    protected float currentLife;
    [Tooltip("Define character's max life")]
    [SerializeField] protected float maxLife;
    protected bool dead;

    [Header ("Movement Controller")]
    [SerializeField] protected float moveSpeed;
    protected Vector2 direction;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] protected float groundDistance;
    protected bool canJump;
    [SerializeField] protected float jumpForce;

    #region Unity Metods
    protected virtual void Awake()
    {
        currentLife = maxLife;
    }
    protected virtual void Update()
    {
        
    }

    protected virtual void fixedUpdate()
    {
        if (canJump) Jump(jumpForce);
    }
    #endregion

    public virtual void TakeDamage(float _value)
    {
        currentLife = Mathf.Max(currentLife - _value, 0);

        if (currentLife == 0) Death(); 
    }

    public virtual void Heal(float _value) 
    { 
        currentLife = Mathf.Min(currentLife + _value, maxLife);
    }

    protected virtual void Death()
    {
        dead = true;
    }

    protected virtual void Move()
    {

    }

    public virtual void Jump(float _value) 
    {
        canJump = false;
    }
    protected virtual bool OnGround() 
    {
        return Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);
    }


}
