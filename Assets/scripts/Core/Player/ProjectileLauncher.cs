using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ProjectileLauncher : NetworkBehaviour{
    [Header("References")]
    [SerializeField]
    private GameObject serverProjectilePrefab;
    [SerializeField]
    private GameObject clientProjectilePrefab;
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private InputReader inputReader;

    [Header("Settings")]
    [SerializeField]
    private float projectileSpeed;

    private bool shouldFire;


    public override void OnNetworkSpawn() {
        if (!IsOwner) { return; }
        inputReader.primaryFireEvent += fireProjectile;

    }

    public override void OnNetworkDespawn() {
        if (!IsOwner) { return; }
        inputReader.primaryFireEvent -= fireProjectile;
    }
    private void Update(){
        if (!IsOwner||!shouldFire) {return; }

        primaryFireServerRpc(spawnPoint.position, spawnPoint.up);
        spawnDummyProjectile(spawnPoint.position, spawnPoint.up);
    }

    private void spawnDummyProjectile(Vector3 spawnPoint, Vector3 direction) {
        GameObject projectileInstance=Instantiate(clientProjectilePrefab, spawnPoint, Quaternion.identity);
        projectileInstance.transform.up = direction;
    }

    private void fireProjectile(bool shouldFireIn) {
        shouldFire = shouldFireIn;
    }

    [ServerRpc]
    private void primaryFireServerRpc(Vector3 spawnPoint, Vector3 direction) {
        GameObject projectileInstance = Instantiate(serverProjectilePrefab, spawnPoint, Quaternion.identity);
        projectileInstance.transform.up = direction;
        spawnDummyProjectileClientRpc(spawnPoint, direction);
    }

    [ClientRpc]
    private void spawnDummyProjectileClientRpc(Vector3 spawnPoint, Vector3 direction) {
        if (IsOwner) { return; }
        spawnDummyProjectile(spawnPoint, direction);
    }


    
}
