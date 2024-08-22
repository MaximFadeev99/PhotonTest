using TMPro;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.UI;
using System;

namespace PhotonTest.UI.Lobby
{
    [RequireComponent(typeof(Toggle))]
    public class RoomCard : MonoBehaviour
    {
        [SerializeField] private TMP_Text _nameField;
        [SerializeField] private TMP_Text _playerCountField;

        private RoomInfo _roomInfo;
        private Toggle _toggle;

        public string RoomName => _roomInfo.Name;
        public GameObject GameObject { get; private set; }

        public Action<bool, RoomCard> Toggled;

        private void Awake()
        {
            GameObject = gameObject;
            _toggle = GetComponent<Toggle>();
        }

        private void OnEnable()
        {
            _toggle.onValueChanged.AddListener(FireToggledEvent);
        }

        private void OnDisable()
        {
            _toggle.onValueChanged.RemoveListener(FireToggledEvent);
        }

        public void Draw(RoomInfo roomInfo)
        {
            _roomInfo = roomInfo;
            _nameField.text = _roomInfo.Name;
            _playerCountField.text = $"{_roomInfo.PlayerCount}/{_roomInfo.MaxPlayers}";
        }

        public void SetToggleGroup(ToggleGroup toggleGroup) 
        {
            _toggle.group = toggleGroup;
        }

        private void FireToggledEvent(bool isSelected) 
        {
            Toggled?.Invoke(isSelected, this);
        }
    }
}