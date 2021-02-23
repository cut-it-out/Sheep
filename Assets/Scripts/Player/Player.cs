using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void MoveToStartPosition(Transform tf)
    {
        gameObject.transform.position = tf.position;
        gameObject.transform.rotation = tf.rotation;
        playerMovement.MovePlayer(tf.position);
    }
    
    public void WarpToStartPosition(Transform tf)
    {
        gameObject.transform.position = tf.position;
        gameObject.transform.rotation = tf.rotation;
        if (playerMovement)
        {
            Debug.Log("playermovement is ok");
        } else
            Debug.LogWarning("playermovement missing!!!!!!!!!!!!!!!!");
        playerMovement.MovePlayer(tf.position, true);
    }
}
