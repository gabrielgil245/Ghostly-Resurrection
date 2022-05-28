using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Light : MonoBehaviour
{
    [SerializeField] float _timer;
    private PlayerController _playerController;
    private SpriteRenderer _playerSprite;
    private bool _lightSwitch = true;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Waiting());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Waiting() {
        yield return new WaitForSecondsRealtime(_timer);
        Switching();
    }

    private void Switching() {
        _lightSwitch = !_lightSwitch;
        GetComponent<TilemapRenderer>().enabled = _lightSwitch;
        GetComponent<Collider2D>().enabled = _lightSwitch;
        StartCoroutine(Waiting());
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.tag == "Player") {
            _playerController = other.GetComponent<PlayerController>();
            if(_playerController._isGhost) {
                _playerController._isReallyDead = true;
                other.gameObject.SetActive(false);
                StartCoroutine(FindObjectOfType<DeathTrigger>().ResetLevel());
            } else {
                _playerSprite = other.GetComponent<SpriteRenderer>();
                _playerSprite.color = Color.magenta;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player" && other.gameObject.activeSelf) {
            _playerSprite.color = Color.red;
        }
    }

}
