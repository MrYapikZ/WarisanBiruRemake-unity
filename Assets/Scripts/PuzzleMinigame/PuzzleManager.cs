using System.Collections.Generic;
using ExpiProject.GameManager;
using ExpiProject.Others;
using ExpiProject.PuzzleMinigame.SO;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace ExpiProject.PuzzleMinigame
{
    public class PuzzleManager : MonoBehaviour
    {
        public static PuzzleManager instance;

        [SerializeField] private PuzzleAssetsScriptableObject puzzleAssets;
        [SerializeField] private GameObject puzzlePartContainer;
        [SerializeField] private GameObject puzzleBoardContainer;
        private int currentPoints;

        private int pointToWin;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            pointToWin = puzzlePartContainer.transform.childCount;
            RandomizeTarget();

            Transition.instance.FadeOut();
        }

        private void RandomizeTarget()
        {
            var count = puzzlePartContainer.transform.childCount;

            var indices = new List<int>();
            for (var i = 0; i < count; i++)
                indices.Add(i);

            for (var i = 0; i < count; i++)
            {
                var temp = indices[i];
                var randIndex = Random.Range(i, count);
                indices[i] = indices[randIndex];
                indices[randIndex] = temp;
            }

            for (var i = 0; i < count; i++)
            {
                var targetIndex = indices[i];
                var child = puzzlePartContainer.transform.GetChild(targetIndex);
                var sprite = puzzleAssets.puzzleAssets[GameManager.GameManager.instance.level].pieces[i];
                child.GetComponent<SpriteRenderer>().sprite = sprite;
                child.GetComponent<SnapScript>().targetBoard = puzzleBoardContainer.transform.GetChild(i).transform;
                puzzleBoardContainer.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite =
                    puzzleAssets.puzzleAssets[GameManager.GameManager.instance.level].background[i];
            }
        }

        public void AddPoint()
        {
            currentPoints = Mathf.Clamp(currentPoints + 1, 0, pointToWin);
            if (currentPoints >= pointToWin)
            {
                var score =
                    GameManager.GameManager.instance.scoreData.lvlScores[GameManager.GameManager.instance.level];
                score.isGameFinished = true;
                GameManager.GameManager.instance.scoreData.lvlScores[GameManager.GameManager.instance.level] = score;
                SaveLoadData.JsonSave(GameManager.GameManager.instance.scoreData);
                Transition.instance.FadeIn(() => SceneManager.LoadScene("LevelSelection"));
            }
        }
    }
}