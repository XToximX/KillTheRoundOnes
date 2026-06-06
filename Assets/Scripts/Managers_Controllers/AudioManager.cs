using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager I;

    [SerializeField] private AudioMixer audioMixer;

    private float masterVolume = 10f;
    private float musicVolume = 10f;
    private float sfxVolume = 10f;

    public float MasterVolume => masterVolume;
    public float MusicVolume => musicVolume;
    public float SFXVolume => sfxVolume;


    private void Awake()
    {
        if (I != null && I!= this)
        {
            Destroy(gameObject);
            return;
        }

        I = this;

        DontDestroyOnLoad(gameObject);
    }

    public void SetMasterVolume(float value)
    {

        masterVolume = value;

        value = value / 10;
        if (value <= 0) value = 0.001f;

        float db = Mathf.Log10(value) * 20f;
        audioMixer.SetFloat("Master", db);
    }

    public void SetMusicVolume(float value)
    {
        musicVolume = value;

        value = value / 10;
        if (value <= 0) value = 0.001f;

        float db = Mathf.Log10(value) * 20f;
        audioMixer.SetFloat("Music", db);
    }

    public void SetSFXVolume(float value)
    {
        sfxVolume = value;

        value = value / 10;
        if (value <= 0) value = 0.001f;

        float db = Mathf.Log10(value) * 20f;
        audioMixer.SetFloat("SFX", db);
    }
}
