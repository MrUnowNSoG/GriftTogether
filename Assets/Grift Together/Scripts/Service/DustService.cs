using UnityEngine;

namespace GriftTogether {

    public class DustService : IService {

        private const int MAX_DUST = 9;
        private const int MIN_DUST = 1;
        private const int MAX_COUNT_DUST = 4;
        private const int MIN_COUNT_DUST = 1;

        private const string CHANGE_DUST_TEXT = "Master change DUST!";

        private int _currentCountDust;
        private int _currentMinDust;
        private int _currentMaxDust;
        
        public DustService() {
            SetDefaultDust();
        }

        public void SetDefaultDust() {
            _currentCountDust = 2;
            _currentMinDust = 1;
            _currentMaxDust = 6;
        }

        public void ChangeDust(int countDust, int minDust, int maxDust) {
            _currentCountDust = countDust;
            _currentMinDust = minDust;
            _currentMaxDust = maxDust;
        }

        public (int, int, int) RandomDust() {
            _currentCountDust = Random.Range(MIN_COUNT_DUST, MAX_COUNT_DUST);
            _currentMaxDust = Random.Range(MIN_DUST, MAX_DUST);
            _currentMinDust = Random.Range(MIN_DUST, _currentMaxDust);

            return(_currentCountDust, _currentMinDust, _currentMaxDust);
        }

        public int GenerateDustStep() {
            int value = 0;
            for (int i = 0; i < _currentCountDust; i++) value = Random.Range(_currentMinDust, _currentMaxDust + 1);
            return value;
        }

        public string ChangeDustMessage() => CHANGE_DUST_TEXT;

        public override string ToString() {
            return $"{_currentCountDust}x{_currentMinDust}x{_currentMaxDust}";
        }
    }
}
