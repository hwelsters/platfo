using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] public Texture2D arrowCursor;
    [SerializeField] public Texture2D handCursor;
    [SerializeField] public Texture2D pointCursor;

    private static CursorManager instance = null;
    
    private void Start()
    {
        instance = this;
        SetArrow();
    }

    public static void SetArrow()
    {
        Cursor.SetCursor(instance.arrowCursor, Vector2.zero, CursorMode.Auto);
    }
    
    public static void SetHand()
    {
        Cursor.SetCursor(instance.handCursor, Vector2.zero, CursorMode.Auto);
    }
    
    public static void SetClicked()
    {
        Cursor.SetCursor(instance.pointCursor, Vector2.zero, CursorMode.Auto);
    }
}
