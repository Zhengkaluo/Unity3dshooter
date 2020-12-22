using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterGame : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource BackGroundMusic;
    //private AudioClip MusicClip;
    void Awake()
    {
        BackGroundMusic = GetComponent<AudioSource>();
        BackGroundMusic.Play();
    }

    private void PlayerDeathAction()
    {
        GetComponent<AudioSource>().enabled = false;
    }

    private void OnEnable()
    {
        PlayerHealth.PlayerDeathEvent += PlayerDeathAction;
    }
    private void OnDisable()
    {
        PlayerHealth.PlayerDeathEvent -= PlayerDeathAction;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
