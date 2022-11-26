using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private List<MenuItem> menuItems;
    private int selectedIndex = 0;

    private void Start()
    {
        for (int i = 0; i < menuItems.Count; i++) { menuItems[i].SetIndex(this, i); }
        SetSelectedIndex(0);
    }

    private void Update()
    {
        if (IsPressedUp()) SetSelectedIndex(selectedIndex - 1);
        else if (IsPressedDown()) SetSelectedIndex(selectedIndex + 1);
    }

    private bool IsPressedUp () 
    {
        return Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
    }
    
    private bool IsPressedDown () 
    {
        return Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);
    }

    public void SetSelectedIndex(int nextIndex)
    {
        Debug.Log("Called");
        Debug.Log(nextIndex);

        this.selectedIndex = Mathf.Clamp(this.selectedIndex, 0, menuItems.Count - 1);
        nextIndex = Mathf.Clamp(nextIndex, 0, menuItems.Count - 1);

        menuItems[this.selectedIndex].SetSelected(false);
        menuItems[nextIndex].SetSelected(true);

        this.selectedIndex = nextIndex;
    }
}
