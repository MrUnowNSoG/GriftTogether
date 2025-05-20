using Firebase.Firestore;
using UnityEngine;

namespace GriftTogether {

    [FirestoreData]
    public class BuildStatsFireStoreDTO : IFireStoreDTO {

        [FirestoreProperty] public string Induficator { get; set; }
        [FirestoreProperty] public string LastActivate { get; set; }
        [FirestoreProperty] public int CountActivate { get; set; }

    }
}
