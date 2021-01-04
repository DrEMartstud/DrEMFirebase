using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class Object : MonoBehaviour {

        [SerializeField] private Text _fileNameLabel;
        [SerializeField] private Image _iconImage;
        [SerializeField] private Button _buttonRead;
        [SerializeField] private Button _buttonWrite;
        [SerializeField] private Button _buttonExecute;

        public string fileName;
        public bool read;
        public bool write;
        public bool execute;

        void Start() {
            _buttonRead.onClick.AddListener(Read);
            _buttonWrite.onClick.AddListener(Write);
            _buttonExecute.onClick.AddListener(Execute);
                
        }

        private void OnEnable() {
            _fileNameLabel.text = fileName;
        }

        private void Read() {
            
        }
        
        private void Write() {
            
        }

        private void Execute() {
            
        }
    }
}