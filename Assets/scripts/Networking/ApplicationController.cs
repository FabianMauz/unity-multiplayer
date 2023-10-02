using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ApplicationController : MonoBehaviour{
    [SerializeField]
    public ClientSingelton clientPrefab;
    [SerializeField]
    public HostSingelton hostPrefab;

    public ClientSingelton clientSingelton;
    public HostSingelton hostSingelton;

    private async void  Start(){
        DontDestroyOnLoad(gameObject);

        bool dedicatedServer = SystemInfo.graphicsDeviceType == UnityEngine.Rendering.GraphicsDeviceType.Null;
        await launchInMode(dedicatedServer);
    }

    private async Task launchInMode(bool dedicatedServer) {
        if (dedicatedServer) {

        }else {
            Task.Delay(1000);
            

            clientSingelton = Instantiate(clientPrefab);
            bool authenticated = await clientSingelton.createClient();

            hostSingelton = Instantiate(hostPrefab);

            if (authenticated) {
                clientSingelton.gameManager.goToMenu();
            }


        }
    }

   
}
