using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    private CameraShake _camera;

    void Awake()
    {
        _camera = Camera.main.GetComponent<CameraShake>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag != "Player")
        {
            _camera.ShakeCamera(Random.Range(0f, 0.05f), Random.Range(0.2f, 0.4f));
            gameObject.SetActive(false);
        }
    }
}
