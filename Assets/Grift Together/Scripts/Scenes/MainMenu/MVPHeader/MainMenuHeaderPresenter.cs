using UnityEngine;

namespace GriftTogether
{
    public class MainMenuHeaderPresenter : IPresenter {


        private GameObject _root;

        private MainMenuHeaderUiView _view;

        public MainMenuHeaderPresenter(GameObject root) {
            _root = root;
        }

        public void Initialize() {
            _view = GameRoot.PrefabManager.InstantiatePrefab(MainMenuPrefabType.HeaderView, _root).GetComponent<MainMenuHeaderUiView>();

            string userName = GameRoot.PlayerGlobalManager.GetUserName;
            int countWin = GameRoot.PlayerGlobalManager.GetCountWin;
            int countCoin = GameRoot.PlayerGlobalManager.GetCountCoin;

            _view.UpdateData(userName, countWin, countCoin);
        }


        public void ShowUI() => _view.gameObject.SetActive(true);

        public void CloseUI() => _view.gameObject.SetActive(false); 

        public void Deinitialize() {
        }

    }
}
