using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // Singleton

    [Header("Audio Sources")]
    public AudioSource musica; // AudioSource que controlar� la m�sica de fondo
    public AudioSource efectos;  // AudioSource que controlar� los efectos de sonido

    [Header("Clips de Audio")]
    public AudioClip[] musicaClips;  // Array de m�sica
    public AudioClip[] efectosClips;    // Array de efectos de sonido

    [Header("Audio Mixer")]
    public AudioMixer audioMixer; // Referencia al MainAudioMixer

    [Header("UI Sliders")]
    public Slider musicSlider; // Slider para musica
    public Slider sfxSlider; // Slider para efectos

    [Header("Gestionar Mensajes")]
    public bool debugEnConsola; // Gestionador de mensajes

    private void Awake()
    {
        // Configurar Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persistencia entre escenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Configurar los valores iniciales de los sliders
        float musicVolume;
        audioMixer.GetFloat("MusicVolume", out musicVolume); // Obtenemos el valor que tenemos por defecto en el Audio Mixer
        musicSlider.value = musicVolume; // Asignamos el valor por defecto al slider

        float sfxVolume;
        audioMixer.GetFloat("SFXVolume", out sfxVolume); // Obtenemos el valor que tenemos por defecto en el Audio Mixer
        sfxSlider.value = sfxVolume; // Asignamos el valor por defecto al slider

        // A�adir listeners a los sliders
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    /// <summary>
    /// M�todo para ajustar el volumen de la musica de fondo
    /// </summary>
    /// <param name="volumen"> Valor enviado por el slider musicSlider </param>
    public void SetMusicVolume(float volumen)
    {
        audioMixer.SetFloat("MusicVolume", volumen);
    }

    /// <summary>
    /// M�todo para ajustar el volumen de los efectos
    /// </summary>
    /// <param name="volumen"> Valor enviado por el slider sfxSlider </param>
    public void SetSFXVolume(float volumen)
    {
        audioMixer.SetFloat("SFXVolume", volumen);
    }

    /// <summary>
    /// Reproducir m�sica de fondo
    /// </summary>
    /// <param name="index"> La posicion del clip del sonido que queremos reproducir </param>
    public void PlayMusic(int index)
    {
        if (index >= 0 && index < musicaClips.Length)
        {
            musica.clip = musicaClips[index];
            musica.Play();
        }
        else
        {
            if (debugEnConsola) print("�ndice de m�sica fuera de rango.");
        }
    }

    /// <summary>
    /// Reproducir efecto de sonido
    /// </summary>
    /// <param name="index"> La posicion del clip del sonido que queremos reproducir</param>
    public void PlayEfect(int index)
    {
        if (index >= 0 && index < efectosClips.Length)
        {
            efectos.PlayOneShot(efectosClips[index]);
        }
        else
        {
            if (debugEnConsola) print("�ndice de SFX fuera de rango.");
        }
    }

    /// <summary>
    /// Detener la m�sica actual
    /// </summary>
    public void StopMusic()
    {
        musica.Stop();
    }
}
