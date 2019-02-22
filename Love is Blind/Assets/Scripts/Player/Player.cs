using System;
using UnityEngine;

public class Player : MonoBehaviour, ISendGameOver
{
    public CharacterType selectedCharacterAtStart;
    [Tooltip("Keep as: First: BlindBoy. Second: DeafGirl")]
    public Character[] characters;
    public CameraController cameraController;

    private int _selectedCharacterIdx = 0;

    private CharacterSwitchComponent _characterSwitchComponent;
    private CharacterRunBoostComponent _characterRunBoostComponent;
    private PlayerInput _input;

    #region Unity methods
    private void Awake()
    {
        _characterSwitchComponent = GetComponent<CharacterSwitchComponent>();
        _characterRunBoostComponent = GetComponent<CharacterRunBoostComponent>();
        _input = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        PlayerManager.Instance.localPlayer = this;
    }

    private void OnDisable()
    {
        if (PlayerManager.Instance.localPlayer == this)
            PlayerManager.Instance.localPlayer = null;
    }

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        var deltaTime = Time.deltaTime;

        _characterSwitchComponent.OnUpdate(deltaTime);

        _characterRunBoostComponent.OnUpdate(deltaTime);

        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].OnUpdated(deltaTime);
        }
    }
    #endregion

    #region Init and components methods
    public void Initialize()
    {
        SetActiveInput(true);

        cameraController.enabled = true;

        _selectedCharacterIdx = (selectedCharacterAtStart == 0 ? 1 : 0);

        /* Switch to the selected character (generates SwitchCharacter cooldown) */
        SwitchCharacter();
    }

    public void SetActiveInput(bool active)
    {
        _input.enabled = active;
    }
    #endregion

    #region SwitchCharacter methods
    /// <summary>
    /// Switch between two character only
    /// </summary>
    public void SwitchCharacter()
    {
        if (_characterSwitchComponent.Cooldown > 0) return;

        _characterSwitchComponent.DoReset();

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

    public void SetCharacterInputActive(bool active)
    {
        characters[_selectedCharacterIdx].SetInputActive(active);
    }
    #endregion

    #region RunBoost methods
    public void ActiveRunBoost()
    {
        if (_characterRunBoostComponent.Cooldown > 0) return;

        _characterRunBoostComponent.DoReset();

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
    #endregion

    public void SendGameOver(GameResult result)
    {
        cameraController.enabled = false;

        SetActiveInput(false);

        GameManager.Instance.GameOver(result);
    }

    public enum CharacterType
    {
        BlindBoy = 0,
        DeafGirl = 1
    }
}
