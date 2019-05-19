using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{

    public float shakeAmount;
    public float shakeTimer;

    public Vector2 camSmoothing;

    Vector3 target;
    Vector2 velocity;

    void Start()
    {
        target = transform.position;
    }

    void FixedUpdate()
    {
        float posX = Mathf.SmoothDamp(transform.position.x, target.x, ref velocity.x, camSmoothing.x);
        float posY = Mathf.SmoothDamp(transform.position.y, target.y, ref velocity.y, camSmoothing.y);
        transform.position = new Vector3(Mathf.Clamp(posX, -.5f, 100.5f), Mathf.Clamp(posY, -200, 200), transform.position.z);
    }

    void Update()
    {
        if (shakeTimer > 0)
        {
            Vector2 shakePos = Random.insideUnitCircle * shakeAmount;
            transform.position = new Vector3(transform.position.x + shakePos.x, transform.position.y + shakePos.y, transform.position.z);
            shakeTimer -= Time.deltaTime;
        }
    }

    public void ShakeCamera(float power, float duration)
    {
        shakeAmount = power;
        shakeTimer = duration;
    }
}

