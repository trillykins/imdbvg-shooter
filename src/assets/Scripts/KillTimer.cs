using UnityEngine;
using System.Collections;
using System;

public class KillTimer : MonoBehaviour
{

    private float _disableCountdown = .2f;

    void OnEnable()
    {
        _disableCountdown = .2f;
    }

    void FixedUpdate()
    {
        _disableCountdown -= Time.deltaTime;
        if (_disableCountdown <= 0f)
        {
            gameObject.SetActive(false);
        }
    }
}
