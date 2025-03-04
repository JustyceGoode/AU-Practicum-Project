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

    public static SoundManager instance;

    // Start is called before the first frame update
    void Start()
    {
        managerAudioSource = GetComponent<AudioSource>();

        // if(!PlayerPrefs.HasKey("musicVolume")){
        //     PlayerPrefs.SetFloat("musicVolume", 1);
        //     Load();
        // }
        // else{
        //     Load();
        // }
        // if(!PlayerPrefs.HasKey("muted")){
        //     PlayerPrefs.SetFloat("muted", 0);
        //     Load();
        // }
        // else{
        //     Load();
        // }

        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");

        if(PlayerPrefs.GetInt("muted") == 1){
            muted = true;
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

    // Update is called once per frame
    void Update()
    {
        //transform.position = Camera.main.transform.position;
    }

    //TODO
    //When ever the Sound Manager is destroyed and raplaced,
    //the object is removed from the button and sliders.
    // public void Awake(){
    //     if(instance == null){
    //         instance = this;
    //         DontDestroyOnLoad(gameObject);
    //         //DontDestroyOnLoad(volumeSlider.gameObject);
            
    //     }
    //     else{
    //         Destroy(gameObject);
    //         //Destroy(volumeSlider.gameObject);
    //     }

    //     //DontDestroyOnLoad(gameObject);
    // }

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

        //Save();
    }

    // private void Save(){
    //     PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    //     if(muted){
    //         PlayerPrefs.SetInt("muted", 1);
    //     }
    //     else{
    //         PlayerPrefs.SetInt("muted", 0);
    //     }
    // }

    // private void Load(){
    //     volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");

    //     if(PlayerPrefs.GetInt("muted") == 1){
    //         muted = true;
    //     }
    //     else{
    //         muted = false;
    //     }
    // }
}
