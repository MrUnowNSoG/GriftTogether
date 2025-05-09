using UnityEngine;

namespace GriftTogether {
    public class MainMenuManager : BaseSceneManager {

        private Canvas _mainCanvas;

        private MainMenuPresenter _presenter;

        public MainMenuManager(Canvas mainCanvas) {
            _mainCanvas = mainCanvas;
        }

        public override void Init() {
            _presenter = new MainMenuPresenter(_mainCanvas);
            _presenter.Initialize();   
        }


        public override void DeInit() {}
    }
}
