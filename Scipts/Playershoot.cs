using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playershoot : MonoBehaviour
{
    public int damagePershot = 20;
    public float range = 100f;
    private Ray shootray;
    private RaycastHit shootHit;
    private int shootableMask;

    private Light GunLight;
    private ParticleSystem GunParticle;
    private AudioSource GunAudio;
    private LineRenderer GunLine;

    public float timeBetweenBullets = 0.15f;
    private float effectsDisplayTime = 0.2f;
    float timer;
    // Start is called before the first frame update
    private void Awake()
    {
        shootableMask = LayerMask.GetMask("Enemy");
        GunParticle = GetComponent<ParticleSystem>();
        GunLight = GetComponent<Light>();
        GunAudio = GetComponent<AudioSource>();
        GunLine = GetComponent<LineRenderer>();
    }

    private void shoot()
    {
        timer = 0f;
        GunAudio.Play();
        GunLight.enabled = true;
        GunParticle.Stop();
        GunParticle.Play();

        GunLine.enabled = true;
        GunLine.SetPosition(0, transform.position);
        shootray.origin = transform.position;
        shootray.direction = transform.forward;

        if(Physics.Raycast(shootray, out shootHit, range, shootableMask)) // range shootablemask 设定好了
        {
            EnemyHealth ThisEnemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
            ThisEnemyHealth.TakeDamage(damagePershot, shootHit.point);
            GunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            GunLine.SetPosition(1, shootray.origin + shootray.direction * range); //origin vector + direction vector
        }
    }
    // Update is called once per frame
    void DIsableEffects()
    {
        GunLine.enabled = false;
        GunLight.enabled = false;
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && timer >= timeBetweenBullets)
        {
            //Debug.Log("fire");
            shoot();
        } 
        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DIsableEffects();
        }
    }
}
