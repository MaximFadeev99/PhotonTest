using PhotonTest.Signals;
using PhotonTest.UI.Chatting;
using PhotonTest.Utilities;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace PhotonTest.UI.Lobby
{
    public class LobbyMenu : MonoBehaviour
    {
        [SerializeField] private Button _createRoomButton;
        [SerializeField] private Button _fastPlayButton;
        [SerializeField] private Button _joinButton;
        [SerializeField] private Button _returnButton;
        [SerializeField] private AvailableRoomMenu _availableRoomMenu;
        [SerializeField] private InputPanel _inputPanel;

        private SignalBus _signalBus;
        private Chat _chat;

        [Inject]
        private void Construct(SignalBus signalBus, Chat chat) 
        {
            _signalBus = signalBus;
            _chat = chat;
        }

        private void Awake()
        {
            _inputPanel.Initilize();
        }

        private void OnEnable()
        {
            _inputPanel.RoomCreationConfirmed += OnRoomCreationConfirmed;
            _createRoomButton.onClick.AddListener(OnCreateRoomClicked);
            _fastPlayButton.onClick.AddListener(OnFastPlayClicked);
            _joinButton.onClick.AddListener(OnJoinClicked);
            _returnButton.onClick.AddListener(OnReturnClicked);
            _signalBus.Subscribe<RoomUpdateSignal>(OnRoomsUpdated);
        }

        private void OnDisable()
        {
            _inputPanel.RoomCreationConfirmed -= OnRoomCreationConfirmed;
            _createRoomButton.onClick.RemoveListener(OnCreateRoomClicked);
            _fastPlayButton.onClick.RemoveListener(OnFastPlayClicked);
            _joinButton.onClick.RemoveListener(OnJoinClicked);
            _returnButton.onClick.RemoveListener(OnReturnClicked);
            _signalBus.Unsubscribe<RoomUpdateSignal>(OnRoomsUpdated);
        }

        private void OnCreateRoomClicked() 
        {
            _inputPanel.GameObject.SetActive(true);
        }

        private void OnRoomCreationConfirmed(string roomName) 
        {
            _signalBus.Fire(new CreateRoomSignal(roomName));
        }

        private void OnFastPlayClicked() 
        {
            _signalBus.Fire(new QuickMatchSignal());
        }

        private void OnJoinClicked() 
        {
            if (_availableRoomMenu.SelectedRoomCard == null) 
            {
                _chat.LogMessage(nameof(LobbyMenu), "No room is selected. Please, choose a room from the list before clicking " +
                    "Join button", MessageTypes.Warning);

                return;
            }

            _signalBus.Fire(new JoinClickedSignal(_availableRoomMenu.SelectedRoomCard.RoomName));
        }

        private void OnReturnClicked() 
        {
            _signalBus.Fire(new ToMainMenuSignal());
        }

        private void OnRoomsUpdated(RoomUpdateSignal roomUpdateSignal) 
        {
            _availableRoomMenu.DrawRooms(roomUpdateSignal.RoomData);
        }
    }
}