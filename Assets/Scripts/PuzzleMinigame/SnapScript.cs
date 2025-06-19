using UnityEngine;

namespace ExpiProject.PuzzleMinigame
{
    public class SnapScript : MonoBehaviour
    {
        [HideInInspector] public Transform targetBoard;
        private float deltaX, deltaY;
        private Vector2 initialPosition;
        private bool locked;
        private SpriteRenderer sprite;

        private void Start()
        {
            sprite = GetComponent<SpriteRenderer>();
            initialPosition = transform.position;
        }

        private void Update()
        {
            if (Input.touchCount > 0 && !locked && Camera.main)
            {
                var touch = Input.GetTouch(0);
                Vector2 touchpos = Camera.main.ScreenToWorldPoint(touch.position);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchpos))
                        {
                            sprite.sortingOrder = 2;
                            deltaX = touchpos.x - transform.position.x;
                            deltaY = touchpos.y - transform.position.y;
                        }

                        break;

                    case TouchPhase.Moved:
                        if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchpos))
                            transform.position = new Vector2(touchpos.x - deltaX, touchpos.y - deltaY);
                        else
                            transform.position = new Vector2(initialPosition.x, initialPosition.y);
                        break;


                    case TouchPhase.Ended:
                        if (Mathf.Abs(transform.position.x - targetBoard.position.x) <= 0.5f &&
                            Mathf.Abs(transform.position.y - targetBoard.position.y) <= 0.5f)
                        {
                            transform.position = new Vector2(targetBoard.position.x, targetBoard.position.y);
                            locked = true;
                            sprite.sortingOrder = 1;
                            PuzzleManager.instance.AddPoint();
                        }
                        else
                        {
                            transform.position = new Vector2(initialPosition.x, initialPosition.y);
                        }

                        break;
                }
            }
        }
    }
}