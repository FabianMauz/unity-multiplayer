using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay :NetworkBehaviour{
    [SerializeField]
    private Health health;
    [SerializeField]
    private Image healthBarImage;

    public override void OnNetworkSpawn() {
        if (!IsClient) { return; }
        health.currentHealth.OnValueChanged += handleHealthCHanged;
        handleHealthCHanged(0, health.currentHealth.Value);
    }

    public override void OnNetworkDespawn() {
        if (!IsClient) { return; }
        health.currentHealth.OnValueChanged -= handleHealthCHanged;
    }

    private void handleHealthCHanged(int oldValue, int newValue) {
        healthBarImage.fillAmount = (float)newValue / ((float)health.MAX_HEALTH);
    }
}
