using UnityEngine;

namespace GriftTogether {

    public class ResolutionScreenService : IService {

        private FullScreenMode _currentMode;
        public string GetCurrentMode => _currentMode.ToString();

        private string _currentResolution;
        public string GetCurrentResolution => _currentResolution;


        private ResolutionScreenConst _resolutionScreen;
        public ResolutionScreenConst GetResolutinConst => _resolutionScreen;

        public ResolutionScreenService() {
            _currentMode = Screen.fullScreenMode;
            _currentResolution = ResolutionScreenConst.NATIVE;

            _resolutionScreen = new ResolutionScreenConst();
        }

        public void SetScreenType(FullScreenMode mode) {

            Screen.fullScreenMode = mode;
            _currentMode = mode;

            switch (mode) {

                case FullScreenMode.ExclusiveFullScreen:
                    SetNativeResolution();
                    break;

                case FullScreenMode.FullScreenWindow:
                    SetNativeResolution();
                    break;

                case FullScreenMode.MaximizedWindow:
                    TrySetScreenSize(ResolutionScreenConst.HD);
                    break;

                case FullScreenMode.Windowed:
                    TrySetScreenSize(ResolutionScreenConst.HD);
                    break;

                default:
                    return;
            }
        }

        public bool TrySetScreenSize(string resolutionName) {

            if (_resolutionScreen.GameResolution.ContainsKey(resolutionName)) {

                if(resolutionName == ResolutionScreenConst.NATIVE) {
                    SetNativeResolution();
                    _currentResolution = resolutionName;
                    return true;
                }


                Resolution resolution = _resolutionScreen.GameResolution[resolutionName];

                if (CanSetSizeScreen()) {

                    if (_currentResolution == resolutionName) return true;

                    Screen.SetResolution(resolution.width, resolution.height, false);
                    _currentResolution = resolutionName;
                    return true;
                }
            }

            return false;
        }

        private bool CanSetSizeScreen() {
            if (_currentMode == FullScreenMode.MaximizedWindow || _currentMode == FullScreenMode.Windowed) return true;
            return false;
        }

        public bool CanChangeScreenSize(int typeScreen) {
            FullScreenMode mode = (FullScreenMode)typeScreen;

            if (mode == FullScreenMode.MaximizedWindow || mode == FullScreenMode.Windowed) return true;
            return false;
        }

        private void SetNativeResolution() {
            Resolution native = Screen.currentResolution;
            Screen.SetResolution(native.width, native.height, true);
            _currentResolution = ResolutionScreenConst.NATIVE;
        }

    }
}
