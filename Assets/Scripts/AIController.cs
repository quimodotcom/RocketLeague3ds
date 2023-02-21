using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms;

public class AIController : MonoBehaviour {

	public Transform currentTarget;
	private Transform oldTarget;
	private NavMeshAgent agent;

	public GameObject[] targets;
	public GameObject ball;

	void Start ()
	{
		agent = GetComponent<NavMeshAgent>();

        currentTarget = ball.transform;

        InvokeRepeating("GoToDest", 0, 2);
    }

	public void GoToDest () {
        agent.SetDestination(currentTarget.position);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            //hit ball
            currentTarget = targets[Random.Range(0, targets.Length)].transform;
        }
        else if(other.CompareTag("Player"))
        {
            //hit player or another ai
            if(currentTarget == ball.transform)
            {
                currentTarget = targets[Random.Range(0, targets.Length)].transform;
            }
            else
            {
                currentTarget = ball.transform;
            }
        }
        else
        {
            //boost
            currentTarget = ball.transform;
        }
    }
}
