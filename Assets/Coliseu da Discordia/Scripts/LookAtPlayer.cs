using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class LookAtPlayer : MonoBehaviourPun
{
    /*  Essa classe faz com que um objeto procure a camera do jogador e fique olhando para ela.
        EX: barra de vida dos jogadores    
        Podemos fazer para canvas de interação os objetos ou só é indicado para os jogadores?
    */

    private void Update()
    {
        RotateToPlayer();
    }
    void RotateToPlayer()
    {
       transform.LookAt(GameManager.instance.cameraPlayer.position);
    }
}
