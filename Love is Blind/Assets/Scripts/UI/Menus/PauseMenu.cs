using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Canvas pauseMenuUI;
    public Animator animator;

    private void Start()
    {
        Resume();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!GameManager.GameIsPaused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.enabled = false;

        GameManager.Instance.Play();
    }

    public void Pause()
    {
        pauseMenuUI.enabled = true;

        animator.SetTrigger("Fade");

        GameManager.Instance.Pause();
    }

    public void Menu()
    {

    }

    public void Quit()
    {
        if (Application.isPlaying)
            Application.Quit();
#if UNITY_EDITOR
        else
            UnityEditor.EditorApplication.isPlaying = false;
#endif
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
