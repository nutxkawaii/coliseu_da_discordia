using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager instance;


    [SerializeField] GameObject playerPrefabAssasino;
    [SerializeField] GameObject playerPrefabVitima;
    [HideInInspector] public Transform cameraPlayer;

    [SerializeField] GameObject gameScreen;

    private void Awake()
    {
        
        instance = this;
        gameScreen.SetActive(false); //alterar para MenuController ++++++++
    }

    public void StartGame()
    {
        photonView.RPC(nameof(CreatePlayerAvatar),PhotonNetwork.LocalPlayer, NetworkManager.instance.inputNickName.text);
    }




    [PunRPC] //PunRPC � utilizado para chamar o m�todo abaixo de forma remota, em outros computadores
    void CreatePlayerAvatar(string _nickName)
    {
        PhotonNetwork.LocalPlayer.NickName = _nickName;
        //Cria uma localiza��o aleat�ria entre -3 e 3 com 2 de altura para instanciar o player
        Vector3 _pos = new Vector3( -6, 1, 21/*Random.Range(-10f, 35f), 0f, Random.Range(-16f, 20f)*/);


        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            PhotonNetwork.Instantiate(playerPrefabAssasino.name, _pos, Quaternion.identity);

        }
        else
        {
            //Instancia o prefab na rede usando seu nome e localiza��o
            PhotonNetwork.Instantiate(playerPrefabVitima.name, _pos, Quaternion.identity);

        }

        gameScreen.SetActive(true);
        NetworkManager.instance.loadingScreen.SetActive(false);//alterar para MenuController +++++++
        NetworkManager.instance.taskScreen.SetActive(true);//alterar para MenuController +++++++
    }
}
