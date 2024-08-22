using PhotonTest.Utilities;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PhotonTest.UI.Chatting 
{
    [RequireComponent(typeof(TMP_Text), typeof(ContentSizeFitter))]
    public class ChatMessage : MonoBehaviour, IMonoBehaviourPoolElement
    {
        private TMP_Text _textMeshPro;
        private GameObject _gameObject;

        public GameObject GameObject => _gameObject;
        public RectTransform RectTransform { get; private set; }

        public void Awake()
        {
            _gameObject = gameObject;
            _textMeshPro = GetComponent<TMP_Text>();
            RectTransform = GetComponent<RectTransform>();
        }

        public void FillIn(DateTime logTime, string loggingScriptName, string message, Color messageColor)
        {
            string hour = logTime.Hour < 10 ? $"0{logTime.Hour}" : logTime.Hour.ToString();
            string minute = logTime.Minute < 10 ? $"0{logTime.Minute}" : logTime.Minute.ToString();

            _textMeshPro.text = $"{hour}:{minute}| {loggingScriptName}: {message}";
            _textMeshPro.color = messageColor;
        }
    }
}