using Firebase.Firestore;
using UnityEngine;

namespace GriftTogether {

    [FirestoreData]
    public class PlayerFireStoreDTO : IFireStoreDTO {

        //���� 
        [FirestoreProperty] public string Login { get; set; }
        [FirestoreProperty] public string Nickname { get; set; }


        //��������������
        [FirestoreProperty] public string Hash { get; set; }
        [FirestoreProperty] public string Salt { get; set; }
        [FirestoreProperty] public int Iterations { get; set; }


        //C���������
        [FirestoreProperty] public long Gold { get; set; }
        [FirestoreProperty] public int CountWinSessions { get; set; }


        public override string ToString() {
            return $"{Nickname} connect to game! \n Stats: GOLD:{Gold}, Win:{CountWinSessions}"; 
        }
    }
}
