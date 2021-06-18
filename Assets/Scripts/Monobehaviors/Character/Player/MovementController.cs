using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float movementSpeed = 3.0f;
    Vector2 movement = new Vector2();
    Rigidbody2D rb2D;
    Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        UpdateState();
    }
    void FixedUpdate()
    {
        MoveCharacter();
    }
    private void MoveCharacter()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (UIManager.Instance.joystick && movement.x == 0 && movement.y == 0)
        {
            movement.x = UIManager.Instance.joystick.Horizontal;
            movement.y = UIManager.Instance.joystick.Vertical;
        }
        movement.Normalize();
        rb2D.velocity = movement * movementSpeed;
    }
    private void UpdateState()
    {

        if (Mathf.Approximately(movement.x, 0) && Mathf.Approximately(movement.y, 0))
        {
            animator.SetBool("isWalking", false);
        }
        else
        {
            animator.SetBool("isWalking", true);
            animator.SetFloat("xDir", movement.x);
            animator.SetFloat("yDir", movement.y);
        }
    }

}
