using UnityEngine;

namespace ExpiProject.Player.SO
{
    [CreateAssetMenu(menuName = "ScriptableObject/Player", fileName = "PlayerScriptableObject")]
    public class PlayerScriptableObject : ScriptableObject
    {
        [Header("PlayerDataCollectors")] [Range(0f, 100f)]
        public float soMaxMovementSpeed = 15f;

        [Range(0f, 200f)] public float soAccelerationMultiplier = 120f;
        [Range(0f, 100f)] public float soDecelerationMultiplier = 30f;
    }
}