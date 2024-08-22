using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PhotonTest.UI.Lobby 
{
    public class InputPanel : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private Button _confirmButton;
        [SerializeField] private Button _cancelButton;

        public GameObject GameObject { get; private set; }

        public Action<string> RoomCreationConfirmed;

        public void Initilize()
        {
            GameObject = gameObject;
        }

        private void OnEnable()
        {
            _confirmButton.onClick.AddListener(OnConfirmButtonPressed);
            _cancelButton.onClick.AddListener(OnCancelButtonPressed);
            _inputField.onEndEdit.AddListener(OnInputEdited);
        }

        private void OnDisable()
        {
            _confirmButton.onClick.RemoveListener(OnConfirmButtonPressed);
            _cancelButton.onClick.RemoveListener(OnCancelButtonPressed);
            _inputField.onEndEdit.RemoveListener(OnInputEdited);
        }

        private void OnConfirmButtonPressed()
        {
            RoomCreationConfirmed?.Invoke(_inputField.text);
            Reset();
        }

        private void OnCancelButtonPressed()
        {
            Reset();
        }

        private void OnInputEdited(string currentInput)
        {
            if (currentInput == string.Empty)
                _confirmButton.interactable = false;
        }

        private void Reset()
        {
            _inputField.text = string.Empty;
            GameObject.SetActive(false);
        }
    }
}