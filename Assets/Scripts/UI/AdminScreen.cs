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

        [SerializeField] 
        private List<ScriptablePermisionValue> _fileObjects = new List<ScriptablePermisionValue>();
        
        [SerializeField]
        private Object _objectViewTemplate;
        
        [SerializeField] 
        private Transform _containerForObjects;
        
        private void Start() {
            _exitButton.onClick.AddListener(Exit);
        }
        
        private void OnEnable() {
            _welcomeMessageStringValue.text = $"Добро пожаловать, {_parsedLogin.value}!";
            SpawnObjects();
        }

        private void OnDisable() {
            _parsedLogin.value = "";
        }

        private void SpawnObjects() {
            foreach (var fileObject in _fileObjects) {
                var objectView = Instantiate(_objectViewTemplate, _containerForObjects);
                objectView.SetData(fileObject, _parsedLogin.value);
            }
        }
        
        private void Exit() {
            _pressedExitEventDispatcher.Dispatch();
        }
    }
}