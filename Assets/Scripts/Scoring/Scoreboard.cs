using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace UntitledCube.Scoring
{
    public static class Scoreboard
    {
        private const string FILE_NAME = "ScoreRecord.json";

        [System.Serializable]
        public class SerializableDictionary : Dictionary<string, float> { }

        public static void SaveDictionary(SerializableDictionary dictionary)
        {
            string jsonData = JsonUtility.ToJson(dictionary);
            string filePath = Path.Combine(Application.persistentDataPath, FILE_NAME);
            File.WriteAllText(filePath, jsonData);

            Debug.Log("Dictionary saved to: " + filePath);
        }

        public static SerializableDictionary LoadDictionary()
        {
            string filePath = Path.Combine(Application.persistentDataPath, FILE_NAME);

            if (!File.Exists(filePath))
            {
                Debug.LogWarning("File not found: " + filePath);
                return null;
            }

            string jsonData = File.ReadAllText(filePath);
            SerializableDictionary dictionary = JsonUtility.FromJson<SerializableDictionary>(jsonData);
            Debug.Log("Dictionary loaded from: " + filePath);
            return dictionary;
        }

        public static void Add(string key, float value)
        {
            SerializableDictionary dictionary = LoadDictionary() ?? new SerializableDictionary();
            dictionary[key] = value;
            SaveDictionary(dictionary);
        }

        public static void Remove(string key)
        {
            SerializableDictionary dictionary = LoadDictionary();
            if (dictionary == null || !dictionary.ContainsKey(key))
                return;

            dictionary.Remove(key);
            SaveDictionary(dictionary);
        }
    }
}