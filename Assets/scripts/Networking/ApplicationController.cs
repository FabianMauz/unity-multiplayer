using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ApplicationController : MonoBehaviour{
    [SerializeField]
    private ClientSingelton clientPrefab;
    [SerializeField]
    private HostSingelton hostPrefab;

    private async void  Start(){
        DontDestroyOnLoad(gameObject);

        bool dedicatedServer = SystemInfo.graphicsDeviceType == UnityEngine.Rendering.GraphicsDeviceType.Null;
        await launchInMode(dedicatedServer);
    }

    private async Task launchInMode(bool dedicatedServer) {
        if (dedicatedServer) {

        }else {
            Task.Delay(1000);
            

            ClientSingelton clientSingelton = Instantiate(clientPrefab);
            bool authenticated = await clientSingelton.createClient();

            HostSingelton hostSingelton = Instantiate(hostPrefab);
            hostSingelton.createHost();


            if (authenticated) {
                clientSingelton.gameManager.goToMenu();
            }


        }
    }

   
}
