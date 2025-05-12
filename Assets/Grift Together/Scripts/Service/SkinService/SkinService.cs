using UnityEngine;

namespace GriftTogether {

    public class SkinService: IService {

        private const string NAME_SKIN_SONTAINER = "Need set name";

        private SkinContainer _skinContainer;

        private SkinServiceAgent _currentAgent;


        public SkinService() {

            var temp = Resources.Load(NAME_SKIN_SONTAINER);

            _skinContainer = temp as SkinContainer;
            if(_skinContainer == null) {
                Debug.LogError("Skin service can't find SKIN CONTAINER!");
            }
        }

        public void SetSkinAgent(SkinServiceAgent agent) {
            _currentAgent = agent;
        }

        public void ResolveCurrentSkin() {

        }

        public string MoveNext<T>() {
            return "";
        }

        public string MoveBack<T>() {
            return "";
        }

        public async void SaveCurrentSkin() {

        }
    }
}
