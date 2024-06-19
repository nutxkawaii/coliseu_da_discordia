using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using Photon.Pun;
using Mystimor;


public class ChatBox : MonoBehaviourPun
{
    public TextMeshProUGUI chatLogText;
    public TMP_InputField chatInput;


    // instance
    public static ChatBox instance;


    void Awake()
    {
        instance = this;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            
            if (EventSystem.current.currentSelectedGameObject == chatInput.gameObject)
                BtnEnviarMsg();
            else
                EventSystem.current.SetSelectedGameObject(chatInput.gameObject);
        }
    }


    // Quando o jogador aciona o bot�o enviar mensagem
    public void BtnEnviarMsg()
    {
        if (chatInput.text.Length > 0)
        {
            photonView.RPC("Log", RpcTarget.All, PhotonNetwork.LocalPlayer.NickName, chatInput.text);
            chatInput.text = "";
        }


        EventSystem.current.SetSelectedGameObject(null);
    }


    // chamado quando um jogador digita uma mensagem na caixa de bate-papo
    // envia para todos os jogadores na sala para atualizar sua interface do usu�rio
    [PunRPC]
    void Log(string playerName, string message)
    {
        // atualiza o chat log com as mensagens enviadas
        chatLogText.text += string.Format("<b>{0}:</b> {1}\n", playerName, message);


        // ajusta o tamanho do chat log conforme o tamanho do texto
        chatLogText.rectTransform.sizeDelta = new Vector2(chatLogText.rectTransform.sizeDelta.x, chatLogText.mesh.bounds.size.y + 20);
    }
}