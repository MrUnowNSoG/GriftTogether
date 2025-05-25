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

        private bool _isMaxLVL;
        public bool ISMaxLVL => _isMaxLVL;

        public PlaygroundAgentBuyData(string nameAgent, string descriptionAgent, int rentPrice, int priceAgent, bool isMaxLvl) {
            _nameAgent = nameAgent;
            _descriptionAgent = descriptionAgent;
            _rentPrice = rentPrice;
            _priceAgent = priceAgent;
            _isMaxLVL = isMaxLvl;
        }
    }
}
