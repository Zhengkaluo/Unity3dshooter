using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int StartHealth = 100;
    public Slider HealthSlide;
    private static int CurrentHealth;

    public AudioClip DeathClip;
    public Image DamageImage;
    public float FlashSpeed = 5f;
    public Color FlashColor = new Color(1f, 0f, 0f, 0.1f);
    private bool Damaged = false;
    private AudioSource PlayerAudio;

    public bool IsDead = false;
    private Animator DeathAnimator;
    public delegate void PlayerDeathAction();
    public static event PlayerDeathAction PlayerDeathEvent;
    // Start is called before the first frame update
    void Awake()
    {
        HealthSlide.maxValue = StartHealth;
        if (CurrentHealth <= 0)
        {
            //重新初始一次
            HealthSlide.value = StartHealth;
            CurrentHealth = StartHealth;
        }
        else
        {
            HealthSlide.value = StartHealth;
        }
        PlayerAudio = GetComponent<AudioSource>();
        DeathAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void TakeDamage(int amount)
    {
        if (IsDead)
        {
            return;
        }
        Damaged = true;
        PlayerAudio.Play();
        CurrentHealth -= amount;
        HealthSlide.value = CurrentHealth;
        if(CurrentHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        IsDead = true;
        PlayerAudio.clip = DeathClip;
        PlayerAudio.Play();
        DeathAnimator.SetTrigger("Death");

        GetComponent<PlayerMovement>().enabled = false;
        GetComponentInChildren<Playershoot>().enabled = false;
        if(PlayerDeathEvent != null)
        {
            PlayerDeathEvent();
        }
    }
    private void Update()
    {
        if (Damaged)
        {
            Damaged = false;
            DamageImage.color = this.FlashColor; 
        }
        else
        {
            DamageImage.color = Color.Lerp(DamageImage.color, Color.clear, Time.deltaTime * FlashSpeed);
        }
    }
}
