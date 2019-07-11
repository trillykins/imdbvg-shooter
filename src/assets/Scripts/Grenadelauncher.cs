using UnityEngine;
using System.Collections;

public class Grenadelauncher : MonoBehaviour
{

    public AudioClip pik;
    public GameObject explosion;

    private Rigidbody2D rigid;
    private float direction;
    private AudioSource boom;
    private float timer = 2f;
    private float inactiveTimer = 1f;
    private bool disabling;
    private Renderer _renderer;
    private Collider2D _collider;
    private CameraShake _camera;
    private ProtagonistControls _player;
    private GameObject _explosion;

    void Awake()
    {
        _explosion = Instantiate(explosion, transform.position, transform.rotation);
        _explosion.SetActive(false);
        _camera = Camera.main.GetComponent<CameraShake>();
        _collider = GetComponent<Collider2D>();
        _renderer = GetComponent<Renderer>();
        boom = GetComponent<AudioSource>();
        _player = FindObjectOfType<ProtagonistControls>();
        rigid = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        disabling = false;
        timer = 2f;
        inactiveTimer = 1f;
        _renderer.enabled = true;
        _collider.enabled = true;
        direction = _player.transform.localScale.x;
        rigid.AddForce(Vector2.up * 450f);
        rigid.AddForce(Vector2.right * 500f * -Mathf.Sign(direction));
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f && _collider.enabled == true)
        {
            _explosion.SetActive(true);
            _explosion.transform.SetPositionAndRotation(transform.position, transform.rotation);
            boom.PlayOneShot(pik);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);
            if (colliders.Length > 0)
            {
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].tag == "Enemy" || colliders[i].tag == "Crate")
                        colliders[i].gameObject.SetActive(false);
                    if (colliders[i].tag == "Player")
                    {
                        _player.Death();
                    }
                }
            }
            _renderer.enabled = false;
            _collider.enabled = false;
            _camera.ShakeCamera(0.1f, 1f);
            disabling = true;
        }
        if (disabling) inactiveTimer -= Time.deltaTime;
        if (inactiveTimer <= 0f) gameObject.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "Enemy" || col.transform.tag == "Crate")
        {
            Instantiate(explosion, transform.position, transform.rotation);
            boom.PlayOneShot(pik);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);
            if (colliders.Length > 0)
            {
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].tag == "Enemy") colliders[i].gameObject.SendMessage("HitByGrenade");
                    if (colliders[i].tag == "Crate") colliders[i].gameObject.SetActive(false);
                    if (colliders[i].tag == "Player") _player.Death();
                }
            }
            _renderer.enabled = false;
            _collider.enabled = false;
            _camera.ShakeCamera(0.1f, 1f);
            disabling = true;
        }
        if (disabling) inactiveTimer -= Time.deltaTime;
        if (inactiveTimer <= 0f) gameObject.SetActive(false);
    }
}
