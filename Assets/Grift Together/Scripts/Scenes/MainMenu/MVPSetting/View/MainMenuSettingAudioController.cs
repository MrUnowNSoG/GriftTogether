using System;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace GriftTogether {

    public class MainMenuSettingAudioController : MonoBehaviour {

        [Space(0)][Header("Master state")]
        [SerializeField] private Toggle _masterToggle;

        [Space(10)][Header("Sound UI")]
        [SerializeField] private TextMeshProUGUI _soundText;
        [SerializeField] private Slider _soundSlider;

        [Space(0)][Header("Music UI")]
        [SerializeField] private TextMeshProUGUI _musicText;
        [SerializeField] private Slider _musicSlider;

        private bool _masterState;
        private float _soundVolume;
        private float _musicVolume;

        public event Action<bool, float, float> onChangeAudio;
    
        public void Init(bool masterState, float soundVolume, float musicVolume) {
            InitUI(masterState, soundVolume, musicVolume);
            InitEventUI();
        }

        private void InitUI(bool masterState, float soundVolume, float musicVolume) {
            _masterToggle.isOn = masterState;
            _masterState = masterState;

            InitSlider(_soundSlider, soundVolume);
            _soundText.text = soundVolume.ToString();
            _soundVolume = soundVolume;

            InitSlider(_musicSlider, musicVolume);
            _musicText.text = musicVolume.ToString();
            _musicVolume = musicVolume;
        }

        private void InitSlider(Slider slider, float currentValue) {
            slider.maxValue = SoundManagerConst.GAME_VOLUME_ON;
            slider.minValue = SoundManagerConst.GAME_VOLUME_OFF;
            slider.wholeNumbers = true;
            slider.value = currentValue;
        }

        private void InitEventUI() {
            _masterToggle.onValueChanged.AddListener(ChangeMasterState);
            _soundSlider.onValueChanged.AddListener(ChangeSoundState);
            _musicSlider.onValueChanged.AddListener(ChangeMusicState);
        }

        private void ChangeMasterState(bool masterState) {
            _masterState = masterState;
            onChangeAudio?.Invoke(_masterState, _soundVolume, _musicVolume);
        }

        private void ChangeSoundState(float volume) {
            _soundVolume = volume;
            _soundText.text = volume.ToString();
            onChangeAudio?.Invoke(_masterState, _soundVolume, _musicVolume);
        }

        private void ChangeMusicState(float volume) { 
            _musicVolume = volume;
            _musicText.text = volume.ToString();
            onChangeAudio?.Invoke(_masterState, _soundVolume, _musicVolume);

        }

        public void Deinitialize() {
            _masterToggle.onValueChanged.RemoveListener(ChangeMasterState);
            _soundSlider.onValueChanged.RemoveListener(ChangeSoundState);
            _musicSlider.onValueChanged.RemoveListener(ChangeMusicState);
        }
    }

}
