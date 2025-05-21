using UnityEngine;

namespace GriftTogether {


    [CreateAssetMenu(fileName = "PlaygroundAgentSubscibeContainer", menuName = "Scriptable Objects/PlaygroundAgentSubscibeContainer")]
    public class PlaygroundAgentSubscibeContainer : ScriptableObject {


        [SerializeField] private string _indeficationAgent;
        public string GetIndeficationAgent => _indeficationAgent;


        [SerializeField] private string _nameAgent;
        public string GetNameAgent => _nameAgent;


        [Space(10)]
        [TextArea][SerializeField] private string _description;
        public string GetDescription => _description;


        [SerializeField] private int _priceGap;
        public int GetPriceGap => _priceGap;
    }
}
