using UnityEngine;
using UnityEngine.Audio;

namespace GriftTogether {


    public class SoundManager {

        private const string NAME_AUDIOMIXER = "GameAudioMixer";
        private const string SOUND_MANAGER_PREFAB = "SoundManagerPrefab";

        private const string MASTER_VOLUME = "MasterVolume";
        private const string SOUND_VOLUME = "SoundVolume";
        private const string MUSIC_VOLUME = "MusicVolume";

        private AudioMixer _gameAudioMixer;
        private SoundWorldCollection _soundWorldCollection;

        public SoundManager(bool masterState, float volumeSound, float volumeMusic) {
            SetMaster();
            SetSetting(masterState, volumeSound, volumeMusic);
        }

        private void SetMaster() {

            var temp = Resources.Load(NAME_AUDIOMIXER);

            _gameAudioMixer = temp as AudioMixer;

            if (_gameAudioMixer == null) {
                Debug.LogError($"Can't find Audio Mixer: {NAME_AUDIOMIXER}!");
                return;
            }

        }

        public void SetSetting(bool masterState, float volumeSound, float volumeMusic) {

            float volume = masterState ? SoundManagerConst.TURN_ON_SOUND : SoundManagerConst.TURN_OFF_SOUND;
            _gameAudioMixer.SetFloat(MASTER_VOLUME, volume);
            _gameAudioMixer.SetFloat(SOUND_VOLUME, ConvertVolume(SoundManagerConst.GAME_VOLUME_ON, volumeSound));
            _gameAudioMixer.SetFloat(MUSIC_VOLUME, ConvertVolume(SoundManagerConst.GAME_VOLUME_ON, volumeMusic));
        }

        public void SetLateSetting(bool masterState, float volumeSound, float volumeMusic) {
            SetSetting(masterState, volumeSound, volumeMusic);

            var temp = Resources.Load(SOUND_MANAGER_PREFAB);
            GameObject prefab = temp as GameObject;

            if (prefab == null) {
                Debug.LogError($"Critical error can't find {SOUND_MANAGER_PREFAB}!");
                return;
            }


            _soundWorldCollection = GameObject.Instantiate(prefab.gameObject).GetComponent<SoundWorldCollection>();
            GameObject.DontDestroyOnLoad(_soundWorldCollection.gameObject);

            _soundWorldCollection.PlayMusic();
        }


        private float ConvertVolume(float max, float current) {

            if(current <= 0) current = SoundManagerConst.GAME_VOLUME_OFF;
            if (current > max) current = max;
            float t = current / max;

            float volume = Mathf.Lerp(SoundManagerConst.TURN_OFF_SOUND, SoundManagerConst.TURN_ON_SOUND, t);
            return volume;
        }


        //API
        public void PlayButtonSound(TypeSoundButton typeSoundButton) {
            
            if(_soundWorldCollection == null) {
                Debug.LogError($"Critical error can't find {SOUND_MANAGER_PREFAB}!");
                return;
            }

            _soundWorldCollection.PlayUISound(typeSoundButton);
        }

    }
}
