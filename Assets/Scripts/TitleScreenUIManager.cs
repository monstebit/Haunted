using UnityEngine;
using UnityEngine.UI;

namespace UU
{
    public class TitleScreenUIManager : MonoBehaviour
    {
        //  TEMP
        [SerializeField] private NetworkUIManager _networkUIManager;
        [SerializeField] private WorldSaveGameManager _worldSaveGameManager;
        
        [SerializeField] private Button _pressStartButton;
        [SerializeField] private Button _startNewGameButton;
        
        private Coroutine _startNewGame;
        
        private void Start()
        {
            if (_pressStartButton != null)
            {
                _pressStartButton.onClick.AddListener(OnPressStartButtonHandleClick);
            }
            
            if (_startNewGameButton != null)
            {
                _startNewGameButton.onClick.AddListener(OnStartNewGameButtonHandleClick);
            }
        }

        private void StartWork()
        {
            StopWork();

            _startNewGame = StartCoroutine(_worldSaveGameManager.LoadNewGame());
        }

        private void StopWork()
        {
            if (_startNewGame != null)
                StopCoroutine(_startNewGame);
        }
        
        private void OnPressStartButtonHandleClick()
        {
            _networkUIManager.StartNetworkAsHost();
            
            _pressStartButton.gameObject.SetActive(false);
            _startNewGameButton.gameObject.SetActive(true);
        }
        
        private void OnStartNewGameButtonHandleClick()
        {
            StartWork();
        }
    }
}