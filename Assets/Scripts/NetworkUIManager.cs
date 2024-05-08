using Unity.Netcode;
using UnityEngine;

namespace UU
{
    public class NetworkUIManager : MonoBehaviour
    {
        public void StartNetworkAsHost()
        {
            NetworkManager.Singleton.StartHost();
        }
    }
}