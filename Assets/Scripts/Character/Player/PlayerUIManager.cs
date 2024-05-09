using Unity.Netcode;
using UnityEngine;

namespace UU
{
    public class PlayerUIManager : MonoBehaviour
    {
        [SerializeField] private bool _startGameAsClient;
        
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
        
        //  ПРИСОЕДИНИТЬСЯ К ХОСТУ КАК КЛИЕНТ
        private void Update()
        {
            if (_startGameAsClient)
            {
                _startGameAsClient = false;
                //  WE MUST FIRST SHUT DOWN, BECAUSE WE HAVE STARTED AS A HOST DURING THE TITLE SCREEN
                NetworkManager.Singleton.Shutdown();
                //  WE THEN RESTART, AS A CLIENT
                NetworkManager.Singleton.StartClient();
            }
        }
    }
}