
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegurarEPegarObjetos : MonoBehaviour
{
    public Transform handPosition;  // Posição onde o objeto será segurado
    private GameObject heldObject = null;
    private Rigidbody heldObjectRb = null;
    protected Collider _colliderObject;
    [SerializeField] protected float handDistance;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // "Fire1" geralmente é o botão esquerdo do mouse
        {
            //Debug.DrawRay(handPosition.position, transform.forward, Color.red); Mostra a linha feita pelo raycast
            if (heldObject == null)
            {
                TryPickUpObject();
            }
            else
            {
                DropObject();
            }
        }
    }

    void TryPickUpObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(handPosition.position, transform.forward, out hit, handDistance))
        {
            
            if (hit.collider.gameObject.CompareTag("Pickup"))
            {
                heldObject = hit.collider.gameObject;
                heldObjectRb = heldObject.GetComponent<Rigidbody>();
                _colliderObject = heldObject.GetComponent<Collider>();
                if (heldObjectRb != null)
                {
                    _colliderObject.isTrigger = true;
                    heldObjectRb.isKinematic = true;
                    heldObject.transform.position = handPosition.position;
                    heldObject.transform.parent = handPosition;
                }
            }
        }
    }

    void DropObject()
    {
        if (heldObject != null)
        {
            _colliderObject.isTrigger = false;
            heldObjectRb.isKinematic = false;
            heldObject.transform.parent = null;
            heldObject = null;
            heldObjectRb = null;
        }
    }
}