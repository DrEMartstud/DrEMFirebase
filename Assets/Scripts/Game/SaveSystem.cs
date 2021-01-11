using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using Events;
using UnityEngine;

namespace Game {

    public class SaveSystem : MonoBehaviour {

        [Serializable]
        public class SaveData {

            public string login;
            public string password;

        }

        [Serializable]
        private class SavedDataWrapper {

            public List<SaveData> saveDatas;

        }

        [SerializeField] 
        private EventListener _registerUserEventListener;

        [SerializeField] 
        private EventDispatcher _userRegisteredEventDispatcher;

        [SerializeField]
        private ScriptableStringValue _parsedLogin;
        
        [SerializeField]
        private ScriptableStringValue _parsedPassword;

        private string _filePath;
        private static List<SaveData> _saveDatas;
        public static List<SaveData> SavedDatas => _saveDatas;

        private void Awake() {
            _saveDatas = new List<SaveData>();
            _filePath = Path.Combine(Application.persistentDataPath, "UserData.drem");
            LoadFromFile();
        }

        private void OnEnable() {
            _registerUserEventListener.OnEventHappened += OnUserRegistered;
        }

        private void OnDisable() {
            _registerUserEventListener.OnEventHappened -= OnUserRegistered;
        }

        private void OnUserRegistered() {
            RegisterUser(_parsedLogin.value, _parsedPassword.value);
            _userRegisteredEventDispatcher.Dispatch();
        }

        private SavedDataWrapper GetWrapper() {
            var wrapper = new SavedDataWrapper {
                saveDatas = _saveDatas
            };
            return wrapper;
        }

        private void LoadFromFile() {
            if (!File.Exists(_filePath)) {
                PreRegisterUsers();
                return;
            }

            var binaryFormatter = new BinaryFormatter();
            using (FileStream fileStream = File.Open(_filePath, FileMode.Open)) {
                var wrapper = (SavedDataWrapper) binaryFormatter.Deserialize(fileStream);
                _saveDatas = wrapper.saveDatas;
            }
        }

        private void SaveToFile() {
            var wrapper = GetWrapper();
            var binaryFormatter = new BinaryFormatter();
            using (FileStream fileStream = File.Open(_filePath, FileMode.OpenOrCreate)) {
                binaryFormatter.Serialize(fileStream, wrapper);
            }
        }
        
        private void PreRegisterUsers() {
            RegisterUser("admin", "admin");
            RegisterUser("Сергей", "12345");
            RegisterUser("Юрий", "12345");
        }
        
        private void RegisterUser(string newLogin, string newPassword) {
            var user = new SaveData {
                login = newLogin.ToLower(), 
                password = newPassword
            };
            
            var foundUser = _saveDatas.Find(log=> log.login == user.login);
            if (foundUser != null) {
                Debug.Log(foundUser.login);
                return;
            }
            _saveDatas.Add(user);
            SaveToFile();
        }
    }
}