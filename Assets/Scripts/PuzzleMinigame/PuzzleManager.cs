using System;
using System.Collections.Generic;
using ExpiProject.Others;
using ExpiProject.PuzzleMinigame.SO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace ExpiProject.PuzzleMinigame
{
    public class PuzzleManager : MonoBehaviour
    {
        public static PuzzleManager instance;
        
        [SerializeField] private PuzzleAssetsScriptableObject  puzzleAssets;
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
            int count = puzzlePartContainer.transform.childCount;
            
            List<int> indices = new List<int>();
            for (int i = 0; i < count; i++)
                indices.Add(i);
            
            for (int i = 0; i < count; i++)
            {
                int temp = indices[i];
                int randIndex = Random.Range(i, count);
                indices[i] = indices[randIndex];
                indices[randIndex] = temp;
            }
            
            for (int i = 0; i < count; i++)
            {
                int targetIndex = indices[i];
                var child = puzzlePartContainer.transform.GetChild(targetIndex);
                var sprite = puzzleAssets.puzzleAssets[GameManager.GameManager.instance.level].pieces[i];
                child.GetComponent<SpriteRenderer>().sprite = sprite;
                child.GetComponent<SnapScript>().targetBoard = puzzleBoardContainer.transform.GetChild(i).transform;
                puzzleBoardContainer.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = puzzleAssets.puzzleAssets[GameManager.GameManager.instance.level].background[i];
            }
        }

        public void AddPoint()
        {
            currentPoints = Mathf.Clamp(currentPoints + 1, 0, pointToWin);
            if (currentPoints >= pointToWin)
            {
                var score = GameManager.GameManager.instance.scoreData.lvlScores[GameManager.GameManager.instance.level];
                score.isGameFinished = true;
                GameManager.GameManager.instance.scoreData.lvlScores[GameManager.GameManager.instance.level] = score;
                Transition.instance.FadeIn(() => SceneManager.LoadScene("LevelSelection")); 
            }
        }
    }
}