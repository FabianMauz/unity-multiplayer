using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawningCoin : Coin {

    public event Action<RespawningCoin> onCollected;
    public override int collect() {

        if (!IsServer) { show(false); return 0; }
        if (alreadyCollected) { return 0; }
        alreadyCollected = true;
        onCollected?.Invoke(this);
        return coinValue;
    }

    public void reset() {
        alreadyCollected = false;
    }
}
