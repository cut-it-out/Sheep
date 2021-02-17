using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] LayerMask canBeClickedOn;
    [SerializeField] float keyboardMoveSpeed = 2f;

    NavMeshAgent agent;
    Game game;
    Animator anim;
    AudioManager audioManager;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        game = Game.GetInstance();
        audioManager = AudioManager.GetInstance();
    }

    private void Update()
    {
        if (!game.IsPaused)
        {
            
            float horInput = Input.GetAxis("Horizontal");
            float verInput = Input.GetAxis("Vertical");

            // Check for Keyboard movement
            if (Mathf.Abs(horInput) > 0 || Mathf.Abs(verInput) > 0)
            {
                Vector3 moveDestination = 
                    transform.position 
                    + Vector3.right * horInput  * keyboardMoveSpeed
                    + Vector3.forward * verInput * keyboardMoveSpeed;

                agent.SetDestination(moveDestination);
            }
            // Check for Mouse click movement
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hitInfo;

                    if (Physics.Raycast(ray, out hitInfo, 100, canBeClickedOn))
                    {
                        agent.SetDestination(hitInfo.point);
                    }
                }
            }
        }

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

    public void MovePlayer(Vector3 moveToPos, bool warp = false)
    {
        if (warp)
        {
            agent.Warp(moveToPos);
        }
        else
        {
            agent.SetDestination(moveToPos);
        }
    }
}
