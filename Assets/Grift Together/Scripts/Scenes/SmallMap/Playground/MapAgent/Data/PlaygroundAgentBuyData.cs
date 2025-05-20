using UnityEngine;

namespace GriftTogether {

    public class PlaygroundAgentBuyData {

        private string _nameAgent;
        public string GetName => _nameAgent;


        private string _descriptionAgent;
        public string GetDescription => _descriptionAgent;


        private int _rentPrice;
        public int GetRent => _rentPrice;


        private int _priceAgent;
        public int GetPrice => _priceAgent;


        public PlaygroundAgentBuyData(string nameAgent, string descriptionAgent, int rentPrice, int priceAgent) {
            _nameAgent = nameAgent;
            _descriptionAgent = descriptionAgent;
            _rentPrice = rentPrice;
            _priceAgent = priceAgent;
        }
    }
}
