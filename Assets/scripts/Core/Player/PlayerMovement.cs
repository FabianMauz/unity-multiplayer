using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour {
    [Header("References")]
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Transform bodyTransform;
    [SerializeField] private Rigidbody2D rb;
    [Header("Settings")]
    [SerializeField] float movementSpeed=4f;
    [SerializeField] float turningRate=30f;

    private Vector2 previousMovementInput;

    public override void OnNetworkSpawn(){
        if(!IsOwner){return;}
        inputReader.playerMoveEvent += handleMovement;
    }

    public override void OnNetworkDespawn() {
        if (!IsOwner) { return; }
        inputReader.playerMoveEvent -= handleMovement;
    }

    private void handleMovement(Vector2 movement) {
        this.previousMovementInput = movement;
    }
    private void Update(){
        if (!IsOwner) { return; }
        float zRotation = previousMovementInput.x * -turningRate * Time.deltaTime;
        bodyTransform.Rotate(0, 0, zRotation);
    }

    private void FixedUpdate() {
        if (!IsOwner) { return; }
        rb.velocity = (Vector2)bodyTransform.up * previousMovementInput.y * movementSpeed;
    }
}
