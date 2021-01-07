using UnityEngine;

namespace Game
{

    [CreateAssetMenu(fileName = "ScriptablePermisionValue")]
    public class ScriptablePermisionValue : ScriptableObject {
        
        public string user;
        public bool read;
        public bool write;
        public bool execute;

    }
}