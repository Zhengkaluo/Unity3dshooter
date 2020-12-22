using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPointManage : MonoBehaviour
{
    public GameObject OneEnemy;
    public float DelayTime = 2f;
    public float RepeatTime = 3f;
    public Transform[] SpawnPoints;
    private bool PlayerIsDead = false;

    private void PlayerDeathAction()
    {
        PlayerIsDead = true;
    }


    private void OnEnable()
    {
        PlayerHealth.PlayerDeathEvent += PlayerDeathAction;
    }
    private void OnDisable()
    {
        PlayerHealth.PlayerDeathEvent -= PlayerDeathAction;
    }

    private void Spawn()
    {
        if (PlayerIsDead == true)
        {
            CancelInvoke("Spawn");
            return;
            
        }
        int PointIndex = Random.Range(0, SpawnPoints.Length);
        Instantiate(OneEnemy, SpawnPoints[PointIndex].position, SpawnPoints[PointIndex].rotation);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", DelayTime, RepeatTime);
    }

    // Update is called once per frame

}
