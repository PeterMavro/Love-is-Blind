using UnityEngine;

public class Player : MonoBehaviour
{
    public enum CharacterType
    {
        BlindMan = 0,
        DeafWoman = 1
    }

    public CharacterType selectedCharacterAtStart;
    [Tooltip("First: BlindMan. Second: DeafWoman")]
    public Character[] characters;
    public CameraController cameraController;
    [Space]
    public GameObject[] collectedItems;
    public int score;

    private int _selectedCharacterIdx = 0;

    private void Start()
    {
        _selectedCharacterIdx = (selectedCharacterAtStart == 0 ? 1 : 0);

        SwitchCharacter();
    }

    /// <summary>
    /// Switch between two character only
    /// </summary>
    public void SwitchCharacter()
    {
        if (characters.ValidIndex(_selectedCharacterIdx))
            DeselectCharacter();

        _selectedCharacterIdx = _selectedCharacterIdx == 0 ? 1 : 0;

        if (characters.ValidIndex(_selectedCharacterIdx))
        {
            SelectCharacter();

            SetCameraTarget();
        }
    }

    private void SelectCharacter()
    {
        characters[_selectedCharacterIdx].Select();
    }

    private void DeselectCharacter()
    {
        characters[_selectedCharacterIdx].Deselect();
    }

    private void SetCameraTarget()
    {
        cameraController.target = characters[_selectedCharacterIdx].transform;
    }
}
