using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

namespace Mystimor
{


    public class Character : MonoBehaviourPun
    {
        [Header ("Life Controller")]
        protected float currentLife;

        [Tooltip("Define character's max life")]
        [SerializeField] protected float maxLife;
        protected bool dead;
       // [SerializeField] Image life; //[vari�vel para ter acesso a imagem para controlar a barra de vida do jogador]

        [Header ("Movement Controller")]
        [Tooltip("Define a Velocidade de Movimento do jogador")]
        [SerializeField] protected float moveSpeed;
        protected Vector2 direction;
        [Tooltip("Define o ponto de detec��o do ch�o do jogador")]
        [SerializeField] protected Transform groundCheck;
        [Tooltip("Cria a verifica��o da camada para o ch�o ser detectado ")]
        [SerializeField] protected LayerMask groundLayer;
        [Tooltip("Define a dist�ncia da verifica��o da camada do ch�o ser detectado e os p�s do Personagens")]
        [SerializeField] protected float groundDistance;
        protected bool canJump;
        [Tooltip("Define a for�a do pulo")]
        [SerializeField] protected float jumpForce;

        [Header("Animator Controller")]
        [Tooltip("Controla as anima��es do personagem")]
        [SerializeField] protected Animator anim;

        [Header("Attack Sistem")]
        [Tooltip("Controla o intervalo entre os ataques do personagem")]
        [SerializeField] protected float attackDelay;
        [Tooltip("Define a velocidade do golpe")]
        [SerializeField] protected float attackSpeed;
        [Tooltip("Define a dist�ncia do golpe")]
        [SerializeField] protected float attackRange;
        [Tooltip("Define a valor do dano")]
        [SerializeField] protected float attackDamage;
        [Tooltip("Define a localiza��o do ponto de contato para validar o contato para aplicar o dano")]
        [SerializeField] protected Transform posAttack;
        [Tooltip("Cria uma camada inimigo para o mesmo ser detectado pelo ponto de contato")]
        [SerializeField] protected LayerMask enemyLayer;
        protected float nextAttackTime;
        protected bool attacking;


        #region Unity Metods
        protected virtual void Awake()
        {
            //Define a vida atual como a vida maxima do jogador
            currentLife = maxLife;
            
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
    
        protected void UpdateLifeInPhoton() //Metodo criado para atualizar a barra de vida do jogador em rede
        {
            if (photonView.IsMine) photonView.RPC(nameof(UpdateLife), RpcTarget.AllBuffered, currentLife);
            // Se o photon view for meu executa o metodo de atualizar vida para todos os jogadores presentes ou ingressantes futuros [AllBuffered]
            //nameof referencia o metodo que queremos executar 

        }
    

        [PunRPC] //Utilizado para o metodo abaixo ser chamado remotamente pelo photon
        protected void UpdateLife(float _currentLife)
        {
            currentLife = _currentLife;
            //life.fillAmount = currentLife/maxLife; //Codigo referente ao objetoda barra de vida para atualizar o parametro fillamount
            
        }

        public virtual void TakeDamage(float _value)
        {
            Debug.Log("TakeDamage");
            currentLife = Mathf.Max(currentLife - _value, 0);
            //UpdateLifeInPhoton();

            if (currentLife == 0 && photonView.IsMine)
            {
                photonView.RPC(nameof(Death), RpcTarget.AllBuffered, true);
            } 
        }

        public virtual void Heal(float _value) 
        { 
            currentLife = Mathf.Min(currentLife + _value, maxLife);
            //UpdateLifeInPhoton();
        }

        [PunRPC]
        protected virtual void Death(bool _value)
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

}