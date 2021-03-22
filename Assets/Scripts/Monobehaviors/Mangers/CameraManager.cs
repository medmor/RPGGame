using UnityEngine;
// 1
using Cinemachine;
using System;

public class CameraManager : Manager<CameraManager>
{
    public CinemachineVirtualCamera VirtualCamera;
    public CinemachineConfiner Confiner;

    public void SetFollower(Transform t)
    {
        VirtualCamera.Follow = t;
    }

    public void SetConfiner(Collider2D c)
    {
        Confiner.m_BoundingShape2D = c;
    }

}
