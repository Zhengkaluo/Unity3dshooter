using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public int AttackDamage = 10;
    private bool PlayerInRange; //可攻击范围
    private PlayerHealth ThisPlayerHealth; //可以呼叫 takedamage 方法 ThisPlayerHealth.takedmage
    private float Timer;
    private float TimeBetweenAttack = 0.5f;

    private Animator EnemyAnimator;
    private bool PlayerIsDead = false;
    void Awake()
    {
        GameObject ThisPlayer = GameObject.FindGameObjectWithTag("Player");
        ThisPlayerHealth = ThisPlayer.GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == ThisPlayerHealth.tag)
        {
            PlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == ThisPlayerHealth.tag)
        {
            PlayerInRange = false;
        }
    }

    private void Attack()
    {
        ThisPlayerHealth.TakeDamage(AttackDamage);
        Timer = 0;
        EnemyAnimator = GetComponent<Animator>();
        
    }

    private void playerDeathAction()
    {
        this.PlayerIsDead = true;
        EnemyAnimator.SetTrigger("Playerisdead");
        GetComponent<EnemyMove>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
    }

    private void OnEnable()
    {
        PlayerHealth.PlayerDeathEvent += playerDeathAction;

    }
    private void OnDisable()
    {
        PlayerHealth.PlayerDeathEvent -= playerDeathAction;

    }
    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if(PlayerInRange && PlayerIsDead == false)
        {
            if (Timer >= TimeBetweenAttack)
            {
                Attack();
            }
        }   
    }
}
