using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class WeaponManager : MonoBehaviour
{
    public Sprite[] weapons;
    public GameObject[] projectiles;

    private string currentWeaponName;
    private AudioSource _shot;
    private float _machinegunTimer = .1f;

    private AmmoPooling _bullets;
    private AmmoPooling _grenades;
    private AmmoPooling _rockets;

    private enum _projectileType { bullet, rocket, grenade }

    void Start()
    {
        _bullets = new AmmoPooling(50, projectiles[(int)_projectileType.bullet]);
        _grenades = new AmmoPooling(50, projectiles[(int)_projectileType.grenade]);
        _rockets = new AmmoPooling(50, projectiles[(int)_projectileType.rocket]);

        currentWeaponName = weapons[1].name;
        _shot = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameController.IsDead)
        {
            switch (currentWeaponName)
            {
                case "Gun":
                    if (Input.GetButtonDown("Fire1"))
                    {
                        var b = _bullets.Next(); //_bullets.AmmoIndex++;//[BulletIndex++];
                        b.transform.SetPositionAndRotation(transform.position, transform.rotation);
                        b.transform.Rotate(0, 0, Random.Range(-3f, 4f));
                        b.SetActive(true);
                        _shot.Play();
                    }
                    break;
                case "Machinegun":
                    _machinegunTimer -= Time.deltaTime;
                    if (Input.GetButton("Fire1") && _machinegunTimer <= 0)
                    {
                        var b = _bullets.Next(); //_bullets[BulletIndex++];
                        b.transform.SetPositionAndRotation(transform.position, transform.rotation);
                        b.transform.Rotate(0, 0, Random.Range(-3f, 4f));
                        _shot.Play();
                        b.SetActive(true);
                        _machinegunTimer = .1f;
                    }
                    break;
                case "Grenadelauncher":
                    if (Input.GetButtonDown("Fire1"))
                    {
                        var g = _grenades.Next();
                        g.transform.SetPositionAndRotation(transform.position, transform.rotation);
                        g.SetActive(true);
                    }

                    break;
                case "Rocketlauncher":
                    if (Input.GetButtonDown("Fire1"))
                    {
                        var r = _rockets.Next();
                        r.transform.SetPositionAndRotation(transform.position, transform.rotation);
                        r.SetActive(true);
                    }
                    break;
                case "Shotgun":
                    _machinegunTimer -= Time.deltaTime;
                    if (Input.GetButtonDown("Fire1") && _machinegunTimer <= 0)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            var b = _bullets.Next();
                            b.transform.SetPositionAndRotation(transform.position, transform.rotation);
                            b.transform.Rotate(0, 0, Random.Range(-3f, 4f));
                            b.SetActive(true);
                        }
                        _shot.Play();
                        _machinegunTimer = .4f;
                    }
                    break;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag.Equals("Crate"))
        {
            GameController.Score++;
            int bla = Random.Range(0, weapons.Length);
            GetComponent<SpriteRenderer>().sprite = weapons[bla];
            currentWeaponName = weapons[bla].name;
            Destroy(col.gameObject);
        }
    }
}

public class AmmoPooling
{
    private readonly int _ammoCount;
    private readonly List<GameObject> _ammo = new List<GameObject>();
    private int _ammoIndex = 0;

    public AmmoPooling(int ammoCount, GameObject gameObject)
    {
        _ammoCount = ammoCount;
        for (int i = 0; i < ammoCount; i++)
        {
            var ammo = Object.Instantiate(gameObject);
            ammo.SetActive(false);
            _ammo.Add(ammo);
        }
    }

    public GameObject Next()
    {
        if (_ammoIndex == 0)
        {
            _ammoIndex++;
            return _ammo[0];
        }
        if (_ammoIndex >= _ammoCount)
        {
            _ammoIndex = 0;
            return _ammo[_ammoIndex];
        }
        return _ammo[_ammoIndex++];
    }
}
