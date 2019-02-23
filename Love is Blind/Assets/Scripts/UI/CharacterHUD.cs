using UnityEngine;

public class CharacterHUD : MonoBehaviour
{
    public GameObject[] characterPortraits;

    private int _activePortrait = -1;

    private void Awake()
    {
        for (int i = 0; i < characterPortraits.Length; i++)
        {
            characterPortraits[i].SetActive(false);
        }
    }

    public void SetActive(int idx)
    {
        if (_activePortrait != -1)
            characterPortraits[_activePortrait].SetActive(false);

        if (characterPortraits.ValidIndex(idx))
        {
            characterPortraits[idx].SetActive(true);

            _activePortrait = idx;
        }
    }
}
