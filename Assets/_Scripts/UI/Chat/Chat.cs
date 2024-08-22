using PhotonTest.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PhotonTest.UI.Chatting 
{
    public class Chat : MonoBehaviour
    {
        [SerializeField] private ChatMessage _chatMessagePrefab;
        [SerializeField] private RectTransform _content;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private RectTransform _scrollViewRectTransform;
        [Tooltip("All inactive messages will be parented to this transform")]
        [SerializeField] private Transform _chatMessageContainer;
        [SerializeField] private Color _errorColor = Color.red;
        [SerializeField] private Color _warningColor = Color.cyan;
        [SerializeField] private Color _logColor = Color.white;
        [Tooltip("The maximum number of visable messages in the chat. Adding every new message above the limit will" +
            "override the earliest message")]
        [SerializeField] private int _maxActiveMessageCount = 3;

        private readonly Queue<ChatMessage> _activeMessages = new();

        private MonoBehaviourPool<ChatMessage> _chatMessagePool;
        private bool _shallUpdateScrollbar = false;
        private ChatMessage _lastMessage;

        public void Initialize()
        {
            _chatMessagePool = new(_chatMessagePrefab, _chatMessageContainer, 10);
        }

        public void LogMessage(string loggingScript, string message, MessageTypes messageType)
        {
            TryDeactivateOldMessage();

            ChatMessage inactiveMessage = _chatMessagePool.GetIdleElement();
            Color messageColor = SetMessageColor(messageType);

            inactiveMessage.FillIn(DateTime.Now, loggingScript, message, messageColor);
            inactiveMessage.GameObject.SetActive(true);
            inactiveMessage.RectTransform.SetParent(_content);
            _activeMessages.Enqueue(inactiveMessage);
            _shallUpdateScrollbar = true; //ScrollBar must be updated on GUI, because at THIS moment new parameters for RectTransforms are not calculated
            _lastMessage = inactiveMessage;
        }

        private void TryDeactivateOldMessage()
        {
            if (_activeMessages.Count < _maxActiveMessageCount)
                return;

            ChatMessage deactivatedMessage = _activeMessages.Dequeue();
            deactivatedMessage.GameObject.SetActive(false);
            deactivatedMessage.RectTransform.SetParent(_chatMessageContainer);
        }

        private Color SetMessageColor(MessageTypes messageType)
        {
            switch (messageType)
            {
                case MessageTypes.Error:
                    return _errorColor;

                case MessageTypes.Assertion:
                    return _errorColor;

                case MessageTypes.Warning:
                    return _warningColor;

                case MessageTypes.Message:
                    return _logColor;

                default:
                    string errorMessage = $"Color for MessageType of {messageType} is not implemented in {nameof(SetMessageColor)} " +
                        $"method of {nameof(Chat)} script. Please, implement it before trying to log messages of this type";
                    CustomLogger.Log(nameof(Chat), errorMessage, MessageTypes.Error);
                    throw new NotImplementedException();
            }
        }

        private void OnGUI()
        {
            if (_shallUpdateScrollbar == false)
                return;

            float totalHeight = _content.rect.height;
            float targetHeight = _lastMessage.RectTransform.rect.height > _scrollViewRectTransform.rect.height ?
                Mathf.Abs(_lastMessage.RectTransform.anchoredPosition.y) - _lastMessage.RectTransform.rect.height / 2f :
                Mathf.Abs(_lastMessage.RectTransform.anchoredPosition.y) + _lastMessage.RectTransform.rect.height / 2f;
            float normalizedTargetHeight = Mathf.Clamp01(1 - (targetHeight / totalHeight));
            _scrollRect.verticalNormalizedPosition = normalizedTargetHeight;
            _shallUpdateScrollbar = false;
        }
    }
}