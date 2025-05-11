using UnityEngine;

namespace GriftTogether {

    public class ResolutionScreenServer : IService{

        private FullScreenMode _currentMode;
        public string GetCurrentMode => _currentMode.ToString();

        private string _currentResolution;
        public string GetCurrentResolution => _currentResolution;


        private ResolutionScreen _resolutionScreen;

        public ResolutionScreenServer() {
            _currentMode = Screen.fullScreenMode;
            _currentResolution = ResolutionScreen.NATIVE;

            _resolutionScreen = new ResolutionScreen();
        }

        public void SetScreenType(FullScreenMode mode) {

            Screen.fullScreenMode = mode;
            _currentMode = mode;

            switch(mode) {

                case FullScreenMode.ExclusiveFullScreen:
                    SetNativeResolution();
                    break;

                case FullScreenMode.FullScreenWindow:
                    SetNativeResolution();
                    break;

                case FullScreenMode.MaximizedWindow:
                    TrySetScreenSize(ResolutionScreen.HD);
                    break;

                case FullScreenMode.Windowed:
                    TrySetScreenSize(ResolutionScreen.HD);
                    break;

                default:
                    return;
            }
        }

        public bool TrySetScreenSize(string resolutionName) {

            if(_resolutionScreen.GameResolution.ContainsKey(resolutionName)) {

                Resolution resolution = _resolutionScreen.GameResolution[resolutionName];

                if (CanSetSizeScreen()) {

                    if(_currentResolution == resolutionName) return true;
                    
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

        public void SetNativeResolution() {
            Resolution native = Screen.currentResolution;
            Screen.SetResolution(native.width, native.height, true);
            _currentResolution = ResolutionScreen.NATIVE;
        }
    }
}
