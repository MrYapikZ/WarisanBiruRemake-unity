using System;
using System.Collections.Generic;
using UnityEngine;

namespace ExpiProject.PuzzleMinigame.SO
{
    [CreateAssetMenu(menuName = "ScriptableObject/PuzzleAssets", fileName = "PuzzleAssetsScriptableObject")]
    public class PuzzleAssetsScriptableObject : ScriptableObject
    {
        public List<PuzzleAssets> puzzleAssets = new List<PuzzleAssets>();
    }

    [Serializable]
    public struct PuzzleAssets
    {
        public Sprite[] pieces;
        public Sprite[] background;
    }
}