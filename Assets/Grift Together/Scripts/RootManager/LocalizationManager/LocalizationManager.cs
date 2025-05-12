using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using WebSocketSharp;

namespace GriftTogether {

    public class LocalizationManager {

        public const int COUNT_GAME_LANGUAGE = 3;

        private const string NAME_ALL_DICTIONARY = "Localization/Localization";
        private const string NAME_MISSING_KEY = @"Assets\Grift Together\Resources\Localization\MissingKey.txt";

        private TextAsset _csvFile;

        private LocalizationLanguage _currentLanguage;
        private Dictionary<string, string> _currentDictionary;
        private readonly HashSet<string> _missingKeys;


        public LocalizationManager(LocalizationLanguage language) {
            _currentLanguage = language;
            _currentDictionary = new Dictionary<string, string>();
            _missingKeys = new HashSet<string>();

            LoadDataLocalization();
            SetLanguage(_currentLanguage);
        }

        private void LoadDataLocalization() {

            _csvFile = Resources.Load<TextAsset>(NAME_ALL_DICTIONARY);

            if (_csvFile == null) {
                Debug.LogError($"{NAME_ALL_DICTIONARY} can't find! Critical error!");
                return;
            }
        }

        public void SetLanguage(LocalizationLanguage language) {
            _currentLanguage = language;
            _currentDictionary.Clear();

            string[] lines = _csvFile.text.Split('\n');
            int indexLanguage = DefineIndexLanguage(lines[0], _currentLanguage);
            _currentDictionary = GetDictionary(lines, indexLanguage);
        }

        private async Task SetLanguageAsync(LocalizationLanguage language) {
            _currentLanguage = language;
            _currentDictionary.Clear();

            string[] lines = _csvFile.text.Split('\n');
            int indexLanguage = DefineIndexLanguage(lines[0], _currentLanguage);
            await Task.Run(() => _currentDictionary = GetDictionary(lines, indexLanguage));
        } 

        private int DefineIndexLanguage(string line, LocalizationLanguage language) {

            line = RemoveSpecialSymbol(line);
            string[] idLanguage = line.Split(',');

            for (int i = 0; i < idLanguage.Length; i++) {
                if (idLanguage[i] == language.ToString()) {
                    return i;
                }
            }

            return 0;
        }

        private string RemoveSpecialSymbol(string line) {
            if(line.Contains("\r")) {
                return line.Replace("\r", string.Empty);
            }

            return line;
        }

        private Dictionary<string, string> GetDictionary(string[] allDictionaries, int indexLanguage) {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            foreach (string line in allDictionaries) {

                if (line.Length == 0) continue;

                string ourLine = RemoveSpecialSymbol(line);
                bool specialWorld = false;
                int countSpecialSymbol = 1;
                List<string> worlds = new List<string>();

                while(ourLine.Length > 0) {

                    string finalWorld = "";
                    if (ourLine[0] == '"') specialWorld = true;

                    if (specialWorld == false) {

                        while (ourLine.Length > 0 && ourLine[0] != ',') {
                            finalWorld += ourLine[0];
                            ourLine = ourLine.Remove(0, 1);
                        }
                    }

                    if (specialWorld == true) {

                        ourLine = ourLine.Remove(0, 1);

                        for (int i = 0; i < ourLine.Length; i++) {
                            if (ourLine[i] == '"') {

                                if (i + 1 >= ourLine.Length) break;
                                if (ourLine[i + 1] == ',') break;

                                if (ourLine[i + 1] == '"') {
                                    finalWorld += "\"";
                                    i++;
                                    countSpecialSymbol++;
                                    continue;
                                }
                            }

                            finalWorld += ourLine[i];
                        }

                        ourLine = ourLine.Remove(0, finalWorld.Length + countSpecialSymbol);

                    }

                    if (ourLine.Length > 0 && ourLine[0] == ',') ourLine = ourLine.Remove(0, 1);

                    worlds.Add(finalWorld);
                    specialWorld = false;
                    countSpecialSymbol = 1;
                }

                dictionary.Add(worlds[0], worlds[indexLanguage]);
            }

            return dictionary;
        }


        
        //API
        public string Get(string key) {

            string ourKey = key.ToLower();

            if (_currentDictionary.TryGetValue(ourKey, out var value))
                return value;

            Debug.Log($"Can't find translate with key {key}!");

#if UNITY_EDITOR
            if (key.IsNullOrEmpty()) {
                Debug.LogError($"Localization find EMPTY key!");

            } else {

                if (_missingKeys.Add(ourKey)) {
                    File.AppendAllText(NAME_MISSING_KEY, ourKey + "\n");
                    Debug.LogWarning($"MissingKeys add new key: {key}");
                }
            }
#endif
            return key;
        }
    }
}
