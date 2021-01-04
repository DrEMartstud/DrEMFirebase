using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;

namespace Game {

    public class LoginSystem : MonoBehaviour {

        [Serializable]
        public class User {

            public string login;
            public string password;
        }

        [SerializeField]
        private ScriptableStringValue _parsedLogin;
        
        [SerializeField]
        private ScriptableStringValue _parsedPassword;

        [SerializeField] 
        private ScriptableStringValue _errorMessageStringValue;
        
        [SerializeField] 
        private EventListener _loginEventListener;

        [SerializeField] 
        private EventDispatcher _loggedInEventDispatcher;
        
        [SerializeField] 
        private EventDispatcher _unsuccessfulLogInEventDispatcher;

        private void Awake() {
        }

        private void OnEnable() {
            SubscribeToEvents();
        }

        protected virtual void OnDisable() {
            UnsubscribeFromEvents();
        }
        
        private void SubscribeToEvents() {
            _loginEventListener.OnEventHappened += Login;
        }
        
        private void UnsubscribeFromEvents() {
            _loginEventListener.OnEventHappened -= Login;
        }

        private void Login() {
            StartCoroutine(CheckIfCanLogIn());
        }
        
        private IEnumerator CheckIfCanLogIn() {
            yield return null;
            var foundUser = SaveSystem.SavedDatas.Find(log=> log.login == _parsedLogin.value);
            if (foundUser != null && foundUser.password == _parsedPassword.value) {
                _parsedPassword.value = "";
                _loggedInEventDispatcher.Dispatch();
                yield break;
            }
            _errorMessageStringValue.value = "Неправильный логин или пароль!";
            _unsuccessfulLogInEventDispatcher.Dispatch();
        }
        
        
    }
}