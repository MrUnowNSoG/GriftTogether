using Firebase.Firestore;
using UnityEngine;

namespace GriftTogether {

    [FirestoreData]
    public class BugReportFireStoreDTO : IFireStoreDTO {

        [FirestoreProperty] public string UserName { get; set; }
        [FirestoreProperty] public string DataTime { get; set; }
        [FirestoreProperty] public int CountPlayer { get; set; }


        [FirestoreProperty] public string TitleBug { get; set; }
        [FirestoreProperty] public string DescriptionBug { get; set; }
    }
}
