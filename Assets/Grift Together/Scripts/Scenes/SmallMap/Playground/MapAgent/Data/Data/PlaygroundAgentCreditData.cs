using UnityEngine;

namespace GriftTogether {

    public class PlaygroundAgentCreditData {

        private string _nameAgent;
        public string GetNameAgent => _nameAgent;


        private string _description;
        public string GetDescription => _description;


        private int _countMaxCredit;
        public int GetCountMaxCredit => _countMaxCredit;


        private int _countMinCredit;
        public int GetCountMinCredit => _countMinCredit;


        private int _percentMaxCredit;
        public int GetPercentMaxCredit => _percentMaxCredit;


        private int _countMaxRound;
        public int GetCountMaxRound => _countMaxRound;


        private int _countMinRound;
        public int GetCountMinRound => _countMinRound;


        private int _percentMaxRound;
        public int GetPercentMaxRound => _percentMaxRound;


        private bool _isHadCredit;
        public bool GetIsHadCredit => _isHadCredit;

        private int _sizeCredit;
        public int GetSizeCredit => _sizeCredit;

        public PlaygroundAgentCreditData(string name, string description, bool isHadCredit, int sizeCredit, 
                                        int maxCoin, int minCoin, int cointPercent, 
                                        int maxRound, int minRound, int percentRound) { 
            
            _nameAgent = name;
            _description = description;

            _sizeCredit = sizeCredit;
            _isHadCredit = isHadCredit;

            _countMaxCredit = maxCoin;
            _countMinCredit = minCoin;
            _percentMaxCredit = cointPercent;

            _countMaxRound = maxRound;
            _countMinRound = minRound;
            _percentMaxRound = percentRound;
        }
    }
}
