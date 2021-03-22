using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class BaseAttack : MonoBehaviour
{
    public bool isAttacking;
    [HideInInspector]
    public BaseWeapon Weapon = new NoWeapon();
    private List<BaseWeapon> weapons = new List<BaseWeapon>();
    [HideInInspector]
    internal Animator animator;

    float positiveSlope;
    float negativeSlope;
    public enum Direction
    {
        East,
        South,
        West,
        North
    }

    internal virtual void Start()
    {
        Vector2 lowerLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        Vector2 upperRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Vector2 upperLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height));
        Vector2 lowerRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0));

        positiveSlope = GetSlope(lowerLeft, upperRight);
        negativeSlope = GetSlope(upperLeft, lowerRight);

        InputManager.Instance.OnTap += Attack;
    }
    public void Attack(Vector3 pos)
    {
        isAttacking = true;
        if (CanAttack() && Weapon != null && Weapon.IsReady())
        {
            UpdateState(pos);
            Weapon.UseWeapon(pos);
        }
        isAttacking = false;
    }
    internal abstract void UpdateState(Vector3 pos);
    public void AddWeapon(BaseWeapon w)
    {
        weapons.Add(w);
        ChangeWeapon(w.GetType());
    }
    public void ChangeWeapon(Type t)
    {
        foreach (var weapon in weapons)
        {
            if (t == weapon.GetType())
            {
                Weapon = weapon;
            }
        }
    }
    internal Direction GetAttackDirection(Vector3 pos)
    {
        bool higherThanPositiveSlopeLine = HigherThanPositiveSlopeLine(pos);
        bool higherThanNegativeSlopeLine = HigherThanNegativeSlopeLine(pos);
        if (!higherThanPositiveSlopeLine && higherThanNegativeSlopeLine)
        {
            return Direction.East;
        }
        else if (!higherThanPositiveSlopeLine && !higherThanNegativeSlopeLine)
        {
            return Direction.South;
        }
        else if (higherThanPositiveSlopeLine && !higherThanNegativeSlopeLine)
        {
            return Direction.West;
        }
        else
        {
            return Direction.North;
        }
    }
    float GetSlope(Vector2 pointOne, Vector3 pointTwo)
    {
        return (pointTwo.y - pointOne.y) / (pointTwo.x - pointOne.x);
    }
    bool HigherThanPositiveSlopeLine(Vector3 inputPosition)
    {
        Vector2 playerPosition = gameObject.transform.position;
        //Vector2 mousePosition = Camera.main.ScreenToWorldPoint(inputPosition);
        float yIntercept = playerPosition.y - (positiveSlope * playerPosition.x);
        float inputIntercept = inputPosition.y - (positiveSlope * inputPosition.x);
        return inputIntercept > yIntercept;
    }
    bool HigherThanNegativeSlopeLine(Vector3 inputPosition)
    {
        Vector2 playerPosition = gameObject.transform.position;
        //Vector2 mousePosition = Camera.main.ScreenToWorldPoint(inputPosition);
        float yIntercept = playerPosition.y - (negativeSlope * playerPosition.x);
        float inputIntercept = inputPosition.y - (negativeSlope * inputPosition.x);
        return inputIntercept > yIntercept;
    }

    private bool CanAttack()
    {
        var mousePosition = Input.mousePosition;
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, -Vector2.up);
        bool can = true;
        if (hit.collider)
        {
            var obj = hit.collider.gameObject;
            if (obj && obj.layer == 5)
            {
                can = false;
                if (obj.CompareTag("Joystick"))
                {
                    if ((mousePosition - obj.transform.position).sqrMagnitude > 18000)
                    {
                        can = true;
                    }
                }
            }
        }
        //for slingshot max distance
        if ((transform.position - Camera.main.ScreenToWorldPoint(mousePosition)).sqrMagnitude > 200)
            can = false;

        return can;
    }
}
