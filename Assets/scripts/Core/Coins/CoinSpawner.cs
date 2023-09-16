using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CoinSpawner :NetworkBehaviour{
    [SerializeField]
    private RespawningCoin prefab;
    [SerializeField]
    private int MAX_COINS = 50;
    [SerializeField]
    private int coinValue = 10;
    [SerializeField]
    private Vector2 xSpawnRange;
    [SerializeField]
    private Vector2 ySpawnRange;
    [SerializeField]
    private LayerMask layerMask;

    private Collider2D[] coinBuffer = new Collider2D[1];

    private float coinRadius;

    public override void OnNetworkSpawn() {
        if (!IsServer) { return; }
        coinRadius = prefab.GetComponent<CircleCollider2D>().radius;

        for(int i = 0; i <= MAX_COINS; i++) {
            spawnCoin();
        }
    }
   
    private void spawnCoin() {
        RespawningCoin coin = Instantiate(prefab, getSpawnPoint(), Quaternion.identity);
        coin.setValue(coinValue);
        coin.GetComponent<NetworkObject>().Spawn();
        coin.onCollected += handleCoinCollected;
    }

    private Vector2 getSpawnPoint() {
        float x = 0;
        float y = 0;
        int tries = 0;
        while (tries<100000) {
            x = Random.Range(xSpawnRange.x, xSpawnRange.y);
            y = Random.Range(ySpawnRange.x, ySpawnRange.y);
            Vector2 spawnPoint = new Vector2(x, y);
             Collider2D hit = Physics2D.OverlapCircle(spawnPoint, coinRadius, layerMask.value);
            if (hit == null) {
                return spawnPoint;
            }
            tries++;
        }
        return new Vector2(0,0);
    }

    private void handleCoinCollected(RespawningCoin coin) {
        coin.transform.position = getSpawnPoint();
        coin.reset();
    }

}
