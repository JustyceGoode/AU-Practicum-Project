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
    public static float sfxVolume = 0.4f;

    //Internal variables
    private bool muted;
    public Image soundOnIcon;
    public Image soundOffIcon;

    // Start is called before the first frame update
    void Start()
    {
        managerAudioSource = GetComponent<AudioSource>();

        //Load Volume Prefs
        if(PlayerPrefs.HasKey("musicVolume")){
            bgmVolumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        }
        else{
            bgmVolumeSlider.value = 0.5f;
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
        PlayerPrefs.SetFloat("musicVolume", bgmVolumeSlider.value);
    }

    public void ChangeSfxVolume(){
        sfxVolume = sfxVolumeSlider.value;
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
