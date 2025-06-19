using ExpiProject.GameManager.SO;
using ExpiProject.Others;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ExpiProject.LevelSelection
{
    public class LevelSelection : MonoBehaviour
    {
        [SerializeField] private ScoreDataScriptableObject scoreData;
        [SerializeField] private GameObject levelButtonContainer;
        [SerializeField] private Button backButton;

        private void Start()
        {
            Transition.instance.FadeOut();

            for (var i = 0; i < levelButtonContainer.transform.childCount; i++)
            {
                var index = i;
                levelButtonContainer.transform.GetChild(index).GetComponent<Button>().onClick.AddListener(() =>
                    Transition.instance.FadeIn(() => SceneManager.LoadScene("Level" + (index + 1))));

                if (levelButtonContainer.transform.childCount > index + 1)
                {
                    if (scoreData.lvlScores[i].isGameFinished)
                        levelButtonContainer.transform.GetChild(index + 1).GetComponent<Button>().interactable = true;
                    else
                        levelButtonContainer.transform.GetChild(index + 1).GetComponent<Button>().interactable = false;
                }
            }

            levelButtonContainer.transform.GetChild(0).GetComponent<Button>().interactable = true;
            backButton.onClick.AddListener(() => Transition.instance.FadeIn(() => SceneManager.LoadScene("MainMenu")));
        }
    }
}