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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeVolume(){
        AudioListener.volume = volumeSlider.value;
    }

    public void SoundToggle(){
        muted = !muted;
        AudioListener.pause = muted;
        //AudioListener.pause = !AudioListener.pause;
        //soundOnIcon.enabled = !muted;
        //soundOffIcon.enabled = muted;
        soundOnIcon.gameObject.SetActive(!muted);
        soundOffIcon.gameObject.SetActive(muted);
    }

    // public void UpdateButtonIcon(){
    //     soundOnIcon.enabled = !muted;
    //     soundOffIcon.enabled = muted;
    // }

    private void Save(){
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }

    public void Load(){
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }
}
