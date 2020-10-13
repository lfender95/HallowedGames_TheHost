using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ai : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public bool hide = false;

    public LayerMask WhatGround, WhatPlayer;

    public Vector3 walkpoint;
    bool walkpointset;
    public float walkRange;

    public float sight, chase;
    public bool playerInsight, playerinattackrange;

    private void Awake()
    {
        player = GameObject.Find("pLAYER").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInsight = Physics.CheckSphere(transform.position, sight, WhatPlayer);
        playerinattackrange = Physics.CheckSphere(transform.position, chase, WhatPlayer);

        if (!playerInsight && !playerinattackrange) patroling();
        if (playerInsight && !playerinattackrange) ChasePLayer();
        if (playerInsight && playerinattackrange) Attackplayer();
    }

    private void patroling()
    {
        if (!walkpointset) searchWalkPoint();
        if (walkpointset)
            agent.SetDestination(walkpoint);

        Vector3 distanceToWalkPoint = transform.position - walkpoint;
        if (distanceToWalkPoint.magnitude < 1f)
            walkpointset = false;
    }
    private void searchWalkPoint()
    {
        float randomZ = Random.Range(-walkRange, walkRange);
        float randomX = Random.Range(-walkRange, walkRange);
        walkpoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkpoint, -transform.up, 2f, WhatGround))
            walkpointset = true;
    }
    private void ChasePLayer()
    {
     agent.SetDestination(player.position);
     
    }
    private void Attackplayer()
    {
     agent.SetDestination(transform.position);
     transform.LookAt(player);
    }
}
