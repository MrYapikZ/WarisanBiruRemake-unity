using ExpiProject.Player.SO;
using UnityEngine;

namespace ExpiProject.Player
{
    public class PlayerMaster : MonoBehaviour
    {
        [Header("Components")] [SerializeField]
        private VariableJoystick variableJoystick;

        [SerializeField] private Rigidbody2D rigidBody;
        [SerializeField] private Animator animator;

        [Header("References")] [SerializeField]
        private PlayerScriptableObject playerData;

        private bool isFacingRight;

        private Vector2 keyboardMovement;
        private Vector2 lastMovement;
        private Vector2 movement;

        private Vector2 velocity;

        private void FixedUpdate()
        {
            MovementInput();

            rigidBody.linearVelocity = velocity;
        }

        private void MovementInput()
        {
            var direction = Vector2.up * variableJoystick.Vertical + Vector2.right * variableJoystick.Horizontal;
            movement = direction != Vector2.zero ? direction.normalized : keyboardMovement.normalized;
            if (movement != Vector2.zero)
            {
                lastMovement = movement;
                animator.SetBool("IsMoving", movement.sqrMagnitude > 0);
                animator.SetFloat("MoveX", movement.x);
                animator.SetFloat("MoveY", movement.y);

                var deltaX = Mathf.MoveTowards(velocity.x,
                    movement.x *
                    playerData.soMaxMovementSpeed,
                    playerData.soAccelerationMultiplier * Time.deltaTime);
                var deltaY = Mathf.MoveTowards(velocity.y, movement.y * playerData.soMaxMovementSpeed,
                    playerData.soAccelerationMultiplier * Time.deltaTime);
                velocity = new Vector2(deltaX, deltaY);
            }
            else
            {
                animator.SetBool("IsMoving", false);
                animator.SetFloat("MoveX", lastMovement.x);
                animator.SetFloat("MoveY", lastMovement.y);

                var deltaX = Mathf.MoveTowards(velocity.x, 0, playerData.soDecelerationMultiplier * Time.deltaTime);
                var deltaY = Mathf.MoveTowards(velocity.y, 0, playerData.soAccelerationMultiplier * Time.deltaTime);
                velocity = new Vector2(deltaX, deltaY);
            }
        }
    }
}