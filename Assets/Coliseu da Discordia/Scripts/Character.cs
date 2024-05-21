using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class Character : MonoBehaviourPun
{
    [Header ("Life Controller")]
    protected float currentLife;
    [Tooltip("Define character's max life")]
    [SerializeField] protected float maxLife;
    protected bool dead;
   // [SerializeField] Image life; //[variável para ter acesso a imagem para controlar a barra de vida do jogador]

    [Header ("Movement Controller")]
    [SerializeField] protected float moveSpeed;
    protected Vector2 direction;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] protected float groundDistance;
    protected bool canJump;
    [SerializeField] protected float jumpForce;

    [Header("Animator Controller")]
    [Tooltip("Controla as animações do personagem")]
    [SerializeField] protected Animator anim;

    [Header("Attack Sistem")]
    [Tooltip("Controla o sistema de ataque do personagem")]
    [SerializeField] protected float attackDelay;
    [SerializeField] protected float attackSpeed;
    [SerializeField] protected float attackRange;
    [SerializeField] protected Transform posAttack;
    [SerializeField] protected LayerMask enemyLayer;
    protected float nextAttackTime;
    protected bool attacking;


    #region Unity Metods
    protected virtual void Awake()
    {
        currentLife = maxLife;
        //UpdateLifeInPhoton();
    }
    protected virtual void Update()
    {
        Animations();
    }

    protected virtual void FixedUpdate()
    {
        if (canJump) Jump(jumpForce);
    }
    #endregion


    #region Life Controller
    /*
    protected void UpdateLifeInPhoton() //Metodo criado para atualizar a barra de vida do jogador em rede
    {
        if (photonView.IsMine) photonView.RPC(nameof(UpdateLife), RpcTarget.AllBuffered, currentLife);
        // Se o photon view for meu executa o metodo de atualizar vida para todos os jogadores presentes ou ingressantes futuros [AllBuffered]
        //nameof referencia o metodo que queremos executar 

    }
    */

    [PunRPC] //Utilizado para o metodo abaixo ser chamado remotamente pelo photon
    protected void UpdateLife(float _currentLife)
    {
        currentLife = _currentLife;
        //life.fillAmount = currentLife/maxLife; //Codigo referente ao objetoda barra de vida para atualizar o parametro fillamount
            
    }

    public virtual void TakeDamage(float _value)
    {
        currentLife = Mathf.Max(currentLife - _value, 0);
        //UpdateLifeInPhoton();
        if (currentLife == 0) Death(); 
    }

    public virtual void Heal(float _value) 
    { 
        currentLife = Mathf.Min(currentLife + _value, maxLife);
        //UpdateLifeInPhoton();
    }

    protected virtual void Death()
    {
        dead = true;
    }
    #endregion

    #region Movement Controller
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
    #endregion

    #region Animations
    protected virtual void Animations()
    {
        anim.SetBool("Dead", dead);
        anim.SetBool("OnGround", OnGround());
    }
    #endregion

}
