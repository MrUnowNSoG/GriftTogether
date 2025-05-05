using UnityEngine;

namespace GriftTogether {

    public class LoginPresenter : IPresenter {

        private Canvas _overlayCanvas;

        private LoginView _view;

        private TestFireStoreDTO _testFireStoreDTO = new TestFireStoreDTO();
        private int _id = 0;

        public LoginPresenter(Canvas canvas) {
            _overlayCanvas = canvas;
        }

        public void Initialize() {
            _view = GameRoot.PrefabManager.InstantiatePrefab(LoginPrefabType.LoginView, _overlayCanvas.gameObject).GetComponent<LoginView>();
            _view.Initialize(this);
            _view.ShowUI();
        }

        public void SavePlayer() {
            GameRoot.FireStoreManager.SaveToCloud(_testFireStoreDTO);
            _id++;
            UpdateLog("SAVE:");
        }

        public void RandomPlayer() {
            _testFireStoreDTO.UserName = "User" + Random.Range(0, 101).ToString();

            _testFireStoreDTO.HP = Random.Range(1, 101);

            float dif = Random.Range(0, 6);
            dif += (Random.Range(1, 11) / 10f);
            _testFireStoreDTO.Difficult = dif;

            UpdateLog("RANDOM:");
        }

        public async void LoadPlayer() {
            _testFireStoreDTO = await GameRoot.FireStoreManager.LoadFromCloud(_id);
            UpdateLog("LOAD:");
        }

        private void UpdateLog(string str) {
            _view.UpdateLog(str + " " + _testFireStoreDTO.ToString());
        }

        public void Deinitialize() {

        }

        public void CloseUI() {
        
        }
    }
}
