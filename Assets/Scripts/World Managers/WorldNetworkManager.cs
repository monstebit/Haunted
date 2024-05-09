using Unity.Netcode;
using UnityEngine;

namespace UU
{
    public class WorldNetworkManager : MonoBehaviour
    {
        public void StartNetworkAsHost()
        {
            NetworkManager.Singleton.StartHost();
        }
    }
}