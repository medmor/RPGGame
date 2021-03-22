using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBox : MonoBehaviour
{
    Animator animator;
    public GameObject contentPrefab;
    bool opened = false;
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !opened)
        {
            opened = true;
            animator.enabled = true;
            var collectable = Instantiate(contentPrefab);
            collectable.transform.position = transform.position + Vector3.down * .75f;
        }
    }
}
