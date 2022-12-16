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
    public Coroutine shakeCoroutine = null;

    public static GameManager instance = null;

    private const float SCREEN_SHAKE_CYCLES = 2f;
    private const float SCREEN_SHAKE_CYCLES_RADIANS = SCREEN_SHAKE_CYCLES * 2f * Mathf.PI;
    private const float SCREEN_SHAKE_SPEED = 100f;
    private const float SCREEN_SHAKE_MAGNITUDE = 0.075f;

    public void RotateSceneCamera(float degrees)
    {
        if (rotateCoroutine != null) StopCoroutine(rotateCoroutine);
        rotateCoroutine = StartCoroutine(RotateCamera(degrees));
    }

    public void ScreenShake()
    {
        if (shakeCoroutine != null) StopCoroutine(shakeCoroutine);
        shakeCoroutine = StartCoroutine(ScreenShakeCoroutine());
    }

    private IEnumerator ScreenShakeCoroutine()
    {
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        Vector3 mainCameraOriginalPosition = mainCamera.transform.position;

        float currentRadian = 0f;
        while (currentRadian < SCREEN_SHAKE_CYCLES_RADIANS)
        {
            float displacement = Mathf.Sin(currentRadian) * SCREEN_SHAKE_MAGNITUDE;
            mainCamera.transform.position = mainCameraOriginalPosition + new Vector3(displacement, 0, 0);
            currentRadian += SCREEN_SHAKE_SPEED * Time.deltaTime;
            yield return null;
        }
        mainCamera.transform.position = mainCameraOriginalPosition;
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
    }

    private void Update()
    {
        if (itemsLeft == 0)
        {
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
