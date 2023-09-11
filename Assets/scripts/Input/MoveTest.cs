using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTest : MonoBehaviour{

    [SerializeField]
    private InputReader inputReader;

    void Start(){
        inputReader.playerMoveEvent += handleMove;
    }

    private void handleMove(Vector2 value) {
        Debug.Log(value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
