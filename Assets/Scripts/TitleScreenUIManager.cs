using UnityEngine;
using UnityEngine.UI;

namespace UU
{
    public class TitleScreenUIManager : MonoBehaviour
    {
        //  TEMP
        [SerializeField] private WorldNetworkManager _worldNetworkManager;
        [SerializeField] private WorldSaveGameManager _worldSaveGameManager;
        
        [SerializeField] private Button _pressStartButton;
        [SerializeField] private Button _startNewGameButton;
        
        private Coroutine _startNewGame;
        
        private void Start()
        {
            _pressStartButton.onClick.AddListener(OnPressStartButtonHandleClick);
            _startNewGameButton.onClick.AddListener(OnStartNewGameButtonHandleClick);
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
            _worldNetworkManager.StartNetworkAsHost();
            
            _pressStartButton.gameObject.SetActive(false);
            _startNewGameButton.gameObject.SetActive(true);
        }
        
        private void OnStartNewGameButtonHandleClick()
        {
            StartWork();
        }
    }
}