using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon
{
    internal string AnimationBoolName;
    internal BaseAttack Parent;
    internal abstract void UseWeapon(Vector3 mousePos);

    internal virtual bool IsReady()
    {
        return false;
    }
}
