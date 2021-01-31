using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] LayerMask canBeClickedOn;
    [SerializeField] float keyboardForwardSpeed = 2f;

    NavMeshAgent agent;
    Game game;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
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
                Vector3 moveDestination = transform.position + transform.right * horInput + transform.forward * verInput * keyboardForwardSpeed;

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

    }
}
