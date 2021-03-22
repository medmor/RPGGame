using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : BaseAttack
{

    internal override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        isAttacking = false;

        Weapon = new NoWeapon();
    }
   
    internal override void UpdateState(Vector3 pos)
    {
        var dir = GetAttackDirection(pos);
        if (isAttacking)
        {
            Vector2 dirVector;
            switch (dir)
            {
                case Direction.East:
                    dirVector = new Vector2(1.0f, 0.0f);
                    break;
                case Direction.South:
                    dirVector = new Vector2(0.0f, -1.0f);
                    break;
                case Direction.West:
                    dirVector = new Vector2(-1.0f, 1.0f);
                    break;
                case Direction.North:
                    dirVector = new Vector2(0.0f, 1.0f);
                    break;
                default:
                    dirVector = new Vector2(0.0f, 0.0f);
                    break;
            }
            if (!string.IsNullOrWhiteSpace(Weapon.AnimationBoolName))
                animator.SetBool(Weapon.AnimationBoolName, true);
            animator.SetFloat("xDir", dirVector.x);
            animator.SetFloat("yDir", dirVector.y);
        }

        StartCoroutine(ResetAnimatorBool());

    }
    IEnumerator ResetAnimatorBool()
    {
        yield return new WaitForSeconds(.1f);
        if (!string.IsNullOrWhiteSpace(Weapon.AnimationBoolName))
            animator.SetBool(Weapon.AnimationBoolName, false);
    }

}
