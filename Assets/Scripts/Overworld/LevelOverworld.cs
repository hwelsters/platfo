using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelOverworld : MonoBehaviour
{
    [SerializeField] private Sprite passiveSprite;
    [SerializeField] private Sprite activeSprite;
    [SerializeField] private string level;

    private SpriteRenderer _spriteRenderer;
    private bool _playerIsTouching;

    private void Start()
    {
        this._spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        this._spriteRenderer.sprite = activeSprite;
        this._playerIsTouching = true;
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        this._spriteRenderer.sprite = passiveSprite;
        this._playerIsTouching = false;
    }

    private void Update()
    {
        if (PlayerPressed() && this._playerIsTouching) SceneManager.LoadScene(level, LoadSceneMode.Single);
    }

    private bool PlayerPressed()
    {
        return Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space);
    }
}
