using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (!"Player".Equals(col.tag))
        {
            Camera.main.GetComponent<CameraShake>().ShakeCamera(Random.Range(0f, 0.05f), Random.Range(0.2f, 0.4f));
            gameObject.SetActive(false);
        }
    }
}
