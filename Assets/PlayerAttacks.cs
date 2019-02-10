using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    
    private float basicAttackDamage = 1.0f;

    [SerializeField]
    private GameObject attackSprite;

    private float attackCooldown = 0.4f;
    private float cooldownTimer = 0.0f;
    private bool attackAvailable = true;

    public float BasicAttackDamage
    {
        get { return basicAttackDamage; }
        private set { basicAttackDamage = value; }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(!attackAvailable)
        { 
            cooldownTimer += Time.deltaTime;
        }

        if(cooldownTimer > attackCooldown)
        {
            attackAvailable = true;
            cooldownTimer = 0.0f;
        }

        if(Input.GetButton("Fire1") && attackAvailable)
        {
            StartCoroutine(ShowAttackSprite());
            attackAvailable = false;
        }
    }

    IEnumerator ShowAttackSprite()
    {
        attackSprite.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        attackSprite.SetActive(false);
    }
}
