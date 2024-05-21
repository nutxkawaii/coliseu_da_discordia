using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Player : Character
{
    
    protected Rigidbody rb;

    protected float rotY;
    [SerializeField] protected float rotSpeed;

    protected override void Awake()
    {
       base.Awake(); 
       rb = GetComponent<Rigidbody>();
       if(photonView.IsMine) Camera.main.gameObject.SetActive(false);
    }
    protected override void Update()
    {
        base.Update();

        if(!photonView.IsMine) return; //Ismine para verificar se o controle não for do meu jogador, não aceita os inputs e Rotação

        PlayerInputs();
        RotatePlayer();
        
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        Move();
    }
    protected override void Move()
    {
        base.Move();

        Vector3 _velocity = (transform.right * direction.x + transform.forward * direction.y) * moveSpeed;
        _velocity.y = rb.velocity.y;
        rb.velocity = _velocity;

    }

    protected virtual void PlayerInputs()
    {
        
        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");

        rotY = Input.GetAxisRaw("Mouse X");

        if(Input.GetButtonDown("Jump") && OnGround()) canJump = true;
    }


    protected virtual void RotatePlayer()
    {
        transform.Rotate(0f, rotY * rotSpeed * Time.deltaTime, 0f);
    } 

    public override void Jump(float _value)
    {
        base.Jump(_value);

        Vector3 _velocity = rb.velocity;
        _velocity.y = 0f;

        rb.velocity = _velocity;

        rb.AddForce(Vector3.up * _value);
    }

    protected override void Animations()
    {
        base.Animations();

        anim.SetFloat("SpeedX", direction.x);
        anim.SetFloat("SpeedZ", direction.y);
    }
}
