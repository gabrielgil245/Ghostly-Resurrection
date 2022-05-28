using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalTrigger : MonoBehaviour
{
    [SerializeField] float _levelLoadDelay = 2f;
    [SerializeField] float _levelExitSlowMoFactor = 0.2f;
    public PlayerController _playerScript;

    // Update is called once per frame
    void Update()
    {
        if(_playerScript._isReallyDead) {
            GetComponent<Collider2D>().enabled = false;
        } else {
            GetComponent<Collider2D>().enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            StartCoroutine(LoadNextLevel());
        }
    }

    IEnumerator LoadNextLevel() {
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = _levelExitSlowMoFactor;
        yield return new WaitForSecondsRealtime(_levelLoadDelay);
        
        Time.timeScale = 1f;
        if(currentSceneIndex == sceneCount - 1) {
            SceneManager.LoadScene(0);
        } else {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }
}
