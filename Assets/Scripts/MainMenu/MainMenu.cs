using ExpiProject.Others;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ExpiProject.MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button playButton, guideButton, exitButton;
        [SerializeField] private string sceneToLoad;

        private void Awake()
        {
            playButton.onClick.AddListener(PlayGame);
            // optionsButton.onClick.AddListener(OptionsGame);
            exitButton.onClick.AddListener(ExitGame);
        }

        private void Start()
        {
            Transition.instance.FadeOut();
        }

        private void PlayGame()
        {
            Transition.instance.FadeIn(() => SceneManager.LoadScene(sceneToLoad));
        }

        private void GuidesGame()
        {
            // Transition.instance.FadeIn(() => SceneManager.LoadScene("Guides"));
        }

        private void ExitGame()
        {
            Application.Quit();
        }
    }
}