using UnityEngine;
using UnityEngine.UI;
using UntitledCube.Timer;

namespace UntitledCube.Scoring
{
    public class ScoreItem : MonoBehaviour
    {
        [SerializeField] private Text seedText;
        [SerializeField] private Text timeText;

        public void Init(string seed, float time)
        {
            seedText.text = seed;
            timeText.text = Stopwatch.Instance.FormatTime(time);
        }
    }
}