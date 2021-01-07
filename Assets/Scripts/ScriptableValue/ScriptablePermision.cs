using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace Game
{

    [CreateAssetMenu(fileName = "ScriptablePermisionValue")]
    public class ScriptablePermisionValue : ScriptableObject {
        
        public string fileName;
		public Sprite iconImage;
        public List<User> Users;
    }

	[Serializable]
    public class User {

		public string userName;
        public bool read;
        public bool write;
        public bool execute;
    }
}