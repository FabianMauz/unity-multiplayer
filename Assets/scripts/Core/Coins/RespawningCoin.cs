using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawningCoin : Coin {
    public override int collect() {

        if (!IsServer) { show(false); return 0; }
        if (alreadyCollected) { return 0; }
        alreadyCollected = true;
        return coinValue;
    }
}
