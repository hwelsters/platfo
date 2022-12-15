using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private List<MenuItem> menuItems;
    private int selectedIndex = -1;

    private const int START = 0;
    private const int LOAD = 1;
    private const int EXIT = 2;

    private void Start()
    {
        for (int i = 0; i < menuItems.Count; i++)
        {
            menuItems[i].SetIndex(this, i);
        }
        SetSelectedIndex(0);
    }

    private void Update()
    {
        if (IsPressedUp())
        {
            SetSelectedIndex(selectedIndex - 1);
        }
        else if (IsPressedDown())
        {
            SetSelectedIndex(selectedIndex + 1);
        } else if (IsPressedSelect()) 
        {
            DoAction();
        }
    }

    private bool IsPressedUp()
    {
        return Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
    }

    private bool IsPressedDown()
    {
        return Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);
    }

    private bool IsPressedSelect()
    {
        return Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space);
    }

    private void DoAction()
    {
        switch (selectedIndex)
        {
            case START:
                SceneManager.LoadScene("Scenes/Intro/Overworld", LoadSceneMode.Single);
                break;
            case LOAD:
                break;
            case EXIT:
                Application.Quit();
                break;
        }
    }

    public void SetSelectedIndex(int nextIndex)
    {
        if (nextIndex == this.selectedIndex) DoAction();
        this.selectedIndex = Mathf.Clamp(this.selectedIndex, 0, menuItems.Count - 1);
        nextIndex = Mathf.Clamp(nextIndex, 0, menuItems.Count - 1);

        menuItems[this.selectedIndex].SetSelected(false);
        menuItems[nextIndex].SetSelected(true);

        this.selectedIndex = nextIndex;
    }
}
