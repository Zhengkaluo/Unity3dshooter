using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyMove : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent mynav;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        mynav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        mynav.destination = player.position;
    }
}
