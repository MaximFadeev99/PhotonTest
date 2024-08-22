using UnityEngine;
using Photon.Pun;
using Zenject;
using PhotonTest.Weapons;
using PhotonTest.Weapons.Bullets;
using PhotonTest.Utilities;
using System.Collections.Generic;
using PhotonTest.Signals;
using PhotonTest.UI.Score;

namespace PhotonTest 
{
    public class RPCHandler : MonoBehaviour
    {
        [SerializeField] private BulletDecalManager _bulletDecalManager;
        [SerializeField] private ScoreManager _scoreManager;

        private PhotonView _photonView;
        private string _localPlayerNickname;
        private SignalBus _signalBus;

        public void Initialize(SignalBus signalBus, string localPlayerNickname, int playerViewID) 
        {
            _signalBus = signalBus;
            _localPlayerNickname = localPlayerNickname;
            _photonView = GetComponent<PhotonView>();
            _signalBus.Fire(new ScoreChangedSignal(_localPlayerNickname, 0));
            InitializeLocalClonesOfExistingPlayers();
            SetSubscriptions();
            _photonView.RPC(nameof(InitializeLocalClone), RpcTarget.Others, playerViewID, _localPlayerNickname, 0);
        }

        private void OnDestroy()
        {
            RemoveSubscriptions();
        }

        private void SetSubscriptions() 
        {
            _signalBus.Subscribe<DecalPlacedSignal>(OnLocalBulletDecalPlaced);
            _signalBus.Subscribe<LocalPlayerScoreSetSignal>(OnLocalPlayerScoreChanged);
            _signalBus.Subscribe<WeaponEquippedSignal>(OnLocalPLayerEquippedWeapon);
        }

        private void RemoveSubscriptions() 
        {
            _signalBus.Unsubscribe<DecalPlacedSignal>(OnLocalBulletDecalPlaced);
            _signalBus.Unsubscribe<LocalPlayerScoreSetSignal>(OnLocalPlayerScoreChanged);
            _signalBus.Unsubscribe<WeaponEquippedSignal>(OnLocalPLayerEquippedWeapon);
        }

        private void InitializeLocalClonesOfExistingPlayers() 
        {
            foreach (KeyValuePair<int, Photon.Realtime.Player> kvp in PhotonNetwork.CurrentRoom.Players)
            {
                if (kvp.Value.NickName == _localPlayerNickname)
                    continue;

                int currentScore = (int)kvp.Value.CustomProperties[CustomProperties.Score];

                _signalBus.Fire(new ScoreChangedSignal(kvp.Value.NickName, currentScore));

                int weaponIndex = (int)kvp.Value.CustomProperties[CustomProperties.WeaponIndex];
                int viewID = (int)kvp.Value.CustomProperties[CustomProperties.ViewID];

                MainPlayer.Player targetPlayer = PhotonView.Find(viewID).GetComponent<MainPlayer.Player>();
                targetPlayer.InitializeAsClone(kvp.Value.NickName, weaponIndex);
            }
        }

        private void OnLocalBulletDecalPlaced(DecalPlacedSignal decalPlacedSignal) 
        {
            if (decalPlacedSignal == null ||
                decalPlacedSignal.PlayerNickname == string.Empty)
            {
                return;
            }

            _photonView.RPC(nameof(PlaceDecalOnOthers), RpcTarget.Others, decalPlacedSignal.WorldPosition, 
                decalPlacedSignal.SurfaceNormal, decalPlacedSignal.PlayerNickname);
        }

        private void OnLocalPlayerScoreChanged(LocalPlayerScoreSetSignal signal) 
        {
            _photonView.RPC(nameof(SetLocalPlayerScoreOnOthers), RpcTarget.Others, signal.Nickname, signal.Score);
        }

        private void OnLocalPLayerEquippedWeapon(WeaponEquippedSignal signal) 
        {
            _photonView.RPC(nameof(EquipWeaponOnClone), RpcTarget.Others, 
                signal.PlayerViewId, signal.EquippedWeaponIndex);
        }

        #region RPCs

        [PunRPC]
        public void PlaceDecalOnOthers(Vector3 worldPosition, Vector3 surfaceNormal, string playerNickname) 
        {
            if (playerNickname == _localPlayerNickname)
                return;

            _bulletDecalManager.PlaceDecal(worldPosition, surfaceNormal, playerNickname, false);
        }

        [PunRPC]
        public void SetLocalPlayerScoreOnOthers(string nickname, int scoreToSet) 
        {
            _scoreManager.SetScore(nickname, scoreToSet);
        }

        [PunRPC]
        public void EquipWeaponOnClone(int photonViewID, int weaponIndex) 
        {
            MainPlayer.Player targetClone = PhotonView.Find(photonViewID).GetComponent<MainPlayer.Player>();

            targetClone.EquipWeapon(weaponIndex);
        }

        [PunRPC]
        public void InitializeLocalClone(int playerViewID, string nickname, int weaponIndex) 
        {
            MainPlayer.Player targetPlayer = PhotonView.Find(playerViewID).GetComponent<MainPlayer.Player>();

            targetPlayer.InitializeAsClone(nickname, weaponIndex);
            _signalBus.Fire(new ScoreChangedSignal(nickname, 0));
        }

        #endregion
    }
}

