using System.Collections;
using ExpiProject.GameManager.SO;
using ExpiProject.Npc;
using ExpiProject.Others;
using FloxyDev.DialogueSystem;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ExpiProject.GameManager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        [Header("UI References")] [SerializeField]
        private GameObject screenUI;

        [SerializeField] private Button interactButton;
        [SerializeField] private Button pauseButtonUI;
        [SerializeField] private TextMeshProUGUI scoreText;

        [Header("Game References")]
        public int level;

        public ScoreDataScriptableObject scoreData;

        [SerializeField] private GameObject[] npcObjects;

        private bool isPuzzleEnable;


        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            Transition.instance.FadeOut();
            Pause.instance.ResumeGame();

            interactButton.interactable = false;
            pauseButtonUI.onClick.AddListener(() => Pause.instance.PauseGame());
            scoreText.text = "0/" + scoreData.lvlScores[level].quizPoint.Length;
        }

        #region Scoring

        public void AddScore(int index)
        {
            scoreData.lvlScores[level].quizPoint[index] = true;
            var score = 0;
            for (var i = 0; i < scoreData.lvlScores[level].quizPoint.Length; i++)
                if (scoreData.lvlScores[level].quizPoint[i])
                {
                    score++;
                    scoreText.text = score + "/" + scoreData.lvlScores[level].quizPoint.Length;
                    if (score == scoreData.lvlScores[level].quizPoint.Length) OpenPuzzle();
                }
        }

        private void OpenPuzzle()
        {
            screenUI.SetActive(false);
            var mainCamera = Camera.main;
            if (mainCamera != null) mainCamera.enabled = false;
            Transition.instance.FadeIn(() => SceneManager.LoadScene("PuzzleMinigame", LoadSceneMode.Additive)); 
            isPuzzleEnable = true;
        }

        #endregion

        #region NPC

        public void NpcTriggerArea(int npcID)
        {
            foreach (var npc in npcObjects)
                if (npc.GetComponent<NpcMaster>().npcID == npcID)
                {
                    interactButton.interactable = true;
                    interactButton.onClick.AddListener(() =>
                        DialogueSystemManager.Instance.StartDialogue(npc.GetComponent<DialogueActivator>()));
                }
        }

        public void NpcOutTriggerArea()
        {
            interactButton.interactable = false;
            interactButton.onClick.RemoveAllListeners();
        }

        #endregion
    }
}