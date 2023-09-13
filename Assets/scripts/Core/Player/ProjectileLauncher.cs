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
    [SerializeField]
    private GameObject muzzleFlash;
    [SerializeField]
    private Collider2D collider;

    [Header("Settings")]
    [SerializeField]
    private float projectileSpeed;
    [SerializeField]
    private float fireRate;
    [SerializeField]
    private float muzzleFlashDuration;

    private bool shouldFire;
    private float reloadTime;
    private float muzzleFlashTimer;


    public override void OnNetworkSpawn() {
        if (!IsOwner) { return; }
        inputReader.primaryFireEvent += fireProjectile;

    }

    public override void OnNetworkDespawn() {
        if (!IsOwner) { return; }
        inputReader.primaryFireEvent -= fireProjectile;
    }
    private void Update(){
        if (muzzleFlashTimer > 0) {
            muzzleFlashTimer -= Time.deltaTime;
            if (muzzleFlashTimer <= 0) {
                muzzleFlash.SetActive(false);
            }
        }

        if (reloadTime > 0) {
            reloadTime -= Time.deltaTime;
            reloadTime = Math.Max(0, reloadTime);
        }

        if (!IsOwner||!shouldFire) {return; }
        if (reloadTime == 0) {
            primaryFireServerRpc(spawnPoint.position, spawnPoint.up);
            spawnDummyProjectile(spawnPoint.position, spawnPoint.up);
            reloadTime = fireRate;
        }
    }

    private void spawnDummyProjectile(Vector3 spawnPoint, Vector3 direction) {
        
            GameObject projectileInstance=Instantiate(clientProjectilePrefab, spawnPoint, Quaternion.identity);
            projectileInstance.transform.up = direction;
            muzzleFlashTimer = muzzleFlashDuration;
            muzzleFlash.SetActive(true);
            Physics2D.IgnoreCollision(collider, projectileInstance.GetComponent<Collider2D>());

            Rigidbody2D rb = projectileInstance.GetComponent<Rigidbody2D>();
            rb.velocity = rb.transform.up * projectileSpeed;
          
        


    }

    private void fireProjectile(bool shouldFireIn) {
        shouldFire = shouldFireIn;
    }

    [ServerRpc]
    private void primaryFireServerRpc(Vector3 spawnPoint, Vector3 direction) {
       
            GameObject projectileInstance = Instantiate(serverProjectilePrefab, spawnPoint, Quaternion.identity);
            projectileInstance.transform.up = direction;
            spawnDummyProjectileClientRpc(spawnPoint, direction);
            Physics2D.IgnoreCollision(collider, projectileInstance.GetComponent<Collider2D>());
            Rigidbody2D rb = projectileInstance.GetComponent<Rigidbody2D>();
            rb.velocity = rb.transform.up * projectileSpeed;
       
    }

    [ClientRpc]
    private void spawnDummyProjectileClientRpc(Vector3 spawnPoint, Vector3 direction) {
        if (IsOwner) { return; }
        spawnDummyProjectile(spawnPoint, direction);
    }


    
}
