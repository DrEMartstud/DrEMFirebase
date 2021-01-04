using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    public class Entity : MonoBehaviour {

        [SerializeField]
        private List<GameObject> _readPermision;
        
        [SerializeField]
        private List<GameObject> _writePermision;
        
        [SerializeField]
        private List<GameObject> _executePermision;
        
    }
}