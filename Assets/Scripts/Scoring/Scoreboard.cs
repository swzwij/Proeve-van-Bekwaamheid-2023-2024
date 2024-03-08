using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace UntitledCube.Scoring
{
    public static class Scoreboard
    {
        private const string FILE_NAME = "ScoreRecord.json";

        public static void Save(Dictionary<string, float> dictionary)
        {
            string jsonData = JsonUtility.ToJson(dictionary);
            string filePath = Path.Combine(Application.persistentDataPath, FILE_NAME);
            File.WriteAllText(filePath, jsonData);

            Debug.Log("Dictionary saved to: " + filePath);
            Debug.Log($"Dictionary saved with {dictionary.Count} items");
            Debug.Log(jsonData);
        }

        public static Dictionary<string, float> Load()
        {
            string filePath = Path.Combine(Application.persistentDataPath, FILE_NAME);

            if (!File.Exists(filePath))
            {
                Debug.LogWarning("File not found: " + filePath);
                return null;
            }

            string jsonData = File.ReadAllText(filePath);
            Dictionary<string, float> dictionary = JsonUtility.FromJson<Dictionary<string, float>>(jsonData);
            Debug.Log("Dictionary loaded from: " + filePath);
            Debug.Log("Dictionary has " + dictionary.Count + " items");
            return dictionary;
        }

        public static void Add(string key, float value)
        {
            Dictionary<string, float> dictionary = Load() ?? new Dictionary<string, float>();
            if(!dictionary.ContainsKey(key))
                dictionary.Add(key, value);
            dictionary[key] = value;
            Save(dictionary);
        }

        public static void Remove(string key)
        {
            Dictionary<string, float> dictionary = Load();
            if (dictionary == null || !dictionary.ContainsKey(key))
                return;

            dictionary.Remove(key);
            Save(dictionary);
        }
    }
}