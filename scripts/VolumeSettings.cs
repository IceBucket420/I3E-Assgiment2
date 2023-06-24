using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider MasterSlider;
    [SerializeField] Slider SFXSlider;
    [SerializeField] Slider BGMSlider;

    public const string MIXER_MASTER = "MasterVolume";
    public const string MIXER_SFX = "SFXVolume";
    public const string MIXER_BGM = "BGMVolume";

    private void Awake()
    {
        MasterSlider.onValueChanged.AddListener(SetMasterVolume);
        SFXSlider.onValueChanged.AddListener(SetSFXVolume);
        BGMSlider.onValueChanged.AddListener(SetBGMVolume);
    }

    private void Start()
    {
        MasterSlider.value = PlayerPrefs.GetFloat(AudioManager.MASTER_KEY, 1f);
        SFXSlider.value = PlayerPrefs.GetFloat(AudioManager.SFX_KEY, 1f);
        BGMSlider.value = PlayerPrefs.GetFloat(AudioManager.BGM_KEY, 1f);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(AudioManager.MASTER_KEY, MasterSlider.value);
        PlayerPrefs.SetFloat(AudioManager.SFX_KEY, SFXSlider.value);
        PlayerPrefs.SetFloat(AudioManager.BGM_KEY, BGMSlider.value);
    }

    void SetMasterVolume(float value)
    {
        mixer.SetFloat(MIXER_MASTER, Mathf.Log10(value) * 20);
    }
    void SetSFXVolume(float value)
    {
        mixer.SetFloat(MIXER_SFX, Mathf.Log10(value) * 20);
    }
    void SetBGMVolume(float value)
    {
        mixer.SetFloat(MIXER_BGM, Mathf.Log10(value) * 20);
    }
}

