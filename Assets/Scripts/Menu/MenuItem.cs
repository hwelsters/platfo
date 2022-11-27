using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuItem : MonoBehaviour
{
    [SerializeField] private Sprite notSelectedSprite;
    [SerializeField] private Sprite selectedSprite;
    
    private int index;
    private MenuManager menuManager;
    private SpriteRenderer spriteRenderer;

    public void SetSelected(bool isSelected)
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        if (isSelected) this.spriteRenderer.sprite = selectedSprite;
        else this.spriteRenderer.sprite = notSelectedSprite;
    }

    public void SetIndex(MenuManager menuManager, int index)
    {
        this.menuManager = menuManager;
        this.index = index;
    }

    public void OnMouseOver()
    {
        if (Input.GetMouseButton(0)) 
        {
            CursorManager.SetClicked();
            this.menuManager.SetSelectedIndex(this.index);
        }
        else CursorManager.SetHand();
    }

    public void OnMouseExit()
    {
        CursorManager.SetArrow();
    }
}
