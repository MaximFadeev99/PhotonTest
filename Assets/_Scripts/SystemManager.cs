using PhotonTest.SceneManagement;
using PhotonTest.Signals;
using PhotonTest.Utilities;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using SceneManager = UnityEngine.SceneManagement.SceneManager;

namespace PhotonTest
{
    public class SystemManager : MonoBehaviour
    {
        private const string UICameraName = "UICamera";

        [SerializeField] private SceneManager _sceneManager;
        [SerializeField] private Canvas _chatCanvas;

        private SignalBus _signalBus;
        private Transform _transform;

        public GameObject GameObject { get; private set; }
      
        [Inject]
        private void Initialize(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void Awake()
        {
            GameObject = gameObject;
            _transform = transform;
            DontDestroyOnLoad(GameObject);
            //DontDestroyOnLoad(_chatCanvas.gameObject);
        }

        private void OnEnable()
        {
            _signalBus.Subscribe<SceneLoadedSignal>(OnNewSceneLoaded);
        }

        private void OnDisable()
        {
            _signalBus.Unsubscribe<SceneLoadedSignal>(OnNewSceneLoaded);
        }

        private void OnNewSceneLoaded(SceneLoadedSignal sceneLoaded) 
        {
            SystemManager[] existingManagers = FindObjectsByType<SystemManager>(FindObjectsSortMode.None);

            if (existingManagers.Length > 1)
                DestroyImmediate(existingManagers.Where(manager => manager != this).First().GameObject);

            SetNewCameraForChatCanvas(sceneLoaded.SceneName);
        }

        private void SetNewCameraForChatCanvas(string newSceneName)
        {
            _chatCanvas.transform.SetParent(null);
            //Scene activeScene = SceneManager.GetActiveScene();
            SceneManager.MoveGameObjectToScene(_chatCanvas.gameObject, SceneManager.GetActiveScene());

            Camera cameraToAssign = newSceneName switch
            {
                SceneNames.GameScene => FindObjectsByType<Camera>(FindObjectsSortMode.None)
                        .First(camera => camera.name == UICameraName),
                _ => Camera.main,
            };

            _chatCanvas.worldCamera = cameraToAssign;
            DontDestroyOnLoad(_chatCanvas.gameObject);
            _chatCanvas.transform.SetParent(_transform);
        }
    }
}