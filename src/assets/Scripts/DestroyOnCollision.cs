using UnityEngine;
using System.Collections;

public class DestroyOnCollision : MonoBehaviour
{
    public float speed = 15f;
    private float direction;
    private Transform _player;

    void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void OnEnable()
    {
        speed = Random.Range(14f, 16f);
        if (_player.localScale.x < 0)
        {
            direction = -1f;
        }
        else
        {
            direction = 1f;
        }
    }

    void Update()
    {
        transform.Translate(new Vector3(direction, 0, 0) * Time.deltaTime * speed);
        transform.localScale = new Vector3(-7 * direction, 7, 1);
    }
}
