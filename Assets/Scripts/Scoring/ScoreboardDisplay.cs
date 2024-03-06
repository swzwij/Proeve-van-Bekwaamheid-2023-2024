using System.Collections.Generic;
using UnityEngine;

namespace UntitledCube.Scoring
{
    public class ScoreboardDisplay : MonoBehaviour
    {
        [SerializeField] private ScoreItem _scoreItem;
        [SerializeField] private Transform _content;

        private void Awake() => Display();

        private void Display()
        {
            Scoreboard.LoadScores();

            Dictionary<string, float> scores = Scoreboard.Scores;
            foreach (string score in scores.Keys) 
            {
                ScoreItem item = Instantiate(_scoreItem, _content);
                item.Init(score, scores[score]);
            }
        }
    }
}