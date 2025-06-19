using System;
using System.Collections.Generic;
using ExpiProject.GameManager.SO;
using UnityEngine;

namespace ExpiProject.GameManager
{
    public static class SaveLoadData
    {
        private const string KeyPrefix = "ScoreData_";

        private const string ScoreDataKey = "ScoreDataJSON";

        public static void PlayerPrefsSave(ScoreDataScriptableObject scoreData)
        {
            var levelCount = scoreData.lvlScores.Count;
            PlayerPrefs.SetInt(KeyPrefix + "LevelCount", levelCount);

            for (var i = 0; i < levelCount; i++)
            {
                var lvl = scoreData.lvlScores[i];
                PlayerPrefs.SetString($"{KeyPrefix}lvl_{i}_name", lvl.name);
                PlayerPrefs.SetInt($"{KeyPrefix}lvl_{i}_finished", lvl.isGameFinished ? 1 : 0);
                PlayerPrefs.SetInt($"{KeyPrefix}lvl_{i}_quizCount", lvl.quizPoint.Length);

                for (var j = 0; j < lvl.quizPoint.Length; j++)
                    PlayerPrefs.SetInt($"{KeyPrefix}lvl_{i}_quiz_{j}", lvl.quizPoint[j] ? 1 : 0);
            }

            PlayerPrefs.Save();
            Debug.Log("✅ Saved using PlayerPrefs (no JSON)");
        }

        public static void PlayerPrefsLoad(ScoreDataScriptableObject scoreData)
        {
            scoreData.lvlScores.Clear();

            var levelCount = PlayerPrefs.GetInt(KeyPrefix + "LevelCount", 0);

            if (levelCount != 0)
            {
                for (var i = 0; i < levelCount; i++)
                {
                    var name = PlayerPrefs.GetString($"{KeyPrefix}lvl_{i}_name", $"Level {i}");
                    var isFinished = PlayerPrefs.GetInt($"{KeyPrefix}lvl_{i}_finished", 0) == 1;

                    var quizCount = PlayerPrefs.GetInt($"{KeyPrefix}lvl_{i}_quizCount", 0);
                    var quizPoints = new bool[quizCount];

                    for (var j = 0; j < quizCount; j++)
                        quizPoints[j] = PlayerPrefs.GetInt($"{KeyPrefix}lvl_{i}_quiz_{j}", 0) == 1;

                    var score = new LvlScore
                    {
                        name = name,
                        isGameFinished = isFinished,
                        quizPoint = quizPoints
                    };

                    scoreData.lvlScores.Add(score);
                }

                Debug.Log("✅ Loaded from PlayerPrefs (no JSON)");
            }
            else
            {
                Debug.LogWarning("⚠️ No saved score data found, generating defaults.");
                
                for (int i = 0; i < 5; i++)
                {
                    scoreData.lvlScores.Add(new LvlScore
                    {
                        name = $"Level{i}",
                        isGameFinished = false,
                        quizPoint = new bool[] { false, false, false, false }
                    });
                }
            }
        }

        public static void ResetAll()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            Debug.Log("♻️ PlayerPrefs reset");
        }

        public static void JsonSave(ScoreDataScriptableObject scoreData)
        {
            var wrapper = new ScoreDataWrapper
            {
                lvlScores = scoreData.lvlScores
            };

            var json = JsonUtility.ToJson(wrapper);
            PlayerPrefs.SetString(ScoreDataKey, json);
            PlayerPrefs.Save();

            Debug.Log("✅ Saved using PlayerPrefs (JSON)");
        }

        public static void JsonLoad(ScoreDataScriptableObject scoreData)
        {
            if (PlayerPrefs.HasKey(ScoreDataKey))
            {
                var json = PlayerPrefs.GetString(ScoreDataKey);
                var wrapper = JsonUtility.FromJson<ScoreDataWrapper>(json);
                scoreData.lvlScores = wrapper.lvlScores;

                Debug.Log("✅ Loaded from PlayerPrefs (JSON)");
            }
            else
            {
                Debug.LogWarning("⚠️ No saved score data found, generating defaults.");
                
                scoreData.lvlScores.Clear();
                for (int i = 0; i < 5; i++)
                {
                    scoreData.lvlScores.Add(new LvlScore
                    {
                        name = $"Level{i}",
                        isGameFinished = false,
                        quizPoint = new bool[] { false, false, false, false }
                    });
                }
            }
        }

        [Serializable]
        private class ScoreDataWrapper
        {
            public List<LvlScore> lvlScores;
        }
    }
}