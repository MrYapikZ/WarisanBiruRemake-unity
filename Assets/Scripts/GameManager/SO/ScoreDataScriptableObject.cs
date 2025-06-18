using System;
using System.Collections.Generic;
using UnityEngine;

namespace ExpiProject.GameManager.SO
{
    [CreateAssetMenu(menuName = "ScriptableObject/ScoreData", fileName = "ScoreDataScriptableObject")]
    public class ScoreDataScriptableObject : ScriptableObject
    {
        public List<LvlScore> lvlScores = new();
    }

    [Serializable]
    public struct LvlScore
    {
        public string name;
        public bool isGameFinished;
        public bool[] quizPoint;
    }
}