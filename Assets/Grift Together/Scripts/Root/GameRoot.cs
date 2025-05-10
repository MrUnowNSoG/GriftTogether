using UnityEngine;

namespace GriftTogether {
    
    public static class GameRoot {

        public static PrefabManager PrefabManager;
        public static CoroutinsManager CoroutinsManager;
        public static ScenesManager ScenesManager;

        public static PlayerPrefsManager PlayerPrefsManager;
        public static PlayerGlobalManager PlayerGlobalManager;

        public static LocalizationManager LocalizationManager;
        public static SoundManager SoundManager;

        public static ServiceLocator ServiceLocator;

        public static FireStoreManager FireStoreManager;
    }
}
