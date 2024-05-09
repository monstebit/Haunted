using Unity.Netcode;
using UnityEngine;

namespace UU
{
    public class WorldNetworkUIManager : MonoBehaviour
    {
        public void StartNetworkAsHost()
        {
            NetworkManager.Singleton.StartHost();
        }
    }
}