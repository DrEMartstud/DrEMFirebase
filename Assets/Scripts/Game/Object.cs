using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class Object : MonoBehaviour {

        [SerializeField] 
        private Text _fileNameLabel;
        [SerializeField] 
        private Image _iconImage;

        [SerializeField]
        private Toggle _readToggle;
        
        [SerializeField]
        private Toggle _writeToggle;
        
        [SerializeField]
        private Toggle _executeToggle;

        private string _userName;
        
        void Start() {
            _readToggle.onValueChanged.AddListener(delegate { HandleToggles(); });
            _writeToggle.onValueChanged.AddListener(delegate { HandleToggles(); });
            _executeToggle.onValueChanged.AddListener(delegate { HandleToggles(); });
        }

        public void SetData(ScriptablePermisionValue permisionSO, string userLogin, bool isAdmin) {
            _fileNameLabel.text = permisionSO.fileName;
            _iconImage.sprite = permisionSO.iconImage;
            _userName = userLogin;
            
            SetPerisionsPanel(true);
            foreach (var user in permisionSO.Users) {
                if (userLogin == user.userName.ToLower()) {
                    SetPermisions(user.read, user.write, user.execute, isAdmin);
                    return;
                }
                else {
                    SetPermisions(false, false, false, isAdmin);
                }
            }
            if (!isAdmin) {
                gameObject.SetActive(false);
            }
        }

        private void OnDisable() {
            Destroy(gameObject);
        }

        private void SetPerisionsPanel(bool state) {
            _readToggle.gameObject.SetActive(state);
            _writeToggle.gameObject.SetActive(state);
            _executeToggle.gameObject.SetActive(state);
        }

        private void SetPermisions(bool read, bool write, bool execute, bool isAdmin) {
            _readToggle.isOn = read;
            _readToggle.interactable = isAdmin;
            _writeToggle.isOn = write;
            _writeToggle.interactable = isAdmin;
            _executeToggle.isOn = execute;
            _executeToggle.interactable = isAdmin;
        }

        private void HandleToggles() {
            var r = _readToggle.isOn;
            var w = _writeToggle.isOn;
            var e = _executeToggle.isOn;
            var f = _fileNameLabel.text;
            Debug.Log(f);
        }
    }
}