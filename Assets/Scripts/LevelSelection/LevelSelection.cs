using System;
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
            for (int i = 0; i < levelButtonContainer.transform.childCount; i++)
            {
                int index = i;
                levelButtonContainer.transform.GetChild(index).GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("Level"+index));
                levelButtonContainer.transform.GetChild(index).GetComponent<Button>().interactable = false;
            }
            levelButtonContainer.transform.GetChild(0).GetComponent<Button>().interactable = true;
            backButton.onClick.AddListener(() => Transition.instance.FadeIn(() => SceneManager.LoadScene("MainMenu")));
            
            Transition.instance.FadeOut();
        }
    }
}