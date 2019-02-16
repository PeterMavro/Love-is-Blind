using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterType selectedCharacterAtStart;
    [Tooltip("Keep as: First: BlindBoy. Second: DeafGirl")]
    public Character[] characters;
    public CameraController cameraController;
    [Space]
    public float characterSwitchCooldown;
    [Space]
    public GameObject[] collectedItems;
    public int score;
    [Space]
    public UnityEvents.Floatx2UnityEvent OnCharacterSwitchCdSetup;
    public UnityEvents.FloatUnityEvent OnCharacterSwitchCdUpdated;

    private int _selectedCharacterIdx = 0;
    private float _characterSwitchCdTimer;

    private void Start()
    {
        _selectedCharacterIdx = (selectedCharacterAtStart == 0 ? 1 : 0);

        SwitchCharacter();

        OnCharacterSwitchCdSetup?.Invoke(0, characterSwitchCooldown);
    }

    private void Update()
    {
        if (_characterSwitchCdTimer > 0)
        {
            _characterSwitchCdTimer -= Time.deltaTime;
            _characterSwitchCdTimer = _characterSwitchCdTimer < 0 ? 0 : _characterSwitchCdTimer;

            OnCharacterSwitchCdUpdated?.Invoke(_characterSwitchCdTimer);
        }
    }

    /// <summary>
    /// Switch between two character only
    /// </summary>
    public void SwitchCharacter()
    {
        if (_characterSwitchCdTimer > 0) return;

        _characterSwitchCdTimer = characterSwitchCooldown;

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

    public enum CharacterType
    {
        BlindMan = 0,
        DeafWoman = 1
    }
}
