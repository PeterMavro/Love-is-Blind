using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterType selectedCharacterAtStart;
    [Tooltip("Keep as: First: BlindBoy. Second: DeafGirl")]
    public Character[] characters;
    public CameraController cameraController;

    private int _selectedCharacterIdx = 0;

    private CharacterSwitchComponent _characterSwitchComponent;
    private CharacterRunBoostComponent _characterRunBoostComponent;

    private void Awake()
    {
        _characterSwitchComponent = GetComponent<CharacterSwitchComponent>();
        _characterRunBoostComponent = GetComponent<CharacterRunBoostComponent>();
    }

    private void Start()
    {
        _selectedCharacterIdx = (selectedCharacterAtStart == 0 ? 1 : 0);

        SwitchCharacter();
    }

    private void Update()
    {
        _characterSwitchComponent.OnUpdate(Time.deltaTime);

        _characterRunBoostComponent.OnUpdate(Time.deltaTime);
    }

    /// <summary>
    /// Switch between two character only
    /// </summary>
    public void SwitchCharacter()
    {
        if (_characterSwitchComponent.Cooldown > 0) return;

        _characterSwitchComponent.Reset();

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

    public void ActiveRunBoost()
    {
        if (_characterRunBoostComponent.Cooldown > 0) return;

        _characterRunBoostComponent.Reset();

        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].ActiveRunBoost(_characterRunBoostComponent.boostMultiplier);
        }
    }

    public void DesactiveRunBoost()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].DesactiveRunBoost();
        }
    }

    public enum CharacterType
    {
        BlindBoy = 0,
        DeafGirl = 1
    }
}
