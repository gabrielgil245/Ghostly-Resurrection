using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            PlayerController player = other.GetComponent<PlayerController>();
            if(player._isGhost == false) {
                player._isGhost = true;
            } else {
                Debug.Log("Player is officially dead...");
                other.gameObject.SetActive(false);
            }
        }
    }
}
