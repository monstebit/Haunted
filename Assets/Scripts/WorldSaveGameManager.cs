using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UU
{
    public class WorldSaveGameManager : MonoBehaviour
    {
        [SerializeField] private int _worldSceneIndex = 1;
        
        public int WorldSceneIndex => _worldSceneIndex;

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        public IEnumerator LoadNewGame()
        {
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(_worldSceneIndex);

            yield return null;
        }
    }
}