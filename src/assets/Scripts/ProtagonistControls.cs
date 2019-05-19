using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Linq;

public class ProtagonistControls : MonoBehaviour
{
    public ChooseLevel ChooseLevel;
    public Transform Respawn;
    public GameObject explosion;
    public float movementSpeed = 1f;
    public Canvas deathScreen;
    public Button retry;
    public bool isDead = false;

    private AudioSource boom;
    private Character character;
    private bool _grounded;
    private int _direction;
    private int ZukoVariable = 1;

    void Awake()
    {
        character = GameController.Character;
        boom = GetComponent<AudioSource>();
    }

    void Start()
    {
        deathScreen = deathScreen.GetComponent<Canvas>();
        retry = retry.GetComponent<Button>();
        deathScreen.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isDead) return;
        switch (character?.Name)
        {
            case "HeisenbergLives":
                if (GameObject.FindGameObjectsWithTag("Enemy").Length > 10)
                    Death();
                break;
            case "nana_strikes":
                if (Random.Range(1, 1000) > 995)
                {
                    Explode();
                }
                break;
            case "Firelord_Zuko":
                if (Random.Range(1, 1000) > 995)
                {
                    ZukoVariable *= -1;
                }
                break;
            default: break;
        }

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            if (Input.GetAxisRaw("Horizontal") > 0)
                _direction = 1;
            else
                _direction = -1;
            transform.Translate(new Vector3(_direction * ZukoVariable, 0, 0) * Time.deltaTime * movementSpeed);
            transform.localScale = new Vector3(-5 * _direction * ZukoVariable, 5, 1);
        }
    }

    void Update()
    {
        if (!isDead)
        {
            if (Input.GetButtonDown("Jump") && _grounded)
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                GetComponent<Rigidbody2D>().angularVelocity = 0;
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * 800f);
                _grounded = false;
            }
            if ("Solaris666".Equals(GameController.Character) && Input.GetButtonDown("Fire1"))
            {
                if (Random.Range(1, 1000) > 995)
                {
                    Death();
                }
            }
            if (transform.position.y < -5 || transform.position.y > 5)
            {
                Death();
            }
        }
        else
        {
            if (transform.position.y < -6)
            {
                GetComponent<Rigidbody2D>().isKinematic = true;
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        switch (col.transform.tag)
        {
            case "Floor":
                _grounded = true;
                break;
            case "Enemy":
                Destroy(col.gameObject);
                Death();
                break;
        }
    }

    bool IsGrounded()
    {
        Debug.DrawRay(transform.position, -Vector2.up, Color.green);
        return Physics2D.Raycast(transform.position, -Vector2.up, 1f);
    }

    void Explode()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        boom.Play();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if ("Enemy".Equals(colliders[i].tag) || "Crate".Equals(colliders[i].tag))
                    Destroy(colliders[i].gameObject);
            }
        }
    }

    void Death()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * 800f);
        GetComponent<Rigidbody2D>().AddForce(Vector2.right * 120f);
        GetComponent<Collider2D>().isTrigger = true;
        GameController.IsDead = true;
        isDead = true;
        deathScreen.enabled = true;
        retry.enabled = true;
    }

    public void Reset()
    {
        GameController.Score = 0;
        transform.position = Respawn.position;
        GameObject.FindGameObjectsWithTag("Enemy").ToList().ForEach(x => Destroy(x));
        GameObject.FindGameObjectsWithTag("Crate").ToList().ForEach(x => Destroy(x));
        GetComponent<Collider2D>().isTrigger = false;
        deathScreen.enabled = false;
        retry.enabled = false;
        ZukoVariable = 1;
        GetComponent<Rigidbody2D>().isKinematic = false;
        GameController.IsDead = false;
        ChooseLevel.GetNewLevel();
        isDead = false;
    }

    public void ExitGame()
    {
        GameController.IsDead = false;
        GameController.Score = 0;

        Destroy(GameObject.FindWithTag("GameController"));
        SceneManager.LoadSceneAsync(0);
    }
}
