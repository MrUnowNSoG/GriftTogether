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

            string userName = GameRoot.PlayerGlobalManager.UserName;
            int countWin = GameRoot.PlayerGlobalManager.CountWin;
            int countCoin = GameRoot.PlayerGlobalManager.CountCoin;

            _view.UpdateData(userName, countWin, countCoin);
        }


        public void ShowUI() => _view.gameObject.SetActive(true);

        public void CloseUI() => _view.gameObject.SetActive(false); 

        public void Deinitialize() {
        }

    }
}
