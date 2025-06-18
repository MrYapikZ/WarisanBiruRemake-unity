using ExpiProject.Others;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ExpiProject.LevelSelection
{
    public class LevelSelection : MonoBehaviour
    {
        [SerializeField] private GameObject levelButtonContainer;
        [SerializeField] private Button backButton;

        private void Start()
        {
            Transition.instance.FadeOut();
            for (var i = 0; i < levelButtonContainer.transform.childCount; i++)
            {
                var index = i;
                levelButtonContainer.transform.GetChild(index).GetComponent<Button>().onClick.AddListener(() =>
                    Transition.instance.FadeIn(() => SceneManager.LoadScene("Level" +
                                                                            (index + 1))));
                levelButtonContainer.transform.GetChild(index).GetComponent<Button>().interactable = false;
            }

            levelButtonContainer.transform.GetChild(0).GetComponent<Button>().interactable = true;
            backButton.onClick.AddListener(() => Transition.instance.FadeIn(() => SceneManager.LoadScene("MainMenu")));
        }
    }
}