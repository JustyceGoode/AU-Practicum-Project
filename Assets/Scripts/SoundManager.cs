using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    private AudioSource managerAudioSource;
    public Slider volumeSlider;
    private bool muted;

    //public Button soundButton;
    public Image soundOnIcon;
    public Image soundOffIcon;

    //public static SoundManager instance;

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

    // Update is called once per frame
    void Update()
    {
        //transform.position = Camera.main.transform.position;
    }

    //TODO
    //When the slider is clicked, the A and D buttons move the slider
    //When any other place is clicked afterward, the effect ends
    public void ChangeVolume(){
        // if(!muted){
        //     AudioListener.volume = volumeSlider.value;
        // }

        AudioListener.volume = volumeSlider.value;
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
        //Save();
    }

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
