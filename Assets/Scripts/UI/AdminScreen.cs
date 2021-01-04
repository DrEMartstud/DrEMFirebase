using System.Collections;
using System.Collections.Generic;
using Events;
using Game;
using UnityEngine;
using UnityEngine.UI;
namespace UI {

    public class AdminScreen : MonoBehaviour {

        [SerializeField] 
        private Text _welcomeMessageStringValue;
        
        [SerializeField]
        private Button _exitButton;

        [SerializeField]
        private ScriptableStringValue _parsedLogin;
        
        [SerializeField] 
        private EventDispatcher _pressedExitEventDispatcher;

        private void Start() {
            _exitButton.onClick.AddListener(Exit);
        }
        
        private void OnEnable() {
            _welcomeMessageStringValue.text = $"Welcome, {_parsedLogin.value}!";
        }

        private void OnDisable() {
            _parsedLogin.value = "";
        }

        private void Exit() {
            _pressedExitEventDispatcher.Dispatch();
        }
    }
}