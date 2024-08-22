using Photon.Realtime;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace PhotonTest.UI.Lobby
{
    [RequireComponent(typeof(ToggleGroup))]
    public class AvailableRoomMenu : MonoBehaviour
    {
        [SerializeField] private RoomCard _roomCardPrefab;
        [SerializeField] private Transform _content;

        private readonly List<RoomCard> _activeCards = new();
        private ToggleGroup _toggleGroup;

        public RoomCard SelectedRoomCard { get; private set; }

        private void Awake()
        {
            _toggleGroup = GetComponent<ToggleGroup>();
        }

        public void DrawRooms(IReadOnlyList<RoomInfo> roomData) 
        {
            foreach (RoomInfo roomInfo in roomData) 
            {
                if (roomInfo.RemovedFromList == true)
                {
                    TryRemoveRoomCard(roomInfo.Name);                
                    continue;
                }

                CreateRoomCard(roomInfo);               
            }   
        }

        private void ClearActiveCards() 
        {
            foreach (RoomCard roomCard in _activeCards) 
            {
                roomCard.Toggled -= OnRoomCardToggled;
                roomCard.SetToggleGroup(null);
                Destroy(roomCard.GameObject);
            }

            _activeCards.Clear();
        }

        private void TryRemoveRoomCard(string roomName) 
        {
            RoomCard removedRoomCard = _activeCards.FirstOrDefault
                        (activeCard => activeCard.RoomName == roomName);

            if (removedRoomCard != null)
            {
                removedRoomCard.Toggled -= OnRoomCardToggled;
                removedRoomCard.SetToggleGroup(null);
                Destroy(removedRoomCard.GameObject);
            }
        }

        private void CreateRoomCard(RoomInfo roomInfo) 
        {
            RoomCard newRoomCard = Instantiate(_roomCardPrefab, _content);
            newRoomCard.Draw(roomInfo);
            newRoomCard.SetToggleGroup(_toggleGroup);
            newRoomCard.Toggled += OnRoomCardToggled;
            _activeCards.Add(newRoomCard);
        }

        private void OnRoomCardToggled(bool isSelected, RoomCard roomCard) 
        {
            SelectedRoomCard = isSelected ? roomCard : null;
        }

        private void OnDestroy()
        {
            ClearActiveCards();
        }
    }
}