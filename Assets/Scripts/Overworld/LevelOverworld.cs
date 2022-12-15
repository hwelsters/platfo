using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelOverworld : MonoBehaviour
{
    [SerializeField] private Sprite passiveSprite;
    [SerializeField] private Sprite activeSprite;
    [SerializeField] private Sprite passiveDoneSprite;
    [SerializeField] private Sprite activeDoneSprite;

    [SerializeField] private string level;

    [SerializeField] private bool complete;

    private SpriteRenderer _spriteRenderer;
    private bool _playerIsTouching;

    private void Start()
    {
        this._spriteRenderer = GetComponent<SpriteRenderer>();
        OnTriggerExit2D(null);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsComplete()) this._spriteRenderer.sprite = activeDoneSprite;
        else this._spriteRenderer.sprite = activeSprite;

        this._playerIsTouching = true;
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (IsComplete()) this._spriteRenderer.sprite = passiveDoneSprite;
        else this._spriteRenderer.sprite = passiveSprite;
        
        this._playerIsTouching = false;
    }

    private void Update()
    {
        if (PlayerPressed() && this._playerIsTouching) SceneManager.LoadScene(level, LoadSceneMode.Single);
    }

    private bool IsComplete()
    {
        return this.complete;
    }

    private bool PlayerPressed()
    {
        return Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space);
    }
}
