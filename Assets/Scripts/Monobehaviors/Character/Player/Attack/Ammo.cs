using UnityEngine;
public class Ammo : MonoBehaviour
{
    public int damageInflicted;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && collision.GetType() == typeof(BoxCollider2D))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            StartCoroutine(enemy.DamageCharacter(damageInflicted, 0.0f));
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("CassableObject"))
        {
            var obj = collision.gameObject.GetComponent<CassableObject>();
            StartCoroutine(obj.Damage(damageInflicted, 0));
            gameObject.SetActive(false);
        }
    }
}