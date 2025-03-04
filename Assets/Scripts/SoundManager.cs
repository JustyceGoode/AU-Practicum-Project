using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    //Game Object Variables
    private AudioSource managerAudioSource;
    public Slider volumeSlider;

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
            volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
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
    public void ChangeVolume(){
        AudioListener.volume = volumeSlider.value;
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
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
