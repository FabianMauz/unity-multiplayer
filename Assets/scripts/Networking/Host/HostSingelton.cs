using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class HostSingelton : MonoBehaviour{

    private static HostSingelton instance;
    public HostGameManager gameManager { private set; get; }

    public static HostSingelton Instance { get {
            if (instance != null) { return instance; }
            instance = FindAnyObjectByType<ApplicationController>().hostPrefab;
            
            if (instance == null) {
                print("Error in host singelton instantion: "+FindAnyObjectByType<HostSingelton>());
                return null;
            }
            return instance;
        }
    }
    
    void Start(){
        DontDestroyOnLoad(gameObject);
    }

    public async Task createHost() {
        gameManager = new HostGameManager();
        
        await gameManager.startHostAsync();
    }

}
