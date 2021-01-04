using System.Collections;
using System.Collections.Generic;
using Events;
using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class RegisterScreen : MonoBehaviour {

        [SerializeField] 
        private EventDispatcher _pressedExitEventDispatcher;
        
        [SerializeField] 
        private EventListener _successRegisterEventListener;
        
        [SerializeField] 
        private EventDispatcher _pressedRegisterEventDispatcher;
        
        [SerializeField]
        private InputField _loginInputField;

        [SerializeField]
        private InputField _passwordInputField;
        
        [SerializeField]
        private ScriptableStringValue _parsedLogin;
        
        [SerializeField]
        private ScriptableStringValue _parsedPassword;
        
        [SerializeField] 
        private Button _exitButton;
        
        [SerializeField]
        private Button _registerButton;
        
        private void Start() {
            _exitButton.onClick.AddListener(Exit);
            _registerButton.onClick.AddListener(OnRegisterButtonClick);
        }

        private void OnEnable() {
            ResetScreen();
            _successRegisterEventListener.OnEventHappened += ResetScreen;
        }

        private void OnDisable() {
            _successRegisterEventListener.OnEventHappened -= ResetScreen;
        }
        
        private void ResetScreen() {
            _parsedPassword.value = "";
            _parsedLogin.value = "";
            _registerButton.interactable = true;
            _loginInputField.text = "";
            _passwordInputField.text = "";
        }
        
        private void OnRegisterButtonClick() {
            _registerButton.interactable = false;
            _parsedLogin.value = _loginInputField.text.ToLower();
            _parsedPassword.value = _passwordInputField.text;
            _pressedRegisterEventDispatcher.Dispatch();
        }

        private void Exit() {
            _pressedExitEventDispatcher.Dispatch();
        }
    }
}