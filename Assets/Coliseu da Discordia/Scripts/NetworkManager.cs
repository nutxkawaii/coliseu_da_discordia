using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;





    public class NetworkManager : MonoBehaviourPunCallbacks
    {
        public static NetworkManager instance;
        /*  Transforma esse script Network Manager para um singleton, Usamos apenas quando temos certeza que APENAS UM OBJETO obterá essa classe[script] 
            [Beneficio] Podemos acessar esse scrip de qualquer objeto que fizermos referência ao Network Manager e suas funcionalidades
         */

        public TMP_InputField inputNickName; // Campo para digitar o nome do jogador ++++++++
        public GameObject menuScreen; //alterar para MenuController ++++++++
        public GameObject loadingScreen; //alterar para MenuController ++++++++
        public GameObject taskScreen; //alterar para MenuController ++++++++

       


        /* List<Photon.Realtime.Player> players;
        Pedir ao Cleber para Criando uma lista com o nome de todos os jogadores da rede 
        podemos usar esse recurso para a votação quando o jogador assasino for suspeito */

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                menuScreen.SetActive(true);
                loadingScreen.SetActive(false);
                taskScreen.SetActive(false);
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
        private void Start()
        {
            Debug.Log("Start");


        }

        //Método para realizar a conecção com o Photon
        public void ConnectToPhoton()
        {
            Debug.Log("ConnectToPhoton");
            loadingScreen.SetActive(true);
            menuScreen.SetActive(false);
           // taskScreen.SetActive(true);

            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("OnConnectedToMaste");
            PhotonNetwork.JoinLobby();
        }
        //Procura uma sala aleatória para se conectar
        public override void OnJoinedLobby()
        {
            Debug.Log("OnJoinedLobby");
            PhotonNetwork.JoinRandomRoom();
        }

        /* Quando nao conseguir entrar em uma sala aleatória
        Criar uma sala para se conectar null seria o nome da sala, mas nao precisamos especificar*/
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("OnJoinRandomFailed");
            PhotonNetwork.CreateRoom(null);
        }

        //Crie uma sala
        public override void OnCreatedRoom()
        {
            Debug.Log("OnCreatedRoom");
            //Debug.Log("Sala Criada");

        }
        //Entre na sala cria e me diga quantos jogadores estão conectados a ela
        public override void OnJoinedRoom()
        {
            Debug.Log("OnJoinedRoom");
            Debug.Log("Playercount: " + PhotonNetwork.CurrentRoom.PlayerCount);
            Debug.Log("NickName: " + inputNickName.text);
            //PhotonNetwork.LocalPlayer.NickName = inputNickName.text;
            GameManager.instance.StartGame();
            // photonView.RPC("CreatePlayerAvatar", PhotonNetwork.LocalPlayer);
        }



    }


