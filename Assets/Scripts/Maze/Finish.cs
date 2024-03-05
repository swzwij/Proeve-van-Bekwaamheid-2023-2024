using UntitledCube.Maze.Generation;
using UntitledCube.Scoring;
using UntitledCube.Timer;

namespace UntitledCube.Maze
{
    public static class Finish
    {
        public static void FinishMaze()
        {
            string seed = MazeGenerator.Seed;
            float time = Stopwatch.Instance.ElapsedTime;

            Scoreboard.AddScore(seed, time);
        }
    }
}