using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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

        public static MetaData MetaData;

        public static string AssignmentText { get; set; }
        public static string Name { get; set; }
        public static string Path { get; set; }
        public static string SaveDate { get; set; }
        public static Texture2D Texture2D { get; set; }
        public static Picture Thumbnail { get; set; }

        public static MetaData LoadMetaData(string filePath)
        {
            string metaDataName = System.IO.Path.GetFileName(filePath).Replace(".jpg", ".dat");

            string folderPath = Path.Replace(System.IO.Path.GetFileName(filePath), "");
            string metaDataPath = System.IO.Path.Combine(folderPath, "MetaData", metaDataName);

            FileStream file;

            if (File.Exists(metaDataPath)) file = File.OpenRead(metaDataPath);
            else
            {
                Debug.LogError($"{metaDataPath} not found");

                return MetaData;
            }

            BinaryFormatter formatter = new BinaryFormatter();
            MetaData data = (MetaData)formatter.Deserialize(file);
            file.Close();

            return data;
        }

        public static void SaveMetaData()
        {
            string metaDataName = Name.Replace(".jpg", ".dat");

            string folderPath = Path.Replace(System.IO.Path.GetFileName(Path), "");
            string metaDataPath = System.IO.Path.Combine(folderPath, "MetaData");

            if (!Directory.Exists(metaDataPath))
            {
                Directory.CreateDirectory(metaDataPath);
            }

            metaDataPath = System.IO.Path.Combine(metaDataPath, metaDataName);

            FileStream file = File.Exists(metaDataPath) ? File.OpenWrite(metaDataPath)
                                                        : File.Create(metaDataPath);

            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(file, MetaData); // 'MetaData' struct now includes our dictionary
            file.Close();
        }

        private static MetaData SetMetaData()
        {
            MetaData.AssignmentText = AssignmentText;
            MetaData.Name = Name;
            MetaData.Path = Path;
            MetaData.ThumbnailName = Thumbnail.Name;
            MetaData.ThumbnailPath = Thumbnail.Path;
            MetaData.MetaDataName = Name.Replace(".jpg", ".dat");
            MetaData.MetaDataPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "MetaData", MetaData.MetaDataName);
            MetaData.SaveDate = SaveDate;

            // Assuming you want to initialize an empty dictionary before returning:
            MetaData.ScoreData = new Dictionary<string, float>();

            return MetaData;
        }
    }
}