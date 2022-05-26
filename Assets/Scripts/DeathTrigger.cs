using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            if(other.GetComponent<PlayerController>()._isGhost == false) {
                other.GetComponent<PlayerController>()._isGhost = true;
            } else {
                Debug.Log("Player is officially dead...");
            }
        }
    }
}
