using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using TMPro;

public class Settings : MonoBehaviour
{
    public AudioMixer audioMixer;
    [SerializeField] private GameObject changeText;
    [SerializeField] private GameObject changeVolume;
  
    float VolumeValue = 0f;
    int GraphicIndex;
    

    void Start()
    {
        VolumeValue = PlayerPrefs.GetFloat("VolumeValue");
        GraphicIndex = PlayerPrefs.GetInt("Graphic");
        if(GraphicIndex == 0)
        {
            changeText.GetComponent<TMP_Dropdown>().value = 0;
        }
        if(GraphicIndex == 1)
        {
            changeText.GetComponent<TMP_Dropdown>().value = 1;
        }
        if(GraphicIndex == 2)
        {
            changeText.GetComponent<TMP_Dropdown>().value = 2;
        }
        changeVolume.GetComponent<Slider>().value = VolumeValue;
        
        Debug.Log(VolumeValue);
        Debug.Log(GraphicIndex);
    }

    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("Volume", volume);
        VolumeValue = volume;
        
    }

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        GraphicIndex = qualityIndex;
    }

    public void Back()
    { 
        PlayerPrefs.SetFloat("VolumeValue", VolumeValue);
        PlayerPrefs.SetInt("Graphic", GraphicIndex);
        SceneManager.LoadScene("Menu");
    }
}
