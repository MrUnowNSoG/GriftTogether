using UnityEngine;
using UnityEngine.Audio;

namespace GriftTogether {


    public class SoundManager {

        private const string NAME_AUDIOMIXER = "GameAudioMixer";

        private const string MASTER_VOLUME = "MasterVolume";
        private const string SOUND_VOLUME = "SoundVolume";
        private const string MUSIC_VOLUME = "MusicVolume";

        private AudioMixer _gameAudioMixer;

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

        private void SetSetting(bool masterState, float volumeSound, float volumeMusic) {

            float volume = masterState ? SoundManagerConst.TURN_ON_SOUND : SoundManagerConst.TURN_OFF_SOUND;
            _gameAudioMixer.SetFloat(MASTER_VOLUME, SoundManagerConst.TURN_ON_SOUND);
            _gameAudioMixer.SetFloat(SOUND_VOLUME, ConvertVolume(SoundManagerConst.GAME_VOLUME_ON, volumeSound));
            _gameAudioMixer.SetFloat(MUSIC_VOLUME, ConvertVolume(SoundManagerConst.GAME_VOLUME_ON, volumeMusic));
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

            //TODO: add sound
            switch (typeSoundButton) {

                case TypeSoundButton.BaseButton:


                default:
                    break;
            }
        }

    }
}
