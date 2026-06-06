using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    private enum type { 
        MASTER, 
        MUSIC,
        SFX
    }

    [SerializeField] type audioType;
    Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();

        slider.onValueChanged.AddListener(OnChanged);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float value = audioType switch
        {
            type.MASTER => AudioManager.I.MasterVolume,
            type.MUSIC => AudioManager.I.MusicVolume,
            type.SFX => AudioManager.I.SFXVolume,
            _ => 1f
        };

        slider.SetValueWithoutNotify(value);
        

        enabled = false;
    }

    private void OnChanged(float value)
    {
        switch(audioType)
        {
            case type.MASTER:
                AudioManager.I.SetMasterVolume(value); break;
            case type.MUSIC:
                AudioManager.I.SetMusicVolume(value); break;
            case type.SFX:
                AudioManager.I.SetSFXVolume(value); break;
        }
    }
}
