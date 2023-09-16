using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public abstract class Coin : NetworkBehaviour{
    [SerializeField]
    private SpriteRenderer renderer;

    Vector3 previousPosition;

    protected int coinValue=10;
    protected bool alreadyCollected;

    public abstract int collect();

    public void setValue(int value) {
        this.coinValue = value;
    }

    private void Update() {
        if (previousPosition != transform.position) {
            show(true);
        }
        previousPosition = transform.position;
    }

    protected void show(bool show) {
        renderer.enabled = show;
    }
}
