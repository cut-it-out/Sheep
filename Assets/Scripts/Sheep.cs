using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sheep : MonoBehaviour
{
    [SerializeField] float randomMoveDistance = 5f;
    [SerializeField] float randomMoveTimerMin = 3f;
    [SerializeField] float randomMoveTimerMax = 6f;

    private NavMeshAgent agent;
    private SphereCollider senseCollider;
    Vector3 newPos;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        senseCollider = GetComponentInChildren<SphereCollider>();
        Coroutine randomMove = StartCoroutine(RandomMove());
    }

    private IEnumerator RandomMove()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(randomMoveTimerMin, randomMoveTimerMax));

            if(RandomPoint(gameObject.transform.position, randomMoveDistance, out newPos))
            {
                Debug.Log(newPos);
                agent.SetDestination(newPos);
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
