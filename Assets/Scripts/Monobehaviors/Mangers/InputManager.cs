﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Manager<InputManager>
{
    private Vector2 fingerDownPosition = default;
    private Vector2 fingerUpPosition = default;

   // [SerializeField]
    //private bool detectSwipeOnlyAfterRelease = false;

    [SerializeField]
    private float minDistanceForSwipe = 20f;

    public event Action<SwipeData> OnSwipe = delegate { };
    public event Action<Vector3> OnTap = delegate { };

    private void Update()
    {
        //foreach (Touch touch in Input.touches)
        //{
        //    if (touch.phase == TouchPhase.Began)
        //    {
        //        fingerUpPosition = touch.position;
        //        fingerDownPosition = touch.position;
        //    }

        //    if (!detectSwipeOnlyAfterRelease && touch.phase == TouchPhase.Moved)
        //    {
        //        fingerDownPosition = touch.position;
        //        DetectSwipe();
        //    }

        //    if (touch.phase == TouchPhase.Ended)
        //    {
        //        fingerDownPosition = touch.position;
        //        DetectSwipe();
        //    }
        //}

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SendSwipe(SwipeDirection.Left);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SendSwipe(SwipeDirection.Right);
        }
        if (Input.GetMouseButtonDown(0))
        {
            OnTap(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    private void DetectSwipe()
    {
        if (SwipeDistanceCheckMet())
        {
            if (IsVerticalSwipe())
            {
                var direction = fingerDownPosition.y - fingerUpPosition.y > 0 ? SwipeDirection.Up : SwipeDirection.Down;
                SendSwipe(direction);
            }
            else
            {
                var direction = fingerDownPosition.x - fingerUpPosition.x > 0 ? SwipeDirection.Right : SwipeDirection.Left;
                SendSwipe(direction);
            }
            fingerUpPosition = fingerDownPosition;
        }
        else
        {
            OnTap(fingerDownPosition);
        }
    }

    private bool IsVerticalSwipe()
    {
        return VerticalMovementDistance() > HorizontalMovementDistance();
    }

    private bool SwipeDistanceCheckMet()
    {
        return VerticalMovementDistance() > minDistanceForSwipe || HorizontalMovementDistance() > minDistanceForSwipe;
    }

    private float VerticalMovementDistance()
    {
        return Mathf.Abs(fingerDownPosition.y - fingerUpPosition.y);
    }

    private float HorizontalMovementDistance()
    {
        return Mathf.Abs(fingerDownPosition.x - fingerUpPosition.x);
    }

    private void SendSwipe(SwipeDirection direction)
    {
        SwipeData swipeData = new SwipeData()
        {
            Direction = direction,
            StartPosition = fingerDownPosition,
            EndPosition = fingerUpPosition
        };
        OnSwipe(swipeData);
    }
}

public struct SwipeData
{
    public Vector2 StartPosition;
    public Vector2 EndPosition;
    public SwipeDirection Direction;
}

public enum SwipeDirection
{
    Up,
    Down,
    Left,
    Right
}

