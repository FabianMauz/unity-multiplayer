using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifetime : MonoBehaviour{
    // Start is called before the first frame update
    [SerializeField] 
    private float lifeTimeInSeconds=1;
    void Start(){
        GameObject.Destroy(this.gameObject,lifeTimeInSeconds);
        
    }

    

    
    
}
