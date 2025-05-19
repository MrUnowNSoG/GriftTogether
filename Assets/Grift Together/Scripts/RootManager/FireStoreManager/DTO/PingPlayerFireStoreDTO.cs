using Firebase.Firestore;

namespace GriftTogether {

    [FirestoreData]
    public class PingPlayerFireStoreDTO : IFireStoreDTO {

        [FirestoreProperty] public string UserName { get; set; }
        [FirestoreProperty] public string DataTime { get; set; }
        [FirestoreProperty] public int CountPlayer { get; set; }
        [FirestoreProperty] public int Ping { get; set; }
    }
}
