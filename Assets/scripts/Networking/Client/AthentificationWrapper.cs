using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using UnityEngine;

public static class AthentificationWrapper {
    public static AuthentificationState authState { get; private set; } = AuthentificationState.NOT_AUTHENTICATED;

    public static async Task<AuthentificationState> doAuth(int attemps = 5) {
        if (authState == AuthentificationState.AUTHENTICATED){ return authState; }

        authState = AuthentificationState.AUTHENTICATING;
        int tries = 0;
        while (authState==AuthentificationState.AUTHENTICATING&& tries < attemps) {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();

            if(AuthenticationService.Instance.IsSignedIn && AuthenticationService.Instance.IsAuthorized) {
                authState = AuthentificationState.AUTHENTICATED;
                break;
            }
            tries++;
            await Task.Delay(1000);
        }

        return authState;
            
    }
}

public enum AuthentificationState {
    NOT_AUTHENTICATED,
    AUTHENTICATING,
    AUTHENTICATED,
    ERROR,
    TIME_OUT

}
