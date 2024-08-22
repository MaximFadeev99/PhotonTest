using TMPro;
using UnityEngine;

namespace PhotonTest.UI.Score 
{
    internal class ScoreCard : MonoBehaviour
    {
        [SerializeField] private TMP_Text _nicknameField;
        [SerializeField] private TMP_Text _scoreField;

        internal string PlayerNickname => _nicknameField.text;
        internal GameObject GameObject { get; private set; }

        private void Awake()
        {
            _nicknameField.text = string.Empty;
            _scoreField.text = string.Empty;
            GameObject = gameObject;
        }

        /// <summary>
        /// Can only be called once. Nickname can not be changed later.
        /// </summary>
        /// <param name="nickname"></param>
        internal void SetPlayerNickName(string nickname) 
        {
            if (_nicknameField.text != string.Empty)
                return;

            _nicknameField.text = nickname;
        }

        internal void SetScore(int score) 
        {
            _scoreField.text = score.ToString();
        }
    }
}