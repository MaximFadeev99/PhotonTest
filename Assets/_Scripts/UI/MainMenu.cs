using PhotonTest.Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace PhotonTest.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _quitButton;

        private SignalBus _signalBus;

        [Inject]
        private void Construct(SignalBus signalBus) 
        {
            _signalBus = signalBus;
        }

        private void OnEnable()
        {
            _playButton.onClick.AddListener(OnPlayButtonClicked);
            _quitButton.onClick.AddListener(OnQuitButtonClicked);
        }

        private void OnDisable() 
        {
            _playButton.onClick.RemoveListener(OnPlayButtonClicked);
            _quitButton.onClick.RemoveListener(OnQuitButtonClicked);
        }

        private void OnPlayButtonClicked() 
        {
            _signalBus.Fire(new PlayClickedSignal());
        }

        private void OnQuitButtonClicked() 
        {
            _signalBus.Fire(new QuitClickedSignal());
        }
    }
}