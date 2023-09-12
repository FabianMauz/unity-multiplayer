using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerAiming : NetworkBehaviour {
    [SerializeField]
    private Transform turretTransform;
    [SerializeField]
    private InputReader inputReader;

    private void LateUpdate() {
        if (!IsOwner) { return; }
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(inputReader.aimPosition);
        turretTransform.up = new Vector2(
            mousePosition.x - turretTransform.position.x,
            mousePosition.y - turretTransform.position.y);
    }
}
