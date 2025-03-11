using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    //Game Object Variables
    private AudioSource managerAudioSource;
    public Slider bgmVolumeSlider;
    public Slider sfxVolumeSlider;
    
    public static float sfxVolume;
    //public float sfxVolume;

    //Internal variables
    private bool muted;
    public Image soundOnIcon;
    public Image soundOffIcon;

    // Start is called before the first frame update
    void Start()
    {
        managerAudioSource = GetComponent<AudioSource>();

        //Load Volume Prefs
        if(PlayerPrefs.HasKey("bgmVolume")){
            bgmVolumeSlider.value = PlayerPrefs.GetFloat("bgmVolume");
        }
        else{
            bgmVolumeSlider.value = 0.5f;
        }

        if(PlayerPrefs.HasKey("sfxVolume")){
            sfxVolumeSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        }
        else{
            sfxVolumeSlider.value = 0.4f;
        }

        //Load Muted Prefs
        if(PlayerPrefs.HasKey("muted")){
            if(PlayerPrefs.GetInt("muted") == 1){
                muted = true;
            }
            else{
                muted = false;
            }
        }
        else{
            muted = false;
        }

        if(!muted){
            managerAudioSource.Play();
        }
        else{
            managerAudioSource.Pause();
        }
        soundOnIcon.gameObject.SetActive(!muted);
        soundOffIcon.gameObject.SetActive(muted);
    }

    //Function for Volume Slider
    public void ChangeBmgVolume(){
        //AudioListener.volume = bgmVolumeSlider.value;
        managerAudioSource.volume = bgmVolumeSlider.value;
        PlayerPrefs.SetFloat("bgmVolume", bgmVolumeSlider.value);
    }

    public void ChangeSfxVolume(){
        sfxVolume = sfxVolumeSlider.value;
        PlayerPrefs.SetFloat("sfxVolume", sfxVolumeSlider.value);
    }

    //Function for Mute Button
    public void SoundToggle(){
        muted = !muted;
        if(!muted){
            managerAudioSource.Play();
        }
        else{
            managerAudioSource.Pause();
        }

        soundOnIcon.gameObject.SetActive(!muted);
        soundOffIcon.gameObject.SetActive(muted);

        if(muted){
            PlayerPrefs.SetInt("muted", 1);
        }
        else{
            PlayerPrefs.SetInt("muted", 0);
        }
    }
}
