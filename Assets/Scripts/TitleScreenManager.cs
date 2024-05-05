using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace UU
{
    public class TitleScreenManager : MonoBehaviour
    {
        [SerializeField] private Button _pressStartButton;
        
        private Coroutine _startNewGame;
        
        private void Start()
        {
            if (_pressStartButton != null)
            {
                _pressStartButton.Select();
                _pressStartButton.onClick.AddListener(OnPressStartButtonHandleClick);
            }

        }
        
        private void OnDisable()
        {
            _pressStartButton.onClick.RemoveListener(OnPressStartButtonHandleClick);
        }

        //  NETCODE
        public void StartNetworkAsHost()
        {
            NetworkManager.Singleton.StartHost();
        }
        

        public void StopWork()
        {
            if (_startNewGame != null)
                StopCoroutine(_startNewGame);
        }

        //  UI
        private void OnPressStartButtonHandleClick()
        {
            Debug.Log("HOST HERE");
            StartNetworkAsHost();
            
            _pressStartButton.gameObject.SetActive(false);
        }
    }
}