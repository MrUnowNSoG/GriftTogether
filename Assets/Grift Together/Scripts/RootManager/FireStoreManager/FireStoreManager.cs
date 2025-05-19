using Firebase.Firestore;
using System.Threading.Tasks;

namespace GriftTogether {

    public class FireStoreManager {

        private FirebaseFirestore _firestore;

        public FireStoreManager() {
            _firestore = FirebaseFirestore.DefaultInstance;
        }

        public async Task<bool> ExistFile(string collection, string nameFile, bool useLoverCase = false) {

            string name = nameFile;
            if(useLoverCase) name = name.ToLower();

            var snapshot = await _firestore.Document($"{collection}/{name}").GetSnapshotAsync();
            return snapshot.Exists;
        }

        public async Task SaveToCloud<T>(T dto, string collection, string nameFile, bool useLoverCase = false) where T : IFireStoreDTO {
            string name = nameFile;
            if (useLoverCase) name = name.ToLower();

            await _firestore.Document($"{collection}/{name}").SetAsync(dto);
        }

        public async Task<T> TryGetFile<T>(string collection, string nameFile, bool useLoverCase = false) where T : IFireStoreDTO {

            T result = default(T);

            var snapshot = await _firestore.Document($"{collection}/{nameFile}").GetSnapshotAsync();

            if (snapshot.Exists) {
                result = snapshot.ConvertTo<T>();
            }

            return result;
        }
    }
}
