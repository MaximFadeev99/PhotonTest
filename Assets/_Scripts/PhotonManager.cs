using System.Collections.Generic;
using Photon.Pun;
using System;
using Photon.Realtime;
using Zenject;
using PhotonTest.Signals;
using PhotonTest.Utilities;
using PhotonTest.UI.Chatting;
using Cysharp.Threading.Tasks;

namespace PhotonTest 
{
    public class PhotonManager : MonoBehaviourPunCallbacks
    {
        private SignalBus _signalBus;
        private Chat _chat;

        internal bool IsConnectedToMaster { get; private set; } = false;
        internal bool IsConnectedToLobby { get; private set; } = false;

        [Inject]
        private void Construct(SignalBus signalBus, Chat chat) 
        {
            _signalBus = signalBus;
            _chat = chat;
        }

        public override void OnEnable()
        {
            base.OnEnable();
            _signalBus.Subscribe<CreateRoomSignal>(CreateNewRoom);
            _signalBus.Subscribe<ToMainMenuSignal>(OnReturnToMainMenu);
            _signalBus.Subscribe<JoinClickedSignal>(OnJoinClicked);
            _signalBus.Subscribe<ToLobbySignal>(OnReturnToLobby);
            _signalBus.Subscribe<QuickMatchSignal>(OnQuickMatchClicked);
        }

        public override void OnDisable()
        {
            base.OnDisable();
            _signalBus.Unsubscribe<CreateRoomSignal>(CreateNewRoom);
            _signalBus.Unsubscribe<ToMainMenuSignal>(OnReturnToMainMenu);
            _signalBus.Unsubscribe<JoinClickedSignal>(OnJoinClicked);
            _signalBus.Unsubscribe<ToLobbySignal>(OnReturnToLobby);
            _signalBus.Unsubscribe<QuickMatchSignal>(OnQuickMatchClicked);
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            //This update is called when a client joins a lobby and when a status of room changes. roomList will not 
            //contain all visible rooms. It usually contains rooms whose status or parameters have changed within a tick. 
            //When a room is closing, RemoveFromList in RoomInfo is set to true;

            //Since the following event will be fired first when this client joins a lobby, it is necessary to load the Lobby scene
            //before joining a lobby. Otherwise, the LobbyMenu will not be able to initialize, subscrive to the event
            //and properly draw existing room options.
            _signalBus.Fire(new RoomUpdateSignal(roomList));
        }

        internal void SetRandomID()
        {
           PhotonNetwork.AuthValues.UserId = Guid.NewGuid().ToString();
        }

        internal void ConnectToBestServer() 
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        internal void ConnectToLobby() 
        {
            PhotonNetwork.JoinLobby();
        }

        internal void Disconnect() 
        {
            PhotonNetwork.Disconnect();
        }    

        private void CreateNewRoom(CreateRoomSignal createRoomSignal) 
        {
            RoomOptions roomOptions = new()
            {
                IsVisible = true,
                MaxPlayers = 5
            };

            //The following method will create a room with the specified name and automatically add the creating client 
            //to it. 
            PhotonNetwork.CreateRoom(createRoomSignal.RoomName, roomOptions);
        }

        private void OnReturnToMainMenu() 
        {
            PhotonNetwork.LeaveLobby();
        }

        private void OnJoinClicked(JoinClickedSignal joinSignal) 
        {
            PhotonNetwork.JoinRoom(joinSignal.RoomName);
        }

        private async void OnReturnToLobby() 
        {
            //When joining a room, a client get disconnected from a lobby. So when leaving a room and
            //returning to the Lobby scene, we must wait again for PhotonNetwork to establish connection and be ready 
            //to process commands. Otherwise, PhotonNetwork.JoinLobby() will not work and will NOT fire an exception;

            PhotonNetwork.LeaveRoom();
            await UniTask.WaitUntil(() => PhotonNetwork.IsConnectedAndReady == true);
            ConnectToLobby();
        }

        private void OnQuickMatchClicked() 
        {
            PhotonNetwork.JoinRandomOrCreateRoom();
        }

        #region Success Callbacks
        public override void OnConnectedToMaster()
        {
            IsConnectedToMaster = true;
            _chat.LogMessage(nameof(PhotonManager), "Successfully conected to Master", MessageTypes.Message);
        }

        public override void OnJoinedLobby()
        {
            IsConnectedToLobby = true;
            _chat.LogMessage(nameof(PhotonManager), $"Successfully joined lobby {PhotonNetwork.CurrentLobby.Name}",
                MessageTypes.Message);
        }

        public override void OnCreatedRoom()
        {
            _chat.LogMessage(nameof(PhotonManager), $"Successfully created room {PhotonNetwork.CurrentRoom.Name}",
                MessageTypes.Message);
        }

        public async override void OnJoinedRoom()
        {
            PhotonNetwork.LoadLevel(SceneNames.GameScene);
            _chat.LogMessage(nameof(PhotonManager), $"Successfully joined room {PhotonNetwork.CurrentRoom.Name}",
                MessageTypes.Message);
            await UniTask.WaitUntil(() => PhotonNetwork.LevelLoadingProgress == 1f);
            _signalBus.Fire(new SceneLoadedSignal(SceneNames.GameScene));
        }
        #endregion

        #region Failure Callbacks
        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            _chat.LogMessage(nameof(PhotonManager), $"Room creation failed. Message from Photon:{message}", 
                MessageTypes.Error);
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            IsConnectedToLobby = false;
            IsConnectedToMaster = false;
            _chat.LogMessage(nameof(PhotonManager), $"Connection to Photon lost. Cause:{cause}", MessageTypes.Error);
            _signalBus.Fire(new ToMainMenuSignal());
        }

        public override void OnErrorInfo(ErrorInfo errorInfo)
        {
            _chat.LogMessage(nameof(PhotonManager), $"{errorInfo.Info}", MessageTypes.Error);
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            _chat.LogMessage(nameof(PhotonManager), $"Joining the random room failed. Error info: {message}", 
                MessageTypes.Error);
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            _chat.LogMessage(nameof(PhotonManager), $"Joining the room failed. Error info: {message}",
                MessageTypes.Error);
        }

        public override void OnCustomAuthenticationFailed(string debugMessage)
        {
            _chat.LogMessage(nameof(PhotonManager), $"Custom authentication failed. Error info: {debugMessage}",
                MessageTypes.Error);
        }
        #endregion
    }
}