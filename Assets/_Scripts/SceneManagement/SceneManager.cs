using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using Zenject;
using PhotonTest.Signals;
using PhotonTest.Utilities;

namespace PhotonTest.SceneManagement
{
    public class SceneManager : MonoBehaviour
    {
        private SignalBus _signalBus;

        [Inject]
        private void Initialize(SignalBus signalBus) 
        {
            _signalBus = signalBus;
        }

        private void OnEnable()
        {
            _signalBus.Subscribe<ToMainMenuSignal>(LoadMainMenu);
            _signalBus.Subscribe<ToLobbySignal>(LoadLobby);
        }

        private void OnDisable()
        {
            _signalBus.Unsubscribe<ToMainMenuSignal>(LoadMainMenu);
            _signalBus.Unsubscribe<ToLobbySignal>(LoadLobby);
        }

        public async UniTask LoadScene(string sceneName) 
        {
            AsyncOperation sceneLoading =  UnityEngine.SceneManagement.SceneManager.LoadSceneAsync
                (sceneName, LoadSceneMode.Single);
            
            if (sceneLoading != null)            
                await UniTask.WaitUntil(() => sceneLoading.isDone == true);
            
            _signalBus.Fire(new SceneLoadedSignal (sceneName));
        }

        private void LoadMainMenu() 
        {
            _ = LoadScene(SceneNames.MainMenu);
        }

        private void LoadLobby() 
        {
            _ = LoadScene(SceneNames.Lobby);
        }
    }
}