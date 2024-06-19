using Mystimor;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mystimor
{
    

    [RequireComponent(typeof(Rigidbody))]

    public class Player : Character
    {

        Player photonPlayer;

        protected Rigidbody rb;

        protected float rotY;
        [SerializeField] protected float rotSpeed;

        [Header ("Variáveis da lista de tarefas")]
        private TaskUI taskUI; // -- Variável do canvas de tarefas --
        private List<TaskBase> tasks = new List<TaskBase>(); // lista de tarefas
    
        #region Unity Metods
        protected override void Awake()
        {
           base.Awake(); 
           rb = GetComponent<Rigidbody>();
           if(photonView.IsMine) Camera.main.gameObject.SetActive(false);
      
        }
        protected void Start()
        {
            GetTasks();
            taskUI = FindAnyObjectByType<TaskUI>();
        }
        protected override void Update()
        {
            base.Update();

            if (dead)
            {
                direction.x = 0;
                direction.y = 0;
                return;
            }

            if(!photonView.IsMine) return; //Ismine para verificar se o controle não for do meu jogador, não aceita os inputs e Rotação

            PlayerInputs();
            RotatePlayer();
        
        }
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            Move();
        }
        #endregion


        #region  Tasks Sistem
        protected void GetTasks()
        {
            tasks = TaskService.Instance.GetTasks(true);
        }

        private void OnTriggerEnter(Collider collision)
        {
            if(collision.GetComponent<TaskBase>())
            {
               

                taskUI.SetPlayerCurrentTask(collision.GetComponent<TaskBase>(), tasks.FindIndex(a => a == collision.GetComponent<TaskBase>()));
            }
        }

        private void OnTriggerExit(Collider collision)
        {
            if (collision.GetComponent<TaskBase>())
            {
                if(taskUI.CurrentTaskInRange == collision.GetComponent<TaskBase>())
                {
                    taskUI.SetPlayerCurrentTask(null, 0);
                }
            }
        }
        #endregion



        protected virtual void PlayerInputs()
        {
                
            direction.x = Input.GetAxis("Horizontal");
            direction.y = Input.GetAxis("Vertical");

            rotY = Input.GetAxisRaw("Mouse X");

            if(Input.GetButtonDown("Fire1") && Time.time >= nextAttackTime && OnGround())
            {
                Attack();
            }

            if(Input.GetButtonDown("Jump") && OnGround() && !attacking) canJump = true;
        }

        #region Attack System

        void Attack()
        {
            attacking = true;
            nextAttackTime = Time.time + 1f/attackSpeed;
            Invoke(nameof(AllowToAttackAgain), 1f / attackSpeed);
            photonView.RPC(nameof(AttackPun), RpcTarget.All);
        
        }

        void AllowToAttackAgain()
        {
            attacking = false; 
        }

        [PunRPC]

        void AttackPun()
        {
            anim.SetTrigger("MeleeAttack");
            Invoke(nameof(TryToHitEnemy), attackDelay);
        }

        void TryToHitEnemy()
        {
            var _enemies = Physics.OverlapSphere(posAttack.position, attackRange, enemyLayer);

            foreach (var _enemy in _enemies)
            {
                Debug.Log("Acerto o inimigo: " + _enemy.name);
                _enemy.GetComponent<Player>().TakeDamage(attackDamage);
            }
        }

        #endregion

        #region Movements

        protected override void Move()
        {
            base.Move();

            Vector3 _velocity = (transform.right * direction.x + transform.forward * direction.y) * moveSpeed;
            _velocity.y = rb.velocity.y;

            if(attacking) 
            {
                _velocity.x = 0f;
                _velocity.z = 0f;
            }

            rb.velocity = _velocity;

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
        #endregion
        protected override void Animations()
        {
            base.Animations();

            anim.SetFloat("SpeedX", direction.x);
            anim.SetFloat("SpeedZ", direction.y);
        }
    }

}