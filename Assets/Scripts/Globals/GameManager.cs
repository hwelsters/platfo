using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public delegate void OnWinHandler();
    public static event OnWinHandler OnWin;

    public static int itemsLeft = 0;
    public Coroutine rotateCoroutine = null;

    public static GameManager instance = null;

    public void RotateSceneCamera(float degrees)
    {
        if (rotateCoroutine != null) StopCoroutine(rotateCoroutine);
        rotateCoroutine = StartCoroutine(RotateCamera(degrees));
    }

    private IEnumerator RotateCamera(float degrees)
    {
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        Quaternion endRotation = Quaternion.Euler(0, 0, degrees);
        Quaternion currentRotation = mainCamera.transform.rotation;
        while (Mathf.Abs(endRotation.eulerAngles.z - currentRotation.eulerAngles.z) > float.Epsilon)
        {
            currentRotation = Quaternion.Lerp(currentRotation, endRotation, 0.05f);
            mainCamera.transform.rotation = currentRotation;
            yield return null;
        }
    }

    private void Start()
    {
        instance = this;
        Physics2D.gravity = Vector2.down * Physics2D.gravity.magnitude;
    }

    private void Update()
    {
        if (itemsLeft == 0)
        {
            Debug.Log("Won");
            if (OnWin != null)
                OnWin();
        }

        if (Input.GetKeyDown(KeyCode.R))
            Reset();
    }

    private void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
