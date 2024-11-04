using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player)
        {
            player.numCarrotSeed++;
            Destroy(this.gameObject);
        }
    }
}