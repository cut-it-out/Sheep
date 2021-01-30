using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sheep : MonoBehaviour
{

    [Header("Random movement")]
    [SerializeField] float randomMoveDistance = 5f;
    [SerializeField] float randomMoveTimerMin = 3f;
    [SerializeField] float randomMoveTimerMax = 6f;
    
    [Header("Player Distance")]
    [SerializeField] float sheepPlayerDistance = 3f;

    [Header("Materials for testing")]
    [SerializeField] Material whiteMaterial;
    [SerializeField] Material redMaterial;


    private NavMeshAgent agent;
    private GameObject player;
    
    MeshRenderer meshRenderer;
    Coroutine randomMove;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        randomMove = StartCoroutine(RandomMove());
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        float squaredDist = (transform.position - player.transform.position).sqrMagnitude;
        float sheepPlayerDistanceSqrt = sheepPlayerDistance * sheepPlayerDistance;

        if (squaredDist < sheepPlayerDistanceSqrt)
        {
            Vector3 dirToPlayer = transform.position - player.transform.position;
            Vector3 newPos = transform.position + dirToPlayer;

            agent.SetDestination(newPos);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Target")
        {
            meshRenderer.material = redMaterial;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        meshRenderer.material = whiteMaterial;
    }

    private IEnumerator RandomMove()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(randomMoveTimerMin, randomMoveTimerMax));

            if(RandomPoint(transform.position, randomMoveDistance, out Vector3 newPos))
            {
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
