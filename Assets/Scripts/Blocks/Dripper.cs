using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dripper : MonoBehaviour
{
    private const float DELAY = 1f;
    [SerializeField] private GameObject rainDrop;

    private bool isDripping = false;

    private void Update()
    {
        if (isDripping) return;

        isDripping = true;
        Instantiate(rainDrop, transform.position, Quaternion.identity);
        StartCoroutine(Drip());
    }

    private IEnumerator Drip()
    {
        yield return new WaitForSeconds(DELAY);
        isDripping = false;
    }
}
