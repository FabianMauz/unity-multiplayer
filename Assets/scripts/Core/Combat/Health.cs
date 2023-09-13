using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Health : NetworkBehaviour{

    [field: SerializeField]
    public int MAX_HEALTH { get; private set; } = 100;
    public NetworkVariable<int> currentHealth = new NetworkVariable<int>();
    private bool isDead;

    public Action<Health> onDie;

    public override void OnNetworkSpawn() {
        if (!IsServer) { return; }
        currentHealth.Value = MAX_HEALTH;
    }

    public void takeTamage(int damage) {
        modifyHealth(-damage);
    }

    public void restoreHealth(int heal) {
        modifyHealth(heal);
    }

    private void modifyHealth(int amount) {
        if (isDead) { return; }
        currentHealth.Value = Mathf.Clamp(currentHealth.Value + amount, 0, MAX_HEALTH);
        if (currentHealth.Value == 0) {
            isDead = true;
            onDie?.Invoke(this);
        }
    }
}
