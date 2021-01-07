﻿using System;
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
            RegisterUsers();
            _registerUserEventListener.OnEventHappened += OnUserRegistered;
        }

        private void OnDisable() {
            _registerUserEventListener.OnEventHappened -= OnUserRegistered;
        }

        private void OnUserRegistered() {
            var newRecord = new SaveData {
                login = _parsedLogin.value,
                password = _parsedPassword.value
            };
               
            _saveDatas.Add(newRecord);

            SaveToFile();

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
        
        private void RegisterUsers() {
            RegisterUser("admin", "admin");
            RegisterUser("Сергей", "12345");
            RegisterUser("Юрий", "12345");
        }
        
        private void RegisterUser(string newLogin, string newPassword) {
            var user = new SaveData {
                login = newLogin.ToLower(), 
                password = newPassword
            };
            var found = SavedDatas.Find(login=> login.login == newLogin);
            if (found != null) {
                Debug.Log("User already exists");
            }
            else {
                _saveDatas.Add(user);
            }
            SaveToFile();
        }
    }
}