using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class HellephantHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public int ephantHealth = 300;
    private int CurrentHealth;
    private Animator EnemyAni;
    private bool IsDead;
    private bool IsSinking = false;

    public AudioClip DeathClip;
    private AudioSource EnemyAudio;
    private ParticleSystem HitParticles;

    public int ScorePerDeath = 50;

    // Start is called before the first frame update
    void Awake()
    {
        EnemyAni = GetComponent<Animator>();
        CurrentHealth = ephantHealth;
        EnemyAudio = GetComponent<AudioSource>();
        HitParticles = GetComponentInChildren<ParticleSystem>();
    }

    private void Death()
    {
        IsDead = true;
        EnemyAni.SetTrigger("Isdead");
        EnemyAudio.clip = DeathClip;
        EnemyAudio.Play();
        ScoreManage.score += ScorePerDeath;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<EnemyMove>().enabled = false;
        GetComponent<EnemyAttack>().enabled = false;
    }


    public void TakeDamage(int amount, UnityEngine.Vector3 position)
    {
        if (IsDead)
        {
            return;
        }
        EnemyAudio.Play();
        HitParticles.transform.position = position;
        HitParticles.Play();
        CurrentHealth -= amount;
        Debug.Log("Damage Taken");
        if (CurrentHealth <= 0)
        {
            Death();
        }
    }

    public void StartSinking()
    {
        IsSinking = true;
        Destroy(gameObject, 2f);//两秒
    }

    // Update is called once per frame
    void Update()
    {
        if (this.IsSinking == true)
        {
            transform.Translate(UnityEngine.Vector3.down * Time.deltaTime); //往下
        }
    }
}
