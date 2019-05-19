using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public static bool isDead;
    public static string character;
    public static int score = 0;
    public AudioClip MenuMusic;
    public AudioClip GameMusic;

    private AudioSource _music;
    private bool _hasChanged = false;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        _music = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Equals("Level01") && !_hasChanged)
        {
            _music.clip = GameMusic;
            _music.Play();
            _hasChanged = true;
        }
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            _music.mute = !_music.mute;
        }
    }

    public void SetName(string name)
    {
        Debug.Log($"Name set to: {name}");
        character = name;
    }
}
