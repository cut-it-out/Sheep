using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sheep : MonoBehaviour
{
    [Header("Runaway movement")]
    [SerializeField] float baseSpeed = 5f;
    [SerializeField] float runningSpeed = 8f;
    [SerializeField] float baseAcceleration = 12f;
    [SerializeField] float runningAcceleration = 20f;
    [SerializeField] float runningTime = 0.4f;

    [Header("Random movement")]
    [SerializeField] float randomMoveDistance = 5f;
    [SerializeField] float randomMoveTimerMin = 3f;
    [SerializeField] float randomMoveTimerMax = 6f;
    
    [Header("Player Distance")]
    [SerializeField] float sheepPlayerDistance = 3f;

    const float STOP_DELAY = 0.8f;

    private NavMeshAgent agent;
    private GameObject player;
    private bool isInTargetArea = false;
    private bool isSheepMovingAway = false;
    private Vector3 sheepMoveAwayTargetPos;

    Coroutine randomMove, sheepMoveAway;
    AudioManager audioManager;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        randomMove = StartCoroutine(RandomMove());
        audioManager = AudioManager.GetInstance();
    }

    private void Update()
    {
        //if (!isInTargetArea)
        //{
            float squaredDist = (transform.position - player.transform.position).sqrMagnitude;
            float sheepPlayerDistanceSqrt = sheepPlayerDistance * sheepPlayerDistance;

            if (squaredDist < sheepPlayerDistanceSqrt)
            {
                if (!isSheepMovingAway)
                {
                    Vector3 dirToPlayer = transform.position - player.transform.position;
                    sheepMoveAwayTargetPos = transform.position + dirToPlayer;

                    //agent.SetDestination(sheepMoveAwayTargetPos);
                    sheepMoveAway = StartCoroutine(MoveSheepAway());
                    
                    audioManager.PlaySheepSound();
                }
                
            }
        //}
        //else
        //{
        //    agent.velocity = Vector3.Lerp(agent.velocity, Vector3.zero,STOP_DELAY*4);
        //    //agent.isStopped = true;
            
        //    //Debug.Log("sheep stopped");
        //}
        
    }

    private IEnumerator MoveSheepAway()
    {
        isSheepMovingAway = true;
        agent.SetDestination(sheepMoveAwayTargetPos);
        agent.speed = runningSpeed;
        agent.acceleration = runningAcceleration;

        yield return new WaitForSeconds(runningTime);

        agent.speed = baseSpeed;
        agent.acceleration = baseAcceleration;
        isSheepMovingAway = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Target")
        {
            StopCoroutine(randomMove);
            StartCoroutine(SetInTarget());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        randomMove = StartCoroutine(RandomMove());
        isInTargetArea = false;
    }

    private IEnumerator SetInTarget()
    {
        yield return new WaitForSeconds(STOP_DELAY);
        isInTargetArea = true;
    }

    private IEnumerator RandomMove()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(randomMoveTimerMin, randomMoveTimerMax));

            if (!isSheepMovingAway)
            {
                if(RandomPoint(transform.position, randomMoveDistance, out Vector3 newPos))
                {
                    agent.SetDestination(newPos);
                }
            }

        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 1.0f, 1 << NavMesh.GetAreaFromName("Walkable")))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

}
