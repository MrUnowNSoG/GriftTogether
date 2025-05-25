using UnityEngine;

namespace GriftTogether {


    [CreateAssetMenu(fileName = "PlaygroundAgentCreditContainer", menuName = "Scriptable Objects/PlaygroundAgentCreditContainer")]
    public class PlaygroundAgentCreditContainer : ScriptableObject {

        [Space(0)][Header("Main")]
        [SerializeField] private string _indeficationAgent;
        public string GetIndeficationAgent => _indeficationAgent;

        [SerializeField] private string _nameAgent;
        public string GetNameAgent => _nameAgent;


        [Space(10)][Header("Description")]
        [TextArea][SerializeField] private string _description;
        public string GetDescription => _description;


        [Space(10)][Header("Info for coin")]
        [SerializeField] private int _countMaxCredit;
        public int GetCountMaxCredit => _countMaxCredit;

        [SerializeField] private int _countMinCredit;
        public int GetCountMinCredit => _countMinCredit;
        
        [SerializeField] private int _percentMaxCredit;
        public int GetPercentMaxCredit => _percentMaxCredit;


        [Space(10)][Header("Info for time")]
        [SerializeField] private int _countMaxRound;
        public int GetCountMaxRound => _countMaxRound;

        [SerializeField] private int _countMinRound;
        public int GetCountMinRound => _countMinRound;

        [SerializeField] private int _percentMaxRound;
        public int GetPercentMaxRound => _percentMaxRound;
    }

}
