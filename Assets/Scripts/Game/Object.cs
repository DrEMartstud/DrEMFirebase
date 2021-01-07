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
        private Button _buttonRead;
        
        [SerializeField] 
        private Button _buttonWrite;
        
        [SerializeField]
        private Button _buttonExecute;

        void Start() {
            // _buttonRead.onClick.AddListener(Read);
            // _buttonWrite.onClick.AddListener(Write);
            // _buttonExecute.onClick.AddListener(Execute);
                
        }

        public void SetData(ScriptablePermisionValue permisionSO, string userLogin) {
            _fileNameLabel.text = permisionSO.fileName;
            _iconImage.sprite = permisionSO.iconImage;
            if (userLogin == "admin") {
                SetButtons(true, true, true);
                return;
            }
            foreach (var user in permisionSO.Users) {
                if (userLogin == user.userName.ToLower()) {
                    SetButtons(user.read, user.write, user.execute);
                    return;
                } 
            }
            gameObject.SetActive(false);
        }

        private void OnDisable() {
            SetButtons(false, false, false);
            Destroy(gameObject);
        }

        private void SetButtons(bool read, bool write, bool execute) {
            _buttonRead.gameObject.SetActive(read);
            _buttonWrite.gameObject.SetActive(write);
            _buttonExecute.gameObject.SetActive(execute);
        }

        // private void Read() {
        //     
        // }
        //
        // private void Write() {
        //     
        // }
        //
        // private void Execute() {
        //     
        // }
    }
}