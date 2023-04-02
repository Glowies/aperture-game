using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSchemeBasedImage : MonoBehaviour
{
    public GameObject KeyboardImage;
    public GameObject GamepadImage;

    private PlayerInput _playerInput;

    void Awake()
    {
        _playerInput = FindObjectOfType<PlayerInput>();
    }

    void OnEnable()
    {
        UpdateImage(_playerInput);
        _playerInput.onControlsChanged += UpdateImage;
    }

    void OnDisable()
    {
        _playerInput.onControlsChanged -= UpdateImage;
    }
    
    void UpdateImage(PlayerInput playerInput)
    {
        bool isGamepad = playerInput.currentControlScheme == "Gamepad";
        
        if(KeyboardImage != null)
        {
            KeyboardImage.SetActive(!isGamepad);
        }
        if(GamepadImage != null)
        {
            GamepadImage.SetActive(isGamepad);
        }
    }
}
