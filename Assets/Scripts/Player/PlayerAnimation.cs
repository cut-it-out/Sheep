using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] float funnyIdleTriggerDelay = 3f;

    NavMeshAgent agent;
    Animator anim;
    AudioManager audioManager;

    Coroutine funIdleMovement;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        audioManager = AudioManager.GetInstance();

        //funIdleMovement = StartCoroutine(FunIdle());
    }

    private void OnEnable()
    {
        if (funIdleMovement != null)
        {
            StopCoroutine(funIdleMovement);
        }
        funIdleMovement = StartCoroutine(FunIdle());
    }

    void Update()
    {

        // handling walk Animation and Sound
        if (agent.velocity.magnitude > 0.1f)
        {
            anim.SetBool("isWalking", true);
            audioManager.PlayWalkingSound(true);
        }
        else
        {
            anim.SetBool("isWalking", false);
            audioManager.PlayWalkingSound(false);

        }
    }

    private IEnumerator FunIdle()
    {
        while (true)
        {
            yield return new WaitForSeconds(funnyIdleTriggerDelay);
            anim.SetInteger("funIdle", Random.Range(1,3));
            yield return new WaitForSeconds(0.2f);
            anim.SetInteger("funIdle", 0);
        }
    }
}
