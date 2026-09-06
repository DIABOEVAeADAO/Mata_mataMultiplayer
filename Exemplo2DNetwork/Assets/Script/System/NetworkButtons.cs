using Unity.Netcode;
using UnityEngine;

public class NetworkButtons : MonoBehaviour
{
    public void BeginHost() => NetworkManager.Singleton.StartHost();
    public void BeginClient() => NetworkManager.Singleton.StartClient();
    public void QuitGame() => NetworkManager.Singleton.Shutdown();

}
