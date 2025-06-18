using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ExpiProject.Others
{
    public class Pause : MonoBehaviour
    {
        public static Pause instance;
        
        [SerializeField] private GameObject pausePanelUI;
        [SerializeField] private Button resumeButtonUI;
        [SerializeField] private Button exitButtonUI;
        [HideInInspector] public bool isPaused;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            resumeButtonUI.onClick.AddListener(ResumeGame);
            exitButtonUI.onClick.AddListener(ExitGame);
        }

        public void PauseGame()
        {
            isPaused = true;
            Time.timeScale = 0;
            pausePanelUI.SetActive(true);
        }
        
        public void ResumeGame()
        {
            isPaused = false;
            Time.timeScale = 1;
            pausePanelUI.SetActive(false);
        }
        
        public void ExitGame()
        {
            Time.timeScale = 1;
            Transition.instance.FadeIn(() => SceneManager.LoadScene("LevelSelection"));
            // Application.Quit();
        }
    }
}