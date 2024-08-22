using Photon.Pun;
using PhotonTest.Input;
using PhotonTest.Signals;
using PhotonTest.UI.Score;
using PhotonTest.Utilities;
using UnityEngine;
using Zenject;
using Player = PhotonTest.MainPlayer.Player;

namespace PhotonTest 
{
    public class GameSceneManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] private Spawner _spawner;
        [SerializeField] private RPCHandler _rpcHandler;
        [SerializeField] private ScoreManager _scoreManager;

        private SignalBus _signalBus;

        [Inject]
        private void Construct(SignalBus signalBus, InputLogger inputLogger) 
        {
            _signalBus = signalBus;
            _spawner.Initialize(_signalBus, inputLogger);
            _signalBus.Subscribe<SceneLoadedSignal>(OnGameSceneLoaded);
        }

        public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
        {
            _scoreManager.RemovePlayerScore(otherPlayer.NickName);
        }

        private void OnGameSceneLoaded(SceneLoadedSignal signal) 
        {
            if (signal.SceneName != SceneNames.GameScene)
                return;

            string localNickname = GetRandomNickname();
            PhotonNetwork.NickName = localNickname;
            Player localPlayer = _spawner.SpawnPlayer(localNickname);

            _scoreManager.Initialize(_signalBus, localPlayer.Nickname);
            _rpcHandler.Initialize(_signalBus, localPlayer.Nickname, localPlayer.ViewID);
        }

        private string GetRandomNickname()
        {
            return "Player" + Random.Range(1, 21);
        }
    }
}