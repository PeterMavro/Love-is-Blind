using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject howToPlay;

    public void LoadLevel()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void PlayButtonClickAudio()
    {
        AudioManager.Instance.Play("UI", "ButtonClick", AudioType.SFX);
    }

    public void PlayButtonHoverAudio()
    {
        AudioManager.Instance.Play("UI", "ButtonHover", AudioType.SFX);
    }
}
