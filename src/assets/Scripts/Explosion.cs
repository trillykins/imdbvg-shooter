using UnityEngine;
using System.Collections;
using System;

public class Explosion : MonoBehaviour
{

    public GameObject explosion;
    public AudioClip pik;

    private AudioSource _boom;
    private ProtagonistControls _player;
    private Renderer _renderer;
    private Collider2D _collider;
    private CameraShake _cameraShake;
    private GameObject _explosion;
    private float inactiveTimer = 1f;
    private bool disabling = false;

    void Awake()
    {
        _explosion = Instantiate(explosion, transform.position, transform.rotation);
        _explosion.SetActive(false);
        _cameraShake = Camera.main.GetComponent<CameraShake>();
        _player = FindObjectOfType<ProtagonistControls>();
        _collider = GetComponent<Collider2D>();
        _renderer = GetComponent<Renderer>();
        _boom = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        disabling = false;
        inactiveTimer = 1f;
        _renderer.enabled = true;
        _collider.enabled = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player"))
        {
            _explosion.SetActive(true);
            _explosion.transform.SetPositionAndRotation(transform.position, transform.rotation);
            _boom.PlayOneShot(pik);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);
            if (colliders.Length > 0)
            {
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].CompareTag("Enemy")) colliders[i].gameObject.SendMessage("HitByGrenade");
                    if (colliders[i].CompareTag("Crate")) colliders[i].gameObject.SetActive(false);
                    if (colliders[i].CompareTag("Player")) _player.Death();
                }
            }
            _renderer.enabled = false;
            _collider.enabled = false;
            _cameraShake.ShakeCamera(0.1f, 1f);
            disabling = true;
        }
        if (disabling) inactiveTimer -= Time.deltaTime;
        if (inactiveTimer <= 0f) gameObject.SetActive(false);
    }
}
