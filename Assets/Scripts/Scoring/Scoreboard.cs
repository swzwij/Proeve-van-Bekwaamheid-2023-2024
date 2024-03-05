using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;

namespace UntitledCube.Scoring
{
    public static class Scoreboard
    {
        private static string _filePath;
        private static Dictionary<string, float> _scores;

        public static Dictionary<string, float> Scores => _scores;

        public static Action OnScoreAdded;
        public static Action OnInitialized;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Initialize()
        {
            _filePath = Application.persistentDataPath + "/scoreboard.json";
            _scores = new Dictionary<string, float>();
            LoadScores();
            OnInitialized?.Invoke();
        }

        public static void AddScore(string seed, float time)
        {
            if(!_scores.ContainsKey(seed))
                _scores.Add(seed, time);

            _scores[seed] = time;
            SaveScores();
            OnScoreAdded?.Invoke();
        }

        private static void LoadScores()
        {
            if (!File.Exists(_filePath))
                return;

            string jsonData = File.ReadAllText(_filePath, Encoding.UTF8);
            _scores = JsonUtility.FromJson<Dictionary<string, float>>(jsonData);
        }

        private static void SaveScores()
        {
            string jsonData = JsonUtility.ToJson(_scores);
            File.WriteAllText(_filePath, jsonData, Encoding.UTF8);
        }
    }
}