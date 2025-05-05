using Firebase;
using Firebase.Firestore;
using System.Threading.Tasks;
using UnityEngine;

namespace GriftTogether {

    public class FireStoreManager {

        private FirebaseFirestore _firestore;
        private int _countFile = 0;

        public FireStoreManager() {
            _firestore = FirebaseFirestore.DefaultInstance;
        }

        public void SaveToCloud(TestFireStoreDTO dto) {
            _firestore.Document($"{FireStoreConst.TEST_COLLECTION}/{_countFile.ToString()}").SetAsync(dto);
            Debug.Log($"{FireStoreConst.TEST_COLLECTION}/{_countFile.ToString()}");
            _countFile++;
        }

        public async Task<TestFireStoreDTO> LoadFromCloud(int id) {

            int ID = id - 1;
            TestFireStoreDTO result = null;

            if(ID > 0 && ID < _countFile) {

                var snapshot = await _firestore.Document($"{FireStoreConst.TEST_COLLECTION}/{ID.ToString()}").GetSnapshotAsync();

                if (snapshot.Exists) {
                    result = snapshot.ConvertTo<TestFireStoreDTO>();
                }
            }

            return result;
        }
    }
}
