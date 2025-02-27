using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Slider volumeSlider;
    private bool muted = false;

    //public Button soundButton;
    public Image soundOnIcon;
    public Image soundOffIcon;

    //public static SoundManager instance;

    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("musicVolume")){
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else{
            Load();
        }
        if(!PlayerPrefs.HasKey("muted")){
            PlayerPrefs.SetFloat("muted", 0);
            Load();
        }
        else{
            Load();
        }

        AudioListener.pause = muted;
        soundOnIcon.gameObject.SetActive(!muted);
        soundOffIcon.gameObject.SetActive(muted);
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

        Save();
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
        muted = (PlayerPrefs.GetInt("muted") == 1);
    }
}
