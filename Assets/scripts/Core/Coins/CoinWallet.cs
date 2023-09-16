using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CoinWallet : NetworkBehaviour{
    public NetworkVariable<int> totalCoins = new NetworkVariable<int>(0);

    private void OnTriggerEnter2D(Collider2D collision) {
        Coin c=collision.GetComponent<Coin>();
        if (c == null) { return; }
        int value = c.collect();
        if (!IsServer) { return; }

        totalCoins.Value += value;

    }

}
