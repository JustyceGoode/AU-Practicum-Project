using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    private AudioSource managerAudioSource;
    public Slider volumeSlider;
    //private bool muted = false;
    private bool muted;

    //public Button soundButton;
    public Image soundOnIcon;
    public Image soundOffIcon;

    //public static SoundManager instance;

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
        //     managerAudioSource.Play();
        //     Load();
        // }
        // else{
        //     Debug.Log("Player Prefs Has Mute Key: " + PlayerPrefs.HasKey("muted"));
        //     Load();
        // }

        Load();

        AudioListener.pause = muted;
        soundOnIcon.gameObject.SetActive(!muted);
        soundOffIcon.gameObject.SetActive(muted);
        
        //Debug.Log("Player Prefs Muted: " + PlayerPrefs.GetInt("muted"));
        //Debug.Log("Muted: " + muted);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Camera.main.transform.position;
    }

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
        if(!muted){
            AudioListener.volume = volumeSlider.value;
        }

        Save();
    }

    public void SoundToggle(){
        muted = !muted;
        AudioListener.pause = muted;
        soundOnIcon.gameObject.SetActive(!muted);
        soundOffIcon.gameObject.SetActive(muted);
        //Debug.Log("Muted: " + muted);

        Save();
        Debug.Log("Player Prefs Muted: " + PlayerPrefs.GetInt("muted"));
    }

    private void Save(){
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
        if(muted){
            PlayerPrefs.SetInt("muted", 1);
        }
        else{
            PlayerPrefs.SetInt("muted", 0);
        }
        //PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }

    public void Load(){
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");

        Debug.Log("Load Function");
        Debug.Log("Player Prefs Muted: " + PlayerPrefs.GetInt("muted"));
        //muted = (PlayerPrefs.GetInt("muted") == 1);
        Debug.Log("Muted: " + muted);
        if(PlayerPrefs.GetInt("muted") == 1){
            muted = true;
        }
        else{
            muted = false;
            managerAudioSource.Play();
        }
    }
}
