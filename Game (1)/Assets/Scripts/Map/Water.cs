using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{

    private int _slow = 50;
    private int _usual = 200;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerMovement>(out PlayerMovement player))
        {
            player.SpeedChange(_slow);
        }
        else if (collision.TryGetComponent<MoveState>(out MoveState enemy))
        {
            enemy.SpeedChange(_slow);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerMovement>(out PlayerMovement player))
        {
            player.SpeedChange(_usual);
        }
        else if (collision.TryGetComponent<MoveState>(out MoveState enemy))
        {
            enemy.SpeedChange(_usual);
        }
    }
}
