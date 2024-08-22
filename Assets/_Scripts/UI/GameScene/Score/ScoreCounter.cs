using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PhotonTest.UI.Score 
{
    internal class ScoreCounter : MonoBehaviour
    {
        [SerializeField] private ScoreCard _scoreCardPrefab;

        private readonly List<ScoreCard> _createdScoreCards = new();

        private Transform _transform;

        internal void Initialize() 
        {
            _transform = transform;
        }

        internal void UpdateScore(string playerNickname, int playerScore) 
        {
            GetScoreCard(playerNickname).SetScore(playerScore);
        }

        internal void RemoveScore(string playerNickname) 
        {
            if (_createdScoreCards.Any(card => card.PlayerNickname == playerNickname) == false)
                return; 

            ScoreCard removedScoreCard = GetScoreCard(playerNickname);
            _createdScoreCards.Remove(removedScoreCard);
            Destroy(removedScoreCard.GameObject);
        }

        private ScoreCard GetScoreCard(string playerNickname) 
        {
            ScoreCard targetScoreCard = _createdScoreCards
                .FirstOrDefault(scoreCard => scoreCard.PlayerNickname == playerNickname);
            
            if (targetScoreCard != null)
                return targetScoreCard;

            targetScoreCard = Instantiate(_scoreCardPrefab, _transform);
            targetScoreCard.SetPlayerNickName(playerNickname);
            targetScoreCard.SetScore(0);
            _createdScoreCards.Add(targetScoreCard);

            return targetScoreCard;
        }
    }
}