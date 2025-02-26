using MarkUlrich.StateMachine.States;
using MarkUlrich.StateMachine;
using System.Collections;
using UnityEngine;
using UntitledCube.Advertisements;
using UntitledCube.Gravity;
using UntitledCube.Maze.Generation;
using UnityEngine.UI;
using UntitledCube.Timer;

namespace UntitledCube.UI.Buttons
{
    public class NewLevelButton : MonoBehaviour
    {
        [SerializeField] private Button _newLevelButton;

        private void Start() => _newLevelButton.onClick.AddListener(ResetLevel);

        private void ResetLevel()
        {
            if (GameStateMachine.Instance.CurrentState is not GameState)
                return;

            Stopwatch.Instance.ResetStopWatch();
            Advertising.Instance.ShowAd();
            MazeGenerator.Generate(new(6, 6), "");

            Stopwatch.Instance.StartWithInteractionSync();
        }
    }
}