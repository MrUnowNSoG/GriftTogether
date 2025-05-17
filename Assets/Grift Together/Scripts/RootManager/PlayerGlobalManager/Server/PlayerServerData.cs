using System.Threading;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;

namespace GriftTogether {
    public class PlayerServerData {

        public readonly string LoginPlayr;

        public readonly string NameUser;
        public readonly int CountWin;
        public readonly int CountCoin;


        public PlayerServerData(string login, string nameUser, int win, int coin) { 
            LoginPlayr = login;
            NameUser = nameUser;
            CountWin = win;
            CountCoin = coin;
        }
    }
}
