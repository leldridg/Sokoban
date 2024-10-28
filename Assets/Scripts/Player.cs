using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Animator animator;
    public LayerMask unwalkableLayer;
    public LayerMask moveableLayer;
    public float speed;
    private Vector2 movement;
    private string lastDirection = "Down";
    private Vector3 targetPosition;

    void Awake()
    {
        targetPosition = transform.position;
    }
    
    void Update()
    {   
        Vector3 projectedPosition = transform.position + (Vector3)movement;
        if (projectedPosition.x == targetPosition.x || projectedPosition.y == targetPosition.y) //not diagonal
        {
            HandleAnimations();
            if (Vector3.Distance(transform.position, targetPosition) < .0001f
             && !Physics2D.OverlapCircle(projectedPosition, .1f, unwalkableLayer)) //target location reached, and next location not unwalkable
            { 
                Collider2D other = Physics2D.OverlapCircle(projectedPosition, .1f, moveableLayer); //next location moveable
                if (other)
                {
                    if (!Physics2D.OverlapCircle(targetPosition + 2 * (Vector3)movement, .1f, unwalkableLayer)
                     && !Physics2D.OverlapCircle(targetPosition + 2 * (Vector3)movement, .1f, moveableLayer)) //location after moveable not unwalkwable or moveable
                    {
                        targetPosition = projectedPosition;
                        if (Vector3.Distance(other.gameObject.transform.position, other.gameObject.GetComponent<Basket>().targetPosition) < .0001f) //location after moveable not unwalkwable
                        {
                            other.gameObject.GetComponent<Basket>().targetPosition = new Vector3(targetPosition.x + movement.x, targetPosition.y + movement.y, 0f);
                        }
                    }
                }
                else
                {
                    targetPosition = projectedPosition;
                }
            }
        }
        //move player towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
    }

    void HandleAnimations()
    {
        string animationName = "";

        if (movement == Vector2.zero)
        {
            animationName = "Idle" + lastDirection;
        }
        else
        {
            animationName = "Walk";
            if (movement.y > .01f)
            {
                lastDirection = "Up";
            }
            else if (movement.y < -.01f)
            {
                lastDirection = "Down";
            }
            else if (movement.x > .01f)
            {
                lastDirection = "Right";
            }
            else if (movement.x < -0.01f)
            {
                lastDirection = "Left";
            }
            animationName += lastDirection;
        }

        Debug.Log("Animation name: " + animationName);
        animator.Play(animationName);
    }
}
