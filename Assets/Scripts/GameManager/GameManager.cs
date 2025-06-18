using ExpiProject.Npc;
using ExpiProject.Others;
using FloxyDev.DialogueSystem;
using UnityEngine;
using UnityEngine.UI;

namespace ExpiProject.GameManager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        [Header("UI References")] [SerializeField]
        private GameObject pausePanelUI;

        [SerializeField] private Button pauseButtonUI;
        [SerializeField] private GameObject playerControllerUI;
        [SerializeField] private Button interactButton;

        [Header("Game References")] [SerializeField]
        private int level;

        [SerializeField] private GameObject[] npcObjects;


        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            Transition.instance.FadeOut();
        }

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
    }
}