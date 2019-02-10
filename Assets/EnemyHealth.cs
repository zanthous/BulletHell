using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private float health;
    private float lastTime = float.MinValue;

    IEnumerator TurnRed()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(.15f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(Time.time - lastTime > 0.35f)
        { 
            if(collision.tag == "PlayerBasicAttack")
            {
                health -= collision.transform.parent.GetComponent<PlayerAttacks>().BasicAttackDamage;
                if(health <= 0.0f)
                {
                    Destroy(gameObject);
                }
                StartCoroutine(TurnRed());
                lastTime = Time.time;
            }
        }
    }
}
