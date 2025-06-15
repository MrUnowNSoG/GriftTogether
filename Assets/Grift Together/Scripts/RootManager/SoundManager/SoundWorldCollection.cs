using System.Collections.Generic;
using UnityEngine;

namespace GriftTogether {

    [System.Serializable] public struct SoundStruct {
        public TypeSoundButton type;
        public AudioSource audioSource;
    }


    public class SoundWorldCollection : MonoBehaviour {

        [SerializeField] public List<SoundStruct> _allSound;
        [SerializeField] private AudioSource _audioSource;
    
        
        public void PlayMusic() {
            _audioSource.Play();
        }

        public void StopMusic() {
            _audioSource.Stop();
        }

        public void PlayUISound(TypeSoundButton soundButton) {

            foreach (var sound in _allSound) {

                if(sound.type == soundButton) {
                    sound.audioSource.Play();
                    return;
                }
            }
        }
    }
}
