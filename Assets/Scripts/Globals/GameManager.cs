using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public delegate void OnWinHandler();
    public static event OnWinHandler OnWin;
    
    public static int itemsLeft = 0;


    public static void RotateSceneCamera(float degrees)
    {
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        mainCamera.transform.rotation = Quaternion.Euler(0, 0, degrees);
    }

    private void Update()
    {
        if (itemsLeft == 0) 
        {
            Debug.Log("Won");
            if (OnWin != null) OnWin();

            // REMOVE DURING PRODUCTION
            itemsLeft--;
        }

        if (Input.GetKeyDown(KeyCode.R)) Reset();
    }

    private void Reset()
    {

    }

}
