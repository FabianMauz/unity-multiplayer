using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class DealDamageOnContact : MonoBehaviour{
    [SerializeField]
    private int damage=5;
    private ulong clientId;

    public void setOwner(ulong ownerId) {
        this.clientId = ownerId;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.attachedRigidbody == null) {return;}
        if (collision.attachedRigidbody.GetComponent<Health>() == null) { return; }

        NetworkObject netObject = collision.attachedRigidbody.GetComponent<NetworkObject>();
        if (netObject != null) {
            if (netObject.OwnerClientId == clientId) {
                return;
            }
        }

        collision.attachedRigidbody.GetComponent<Health>().takeTamage(damage);


    }
}
