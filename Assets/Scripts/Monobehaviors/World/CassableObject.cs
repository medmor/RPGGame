using System.Collections;
using UnityEngine;
// 1
public class CassableObject : MonoBehaviour
{
    public float hitPoints;

    public IEnumerator Damage(int damage, float interval)
    {
        while (true)
        {
            StartCoroutine(FlickerCharacter());
            hitPoints = hitPoints - damage;
            if (hitPoints <= float.Epsilon)
            {
                Destroy(gameObject);
                break;
            }
            if (interval > float.Epsilon)
            {
                yield return new WaitForSeconds(interval);
            }
            else
            {
                break;
            }
        }
    }


    IEnumerator FlickerCharacter()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
