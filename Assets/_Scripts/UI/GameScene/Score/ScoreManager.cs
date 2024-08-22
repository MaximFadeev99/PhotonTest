using Photon.Pun;
using PhotonTest.Utilities;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using PhotonTest.Signals;

namespace PhotonTest.UI.Score
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private ScoreCounter _scoreCounter;

        private readonly Dictionary<string, int> _playerScoreDictionary = new();

        private SignalBus _signalBus;
        private string _localPlayerNickname;

        public void Initialize(SignalBus signalBus, string localPlayerNickname) 
        {
            _signalBus = signalBus;
            _localPlayerNickname = localPlayerNickname;
            _scoreCounter.Initialize();
            _signalBus.Subscribe<ScoreChangedSignal>(OnScoreChanged);
        }

        public void SetScore(string playerNickname, int newScore) 
        {
            _scoreCounter.UpdateScore(playerNickname, newScore);        
        }

        public void RemovePlayerScore(string playerNickname) 
        {
            _scoreCounter.RemoveScore(playerNickname);
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<ScoreChangedSignal>(OnScoreChanged);
        }

        private void OnScoreChanged(ScoreChangedSignal scoreChangedSignal)
        {
            if (_playerScoreDictionary.ContainsKey(scoreChangedSignal.PlayerNickname))
            {
                _playerScoreDictionary[scoreChangedSignal.PlayerNickname] += scoreChangedSignal.ScoreDelta;
            }
            else
            {
                _playerScoreDictionary[scoreChangedSignal.PlayerNickname] = scoreChangedSignal.ScoreDelta;
            }

            _scoreCounter.UpdateScore(scoreChangedSignal.PlayerNickname,
                _playerScoreDictionary[scoreChangedSignal.PlayerNickname]);

            if (scoreChangedSignal.PlayerNickname != _localPlayerNickname)
                return;

            Hashtable newHashable = PhotonNetwork.LocalPlayer.CustomProperties;

            newHashable[CustomProperties.Score] = _playerScoreDictionary[_localPlayerNickname];
            PhotonNetwork.LocalPlayer.SetCustomProperties(newHashable);
            _signalBus.Fire(new LocalPlayerScoreSetSignal(_playerScoreDictionary[_localPlayerNickname]));
        }
    }
}