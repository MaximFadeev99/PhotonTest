using Cysharp.Threading.Tasks;
using PhotonTest.SceneManagement;
using PhotonTest.Signals;
using PhotonTest.UI.Chatting;
using PhotonTest.Utilities;
using UnityEngine;
using Zenject;

namespace PhotonTest 
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private PhotonManager _photonManager;
        [SerializeField] private SceneManager _sceneManager;
        
        private Chat _chat;
        private SignalBus _signalBus;

        [Inject]
        private void Initialize(SignalBus signalBus, Chat chat)
        {
            _signalBus = signalBus;
            _chat = chat;
        }

        private void Awake()
        {
            _chat.Initialize();
        }

        private void OnEnable()
        {
            _signalBus.Subscribe<SceneLoadedSignal>(OnNewSceneLoaded);
            _signalBus.Subscribe<QuitClickedSignal>(OnQuitClicked);
            _signalBus.Subscribe<PlayClickedSignal>(OnPlayClicked);
        }

        private void OnDisable()
        {
            _signalBus.Unsubscribe<SceneLoadedSignal>(OnNewSceneLoaded);
            _signalBus.Unsubscribe<QuitClickedSignal>(OnQuitClicked);
            _signalBus.Unsubscribe<PlayClickedSignal>(OnPlayClicked);
        }

        private async void Start()
        {
            _photonManager.ConnectToBestServer();
            await UniTask.WaitUntil(() => _photonManager.IsConnectedToMaster);
            _photonManager.SetRandomID();
            _ = _sceneManager.LoadScene(SceneNames.MainMenu);
        }

        private void OnNewSceneLoaded(SceneLoadedSignal sceneLoaded) 
        {
            //CustomLogger.Log(nameof(GameManager), $"{sceneLoaded.SceneName} scene has been successfully loaded",
            //    MessageTypes.Message);
        }

        private void OnQuitClicked() 
        {
            Application.Quit();
        }

        private async void OnPlayClicked() 
        {
            await _sceneManager.LoadScene(SceneNames.Lobby);
            _photonManager.ConnectToLobby();
        }

        private void OnApplicationQuit()
        {
            _photonManager.Disconnect();
        }
    }
}