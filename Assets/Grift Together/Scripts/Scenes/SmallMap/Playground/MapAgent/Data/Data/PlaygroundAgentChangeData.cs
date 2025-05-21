using UnityEngine;

namespace GriftTogether {

    public class PlaygroundAgentChangeData {


        private string _nameAgent;
        public string GetName => _nameAgent;


        private string _descriptionAgent;
        public string GetDescription => _descriptionAgent;


        private bool _isSubscribe;
        public bool IsSubscribe => _isSubscribe;


        private int _percentGap;
        public int GetPercentGap => _percentGap;
        
        public PlaygroundAgentChangeData(string name, string description, bool isSubscribe, int percentGap) {
            _nameAgent = name;
            _descriptionAgent = description;
            _isSubscribe = isSubscribe;
            _percentGap = percentGap;
        }
    }
}
