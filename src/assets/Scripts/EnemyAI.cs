using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public int health = 15;
    //public GameObject crate;
    private int _direction = 1;

    void Update()
    {
        if (transform.position.y < -5)
        {
            if (_direction > 0)
                transform.position = new Vector2(-2.33f, 4.74f);
            else if (_direction < 0)
                transform.position = new Vector2(1.8f, 4.84f);
        }
        if (health <= 0)
        {
            gameObject.SetActive(false);
            SpawnEnemies.SpawnCrate(transform);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Wall") || col.transform.CompareTag("Enemy"))
        {
            _direction *= -1;
        }
        if (col.transform.CompareTag("Grenade") || col.transform.CompareTag("Rocket"))
        {
            health = 0;
        }
    }

    void HitByGrenade()
    {
        // Do curved jump?
        health = 0;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.transform.tag)
        {
            case "Bullet": health -= 5; break;
            case "Rocket": health = 0; break;
        }
    }

    void FixedUpdate()
    {
        transform.Translate(new Vector3(_direction, 0, 0) * Time.fixedDeltaTime * 5f);
        transform.localScale = new Vector3(5 * _direction, 5, 1);
    }
}
