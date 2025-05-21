using UnityEngine;

namespace GriftTogether {

    public class PlaygroundAgentRentData {

        private string _nameAgent;
        public string GetName => _nameAgent;


        private int _rentPrice;
        public int GetRent => _rentPrice;


        private string _rentDescription;
        public string GetRentDescription => _rentDescription;


        public PlaygroundAgentRentData(string nameAgent, string rentDescription, int rentPrice) {
            _nameAgent = nameAgent;
            _rentDescription = rentDescription;
            _rentPrice = rentPrice;
        }

    }
}
