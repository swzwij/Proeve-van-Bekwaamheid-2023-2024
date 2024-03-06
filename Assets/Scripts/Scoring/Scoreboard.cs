using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Xml.Serialization;

namespace UntitledCube.Scoring
{
    public static class Scoreboard
    {
        private static readonly string _filePath = Application.persistentDataPath + "/newScoreboard.xml";
        private static Dictionary<string, float> _scores = new();

        public static Dictionary<string, float> Scores => _scores;

        public static Action OnScoreAdded;
        public static Action OnInitialized;

        public static void AddScore(string seed, float time)
        {
            if(!_scores.ContainsKey(seed))
                _scores.Add(seed, time);

            _scores[seed] = time;
            SaveScores();
            OnScoreAdded?.Invoke();
        }

        private static void SaveScores()
        {
            var serializer = new XmlSerializer(typeof(Dictionary<string, float>));
            using var writer = new StreamWriter(_filePath);
            serializer.Serialize(writer, _scores);
        }

        public static void LoadScores()
        {
            if (!File.Exists(_filePath)) return;

            var serializer = new XmlSerializer(typeof(Dictionary<string, float>));
            using var reader = new StreamReader(_filePath);
            _scores = (Dictionary<string, float>)serializer.Deserialize(reader);
        }
    }
}