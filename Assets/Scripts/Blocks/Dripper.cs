using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dripper : MonoBehaviour
{
    private const float DELAY = 1f;
    [SerializeField] private GameObject _rainDrop;

    private bool _isDripping = false;

    private void Update()
    {
        if (this._isDripping) return;

        this._isDripping = true;
        Instantiate(this._rainDrop, transform.position, Quaternion.identity);
        StartCoroutine(Drip());
    }

    private IEnumerator Drip()
    {
        yield return new WaitForSeconds(DELAY);
        this._isDripping = false;
    }
}
