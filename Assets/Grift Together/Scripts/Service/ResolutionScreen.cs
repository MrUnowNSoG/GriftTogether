using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace GriftTogether {

    public class ResolutionScreen {

        public const string NATIVE = "NATIVE";
        public const string SD = "640x480";
        public const string SXGA = "1280x1024";
        public const string HD = "1280x720";
        public const string WXGA = "1366x768";
        public const string WXGA_PLUS = "1440x900";
        public const string HD_PLUS = "1600x900";
        public const string FULL_HD = "1920x1080";
        public const string QHD = "2560x1440";

        public Dictionary<string, Resolution> GameResolution { get; private set; }

        public ResolutionScreen() {

            GameResolution = new Dictionary<string, Resolution>();

            int currentRefresh = Screen.currentResolution.refreshRate;

            GameResolution.Add(SD, new Resolution { width = 640, height = 480, refreshRate = currentRefresh });
            GameResolution.Add(SXGA, new Resolution { width = 1280, height = 1024, refreshRate = currentRefresh });
            GameResolution.Add(HD, new Resolution { width = 1280, height = 720, refreshRate = currentRefresh });
            GameResolution.Add(WXGA, new Resolution { width = 1366, height = 768, refreshRate = currentRefresh });
            GameResolution.Add(WXGA_PLUS, new Resolution { width = 1440, height = 900, refreshRate = currentRefresh });
            GameResolution.Add(HD_PLUS, new Resolution { width = 1600, height = 900, refreshRate = currentRefresh });
            GameResolution.Add(FULL_HD, new Resolution { width = 1920, height = 1080, refreshRate = currentRefresh });
            GameResolution.Add(QHD, new Resolution { width = 2560, height = 1440, refreshRate = currentRefresh });
        }
    }
}
