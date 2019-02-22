using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public Canvas canvas;
    public GameObject winUI;
    public GameObject loseUI;
    public Animator animator;

    public void ShowUI(GameResult result)
    {
        canvas.enabled = true;

        animator.SetTrigger("Fade");

        switch (result)
        {
            case GameResult.Win:
                loseUI.SetActive(false);
                winUI.SetActive(true);
                break;
            case GameResult.Lose:
                winUI.SetActive(false);
                loseUI.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void HideUI()
    {
        canvas.enabled = false;
    }

    public void ReloadScene()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
