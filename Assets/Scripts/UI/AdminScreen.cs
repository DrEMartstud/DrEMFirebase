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
        
        [SerializeField]
        private Dropdown _userOptions;
        
        private void Start() {
            _exitButton.onClick.AddListener(Exit);
        }
        
        private void OnEnable() {
            _userOptions.onValueChanged.AddListener(delegate { SetScreen(); });
            SetScreen();
        }

        private void OnDisable() {
            _parsedLogin.value = "";
        }

        private void SetScreen() {
            _containerForObjects.gameObject.SetActive(false);
            _containerForObjects.gameObject.SetActive(true);
            bool isAdmin = _parsedLogin.value == "admin";
            _userOptions.gameObject.SetActive(isAdmin);
            _welcomeMessageStringValue.text = $"Добро пожаловать, {_parsedLogin.value}!";
            if (!isAdmin) {
                SpawnObjects(_parsedLogin.value, isAdmin);
                return;
            }
            else {
                _userOptions.options.Clear();
                for (int i = 0; i < SaveSystem.SavedDatas.Count; i++) {
                    _userOptions.options.Add(new Dropdown.OptionData(){ text = SaveSystem.SavedDatas[i].login});
                }
                SpawnObjects(_userOptions.options[_userOptions.value].text, isAdmin);
            }
        }
        
        private void SpawnObjects(string userLogin, bool isAdmin) {
            foreach (var fileObject in _fileObjects) {
                var objectView = Instantiate(_objectViewTemplate, _containerForObjects);
                objectView.SetData(fileObject, userLogin, isAdmin);
            }
        }

        public void SetPermisionsOnFile(bool r, bool w, bool e, string fileName, string userLogin) {
            var foundFile = _fileObjects.Find(file => file.fileName == fileName);
            foreach (var user in foundFile.Users) {
                if (user.userName == userLogin) {
                    user.read = r;
                    user.write = w;
                    user.execute = e;
                }
            }
        }
        
        private void Exit() {
            _pressedExitEventDispatcher.Dispatch();
        }
    }
}