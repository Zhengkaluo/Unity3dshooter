using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms.Impl;

public class EnemyHealth : MonoBehaviour
{
    public int StartHealth = 100;
    private int CurrentHealth;
    private Animator EnemyAni;
    private bool IsDead;
    private bool IsSinking = false;

    public AudioClip DeathClip;
    private AudioSource EnemyAudio;
    private ParticleSystem HitParticles;

    public int ScorePerDeath = 10;

    // Start is called before the first frame update
    void Awake()
    {
        EnemyAni = GetComponent<Animator>();
        CurrentHealth = StartHealth;
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
        Debug.Log("Taken Damage");
        if(CurrentHealth <= 0)
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
         if(this.IsSinking == true)
        {
            transform.Translate(UnityEngine.Vector3.down * Time.deltaTime); //往下
        }
    }
}
