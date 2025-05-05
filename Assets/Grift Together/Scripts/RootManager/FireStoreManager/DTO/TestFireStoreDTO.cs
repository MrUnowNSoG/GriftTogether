using Firebase.Firestore;
using UnityEngine;

namespace GriftTogether {

    [FirestoreData]
    public class TestFireStoreDTO {

        private string _userName = "UserName";
        private int _hp = 100;
        private float difficult = 1.3f;

        [FirestoreProperty]
        public string UserName { 
            get { return _userName; } 
            set { _userName = value; } 
        }

        [FirestoreProperty]
        public int HP { 
            get { return _hp; } 
            set {  _hp = value; } 
        }

        [FirestoreProperty]
        public float Difficult { 
            get {  return difficult; } 
            set {  difficult = value; } 
        }

        public override string ToString() {
            return $"UserName:{UserName}; HP:{HP}; Difficult:{Difficult}; ";
        }
    }
}
