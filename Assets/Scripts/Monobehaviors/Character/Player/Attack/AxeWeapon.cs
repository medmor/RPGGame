using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeWeapon : BaseWeapon
{
    public GameObject ammoPrefab;
    GameObject axeShot;
    public AxeWeapon(BaseAttack p)
    {
        AnimationBoolName = "isHitting";
        Parent = p;
        axeShot = MonoBehaviour.Instantiate(Resources.Load("AmmoObject")) as GameObject;
        axeShot.transform.parent = Parent.transform;

        axeShot.GetComponent<Renderer>().enabled = false;
        axeShot.SetActive(false);
    }
    internal override void UseWeapon(Vector3 mousePos)
    {
        Vector2 dir = mousePos - Parent.transform.position;
        axeShot.transform.position = Parent.transform.position;
        axeShot.transform.Translate(dir.normalized * .4f);
        axeShot.SetActive(true);
        Parent.StartCoroutine(disableAxeShot());
    }

    private IEnumerator disableAxeShot()
    {
        yield return new WaitForSeconds(.2f);
        axeShot.SetActive(false);
    }

    internal override bool IsReady()
    {
        return true;
    }
}
