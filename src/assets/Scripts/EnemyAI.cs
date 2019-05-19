using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public int health = 15;
    public GameObject crate;
    private int direction = 1;

    void Update()
    {
        if (transform.position.y < -5)
        {
            if (direction > 0)
                transform.position = new Vector2(-2.33f, 4.74f);
            else if (direction < 0)
                transform.position = new Vector2(1.8f, 4.84f);
        }
        if (health <= 0)
        {
            Instantiate(crate, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag.Equals("Wall") || col.transform.tag.Equals("Enemy"))
        {
            direction *= -1;
        }
        if (col.transform.tag.Equals("Grenade"))
        {
            health = 0;
        }
        if (col.transform.tag.Equals("Rocket"))
        {
            health = 0;
        }
    }

    void HitByGrenade()
    {
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
        transform.Translate(new Vector3(direction, 0, 0) * Time.deltaTime * 5f);
        transform.localScale = new Vector3(5 * direction, 5, 1);
    }
}
