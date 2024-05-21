using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject playerPrefab;
    private void Start()
    {
        Debug.Log("Start");
        ConnectToPhoton();
            
    }

    //Método para realizar a conecção com o Photon
    public void ConnectToPhoton()
    {
        Debug.Log("ConnectToPhoton");
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
        Debug.Log("Sala Criada");
        
    }
    //Entre na sala cria e me diga quantos jogadores estão conectados a ela
    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
        Debug.Log("Playercount: " + PhotonNetwork.CurrentRoom.PlayerCount);
        photonView.RPC("CreatePlayerAvatar", PhotonNetwork.LocalPlayer);
    }

    [PunRPC] //PunRPC é utilizado para chamar o método abaixo de forma remota, em outros computadores
    void CreatePlayerAvatar()
    {
        //Cria uma localização aleatória entre -3 e 3 com 2 de altura para instanciar o player
        Vector3 _pos = new Vector3(Random.Range(-3f, 3f), 2f, Random.Range(-3f, 3f));

        //Instancia o prefab na rede usando seu nome e localização
        PhotonNetwork.Instantiate(playerPrefab.name, _pos, Quaternion.identity);
    }

}
