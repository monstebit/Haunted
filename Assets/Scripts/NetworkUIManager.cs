using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace UU
{
    public class NetworkUIManager : MonoBehaviour
    {
        [SerializeField] private Button _hostButton;

        private void Awake()
        {
            _hostButton.onClick.AddListener(() =>
            {
                NetworkManager.Singleton.StartHost();
            });
        }
    }
}