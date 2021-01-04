using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;

namespace UI {

    public class UIManager : MonoBehaviour {

        public static UIManager Instance;
        
        [SerializeField]
        private List<GameObject> _menuScreens = new List<GameObject>();

        [SerializeField] 
        private EventListener _loginEventListener;
        
        [SerializeField] 
        private EventListener _goToRegistrationScreenEventListener;
        
        [SerializeField] 
        private EventListener _exitToLoginScreenEventListener;

        private void Start() {
            ShowScreen(0);
        }
        
        private void Awake() {
            if (Instance != null) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        private void OnEnable() {
            SubscribeToEvents();
        }

        protected virtual void OnDisable() {
            UnsubscribeFromEvents();
        }

        private void SubscribeToEvents() {
            _loginEventListener.OnEventHappened += ShowAdmin;
            _exitToLoginScreenEventListener.OnEventHappened += ShowLogin;
            _goToRegistrationScreenEventListener.OnEventHappened += ShowRegister;
        }
        
        private void UnsubscribeFromEvents() {
            _loginEventListener.OnEventHappened -= ShowAdmin;
            _exitToLoginScreenEventListener.OnEventHappened -= ShowLogin;
            _goToRegistrationScreenEventListener.OnEventHappened -= ShowRegister;
        }

        public void ShowLogin() {
            ShowScreen(0);
        }

        public void ShowRegister() {
            ShowScreen(1);
        }
        
        public void ShowAdmin() {
            ShowScreen(2);
        }
        
        public void ShowScreen(int screenId) {
            HideAllScreens();
            _menuScreens[screenId].SetActive(true);
        }

        public void HideAllScreens() {
            foreach (var screen in _menuScreens) {
                screen.SetActive(false);
            }
        }
    }
}
