using Mystimor;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Mystimor
{
    
    //Anexa obrigatoriamente o componente RigidBody ao objeto que este script estiver anexado, tambem não deixa o mesmo ser retirado.
    [RequireComponent(typeof(Rigidbody))]

    public class Player : Character // faz o jogador herdar as funções e variaveis da classe Character 
    {

        Player photonPlayer;

        protected Rigidbody rb;

        protected float rotY;
        [Tooltip("Define a velocidade  de rotação do jogador")]
        [SerializeField] protected float rotSpeed;

        [Header ("Variáveis da lista de tarefas")]
        private TaskUI taskUI; // -- Variável do canvas de tarefas --
        private List<TaskBase> tasks = new List<TaskBase>(); // lista de tarefas
        //public bool isChatActive;




        #region Unity Metods
        protected override void Awake()
        {   
            //base.Awake: Tras as configurações feitas previamente no metodo Awake da classe Character
            base.Awake(); 
           //Awake: Inicializa o e configura a câmera para o player local.Rigidbody
           rb = GetComponent<Rigidbody>();
           // photonView.Isme está sendo utilizado para saber se a camera do jogador é dele e desativa
           if(photonView.IsMine) Camera.main.gameObject.SetActive(false);
      
        }
        protected void Start()
        {
            //Recupera a lista de tarefas dentro do mapa e a instância.TaskUI
            GetTasks();
            taskUI = FindAnyObjectByType<TaskUI>();
        }
        protected override void Update()
        {
            base.Update();
            //Dead delimita a movimentação e rotação do jogador caso ele esteja morto
            if (dead)
            {
                direction.x = 0;
                direction.y = 0;
                return;
            }

            if(!photonView.IsMine) return; //Ismine para verificar se o controle não for do meu jogador, não aceita os inputs e Rotação
            
            //Manipula a entrada e rotação do jogador, garantindo que as entradas sejam aceitas apenas do jogador local.
            
                PlayerInputs();
                RotatePlayer();

        
        }
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            //Gerencia o movimento do jogador.
            Move();
        }
        #endregion


        #region  Tasks Sistem
        protected void GetTasks()
        {
            //GetTasks: Busca a lista de tarefas do .TaskService
            tasks = TaskService.Instance.GetTasks(true);
        }

        private void OnTriggerEnter(Collider collision) 
        {
            //Define a tarefa atual quando o jogador entra em um colisor de tarefas.
            if (collision.GetComponent<TaskBase>())
            {
               

                taskUI.SetPlayerCurrentTask(collision.GetComponent<TaskBase>(), tasks.FindIndex(a => a == collision.GetComponent<TaskBase>()));
            }
        }

        private void OnTriggerExit(Collider collision)
        {
            //Limpa a tarefa atual quando o jogador sai de um colisor de tarefas.
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
            //PlayerInputs: Lida com o movimento, rotação e entradas de ataque do jogador.    
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
            //Attack: Inicia o ataque, sincroniza-o pela rede e agenda o próximo ataque.
            attacking = true;
            //Calcula o tempo do proximo ataque
            nextAttackTime = Time.time + 1f/attackSpeed;
            Invoke(nameof(AllowToAttackAgain), 1f / attackSpeed);
            photonView.RPC(nameof(AttackPun), RpcTarget.All);
        
        }

        void AllowToAttackAgain()
        {
            //AllowToAttackAgain: Redefine o estado de ataque.
            attacking = false; 
        }

        [PunRPC]

        void AttackPun()
        {
            // Aciona a animação de ataque e agenda a detecção de acertos.
            anim.SetTrigger("MeleeAttack");
            Invoke(nameof(TryToHitEnemy), attackDelay);
        }

        void TryToHitEnemy()
        {
            //Detecta e aplica dano a inimigos dentro do alcance de ataque
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
        //Atualiza a velocidade do jogador com base na entrada e no estado de ataque
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
            //Gira o player com base na entrada do mouse
            transform.Rotate(0f, rotY * rotSpeed * Time.deltaTime, 0f);
        } 

        public override void Jump(float _value)
        {
            //Gerencia o salto do jogador, redefinindo a velocidade vertical antes de aplicar a força de salto.
            base.Jump(_value);

            Vector3 _velocity = rb.velocity;
            _velocity.y = 0f;

            rb.velocity = _velocity;

            rb.AddForce(Vector3.up * _value);
        }
        #endregion
        protected override void Animations()
        {
            //Atualiza os parâmetros de animação com base no movimento do jogador
            base.Animations();

            anim.SetFloat("SpeedX", direction.x);
            anim.SetFloat("SpeedZ", direction.y);
        }
    }

}