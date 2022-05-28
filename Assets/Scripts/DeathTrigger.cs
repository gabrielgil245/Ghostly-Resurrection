using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathTrigger : MonoBehaviour
{
    [SerializeField] float _levelLoadDelay = 2f;
    [SerializeField] float _levelExitSlowMoFactor = 0.2f;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            PlayerController player = other.GetComponent<PlayerController>();
            if(!player._isGhost) {
                player._isGhost = true;
            } else {
                player._isReallyDead = true;
                other.gameObject.SetActive(false);
                StartCoroutine(ResetLevel());
            }
        }
    }

    IEnumerator ResetLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = _levelExitSlowMoFactor;
        yield return new WaitForSecondsRealtime(_levelLoadDelay);
        
        Time.timeScale = 1f;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
