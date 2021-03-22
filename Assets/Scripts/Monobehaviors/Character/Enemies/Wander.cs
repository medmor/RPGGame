using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Animator))]
public class Wander : MonoBehaviour
{
    public float pursuitSpeed;
    public float wanderSpeed;
    float currentSpeed;
    public float directionChangeInterval;
    public bool followPlayer;
    Coroutine moveCoroutine;
    Rigidbody2D rb2d;
    Animator animator;
    Transform targetTransform = null;
    Vector3 endPosition;
    float currentAngle = 0;

    private void Start()
    {
        currentSpeed = wanderSpeed;
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        StartCoroutine(WanderRoutine());
    }
    private void Update()
    {
    }
    public IEnumerator WanderRoutine()
    {
        while (true)
        {
            ChooseNewEndpoint();
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            moveCoroutine = StartCoroutine(Move(rb2d, currentSpeed));
            yield return new WaitForSeconds(directionChangeInterval);
        }
    }

    void ChooseNewEndpoint()
    {
        currentAngle += Random.Range(0, 360);
        currentAngle = Mathf.Repeat(currentAngle, 360);
        endPosition += Vector3FromAngle(currentAngle);
        if (endPosition.sqrMagnitude > 1)
            endPosition = transform.position + Vector3FromAngle(currentAngle);
    }

    Vector3 Vector3FromAngle(float inputAngleDegrees)
    {
        float inputAngleRadians = inputAngleDegrees * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(inputAngleRadians), Mathf.Sin(inputAngleRadians), 0);
    }

    public IEnumerator Move(Rigidbody2D rigidBodyToMove, float speed)
    {
        float remainingDistance = ((Vector2)(transform.position - endPosition)).sqrMagnitude;
        animator.SetFloat("xDir", endPosition.x - transform.position.x);
        animator.SetFloat("yDir", endPosition.y - transform.position.y);
        while (remainingDistance > float.Epsilon)
        {
            if (targetTransform != null)
            {
                endPosition = targetTransform.position;
                animator.SetFloat("xDir", endPosition.x - transform.position.x);
                animator.SetFloat("yDir", endPosition.y - transform.position.y);
            }
            if (rigidBodyToMove != null)
            {
                animator.SetBool("isWalking", true);
                Vector2 newPosition = Vector2.MoveTowards(rigidBodyToMove.position, endPosition, speed * Time.deltaTime);
                rb2d.MovePosition(newPosition);
                remainingDistance = ((Vector2)(transform.position - endPosition)).sqrMagnitude;
            }
            yield return new WaitForFixedUpdate();
        }
        animator.SetBool("isWalking", false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && followPlayer)
        {
            currentSpeed = pursuitSpeed;
            targetTransform = collision.gameObject.transform;
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            moveCoroutine = StartCoroutine(Move(rb2d, currentSpeed));
        }

    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("isWalking", false);
            currentSpeed = wanderSpeed;
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            targetTransform = null;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            endPosition *= -1;
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            moveCoroutine = StartCoroutine(Move(rb2d, currentSpeed));
        }
    }
}