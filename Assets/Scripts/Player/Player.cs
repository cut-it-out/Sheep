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
}
