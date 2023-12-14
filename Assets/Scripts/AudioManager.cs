using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource musicSource, sfxSource;

    // Cada entrada del diccionario tiene una string como clave y un AudioClip como valor
    public Dictionary<string, AudioClip> sfxClips = new Dictionary<string, AudioClip>();
    public Dictionary<string, AudioClip> musicClips = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        // Garantizamos que solo exista una instancia del AudioManager
        // Si no hay instancias previas se asigna la actual
        // Si hay instancias se destruye la nueva
        if (instance == null) instance = this;
        else { Destroy(gameObject); return; }

        // No destruimos el AudioManager aunque se cambie de escena
        DontDestroyOnLoad(gameObject);

        // Cargamos los AudioClips en los diccionarios
        LoadMusicClips();
        LoadSFXClips();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Método privado para cargar la música de fondo directamente desde las carpetas
    private void LoadMusicClips()
    {
        // Los recursos (ASSETS) que se cargan en TIEMPO DE EJECUCIÓN DEBEN ESTAR DENTRO de una carpeta denominada /Assets/Resources/Music
        musicClips["MainTheme"] = Resources.Load<AudioClip>("Music/Main_Theme");
        musicClips["InvincibilityTheme"] = Resources.Load<AudioClip>("Music/Invincibility_Theme");
        musicClips["LoseALife"] = Resources.Load<AudioClip>("Music/Lost_A_Life");
    }

    private void LoadSFXClips()
    {
        // Los recursos (ASSETS) que se cargan en TIEMPO DE EJECUCIÓN DEBEN ESTAR DENTRO de una carpeta denominada /Assets/Resources/SFX
        sfxClips["Jump"] = Resources.Load<AudioClip>("SFX/Jump");
        sfxClips["CollectCoin"] = Resources.Load<AudioClip>("SFX/Collect_Coin");
        sfxClips["Hit"] = Resources.Load<AudioClip>("SFX/Hit");
    }

    // Método de la clase singleton para reproducir música de fondo
    public void PlayMusic(string clipName)
    {
        if (musicClips.ContainsKey(clipName))
        {
            musicSource.clip = musicClips[clipName];
            musicSource.Play();
        }
        else Debug.LogWarning("El AudioClip " + clipName + " no se encontró en el diccionario de musicClips.");

        if (clipName == "MainTheme") musicSource.loop = true;
        else musicSource.loop = false;
    }

    // Método de la clase singleton para reproducir efectos de sonido
    public void PlaySFX(string clipName)
    {
        if (sfxClips.ContainsKey(clipName))
        {
            sfxSource.clip = sfxClips[clipName];
            sfxSource.Play();
        }
        else Debug.LogWarning("El AudioClip " + clipName + " no se encontró en el diccionario de sfxClips.");
    }
}
