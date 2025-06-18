using System;
using FloxyDev.DialogueSystem;
using UnityEngine;

namespace ExpiProject.Npc
{
    public class NpcMaster : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] public int npcID;
        [SerializeField] private DialogueActivator dialogueActivator;
        [SerializeField] private float radius = 1f;
        
        [Header("Debug")] 
        [SerializeField] private bool debug;

        private bool hasDetectedPlayer;

        private void Update()
        {
            DetectPlayer();
        }

        private void DetectPlayer()
        {
            var hit = Physics2D.OverlapCircle(transform.position, radius, LayerMask.GetMask("Player"));

            if (hit != null && hit.CompareTag("Player") && !hasDetectedPlayer)
            {
                hasDetectedPlayer = true;
                GameManager.GameManager.instance.NpcTriggerArea(npcID);
                
                // if (!scoreSystem.savedData.lvlScores[scoreSystem.Level].quizPoint[playerID])
                // {
                //     interactButton.interactable = true;
                //     interactButton.onClick.AddListener(() => ButtonInteract());
                // }
            }
            else if (hit == null && hasDetectedPlayer)
            {
                hasDetectedPlayer = false;
                GameManager.GameManager.instance.NpcOutTriggerArea();
                
                // interactButton.interactable = false;
                // interactButton.onClick.RemoveListener(() => dialogController.OnDialogueTrigger(playerID));
            }
        }

        private void OnDrawGizmos()
        {
            if (!debug) return;
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}