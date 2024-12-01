using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public AudioClip button_audio;

    public void LoadGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void SetActive(GameObject obj)
    {
        obj.SetActive(true);
    }

    public void SetInactive(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void PlayMenuSound()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.PlayOneShot(button_audio);
    }
}
