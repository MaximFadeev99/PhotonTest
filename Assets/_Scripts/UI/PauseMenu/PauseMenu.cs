using PhotonTest.Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace PhotonTest.UI.Pause 
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private Button _returnButton;

        private SignalBus _signalBus;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void OnEnable()
        {
            _returnButton.onClick.AddListener(OnReturnPressed);
        }

        private void OnDisable()
        {
            _returnButton.onClick.RemoveListener(OnReturnPressed);
        }

        private void OnReturnPressed()
        {
            _signalBus.Fire(new ToLobbySignal());
        }
    }
}