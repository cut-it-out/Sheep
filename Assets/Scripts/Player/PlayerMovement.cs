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

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        game = Game.GetInstance();
    }

    private void Update()
    {
        if (!game.IsPaused)
        {
            float horInput = Input.GetAxis("Horizontal");
            float verInput = Input.GetAxis("Vertical");

            if(Mathf.Abs(horInput) > 0 || Mathf.Abs(verInput) > 0)
            {
                Vector3 moveDestination = 
                    transform.position 
                    + Vector3.right * horInput  * keyboardMoveSpeed
                    + Vector3.forward * verInput * keyboardMoveSpeed;

                agent.SetDestination(moveDestination);
            }
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


        // TODO maybe move it to separate script?
        // handling walk Animation 
        if (Math.Abs(agent.velocity.x + agent.velocity.y + agent.velocity.z) > .05f)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
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
