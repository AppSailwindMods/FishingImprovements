using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using System.Reflection;

namespace FishingImprovements
{
    [BepInPlugin(GUID, NAME, VERSION)]
    public class FishingImprovementsMain : BaseUnityPlugin
    {
        public const string GUID = "com.app24.fishingimprovements";
        public const string NAME = "Fishing Improvements";
        public const string VERSION = "1.0.0";

        internal static ManualLogSource logSource;

        internal ConfigEntry<float> pickUpDistance;
        internal ConfigEntry<float> scrollSensitivity;

        internal ConfigEntry<bool> destroyHook;
        internal ConfigEntry<bool> fishCanEscape;
        internal ConfigEntry<bool> instantFishCatch;
        internal ConfigEntry<bool> instantScroll;

        internal static FishingImprovementsMain instance;

        private void Awake()
        {
            instance = this;
            logSource = Logger;
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), GUID);

            pickUpDistance = Config.Bind("Values", "Pickup Distance", 0.1f);
            scrollSensitivity = Config.Bind("Values", "Scroll Sensitivity", 0.75f);

            destroyHook = Config.Bind("Values", "Hook Can Destroy", true);
            fishCanEscape = Config.Bind("Values", "Fish Can Escape", true);
            instantFishCatch = Config.Bind("Values", "Instant Fish Catch", false);
            instantScroll = Config.Bind("Values", "No Scroll Cooldown", false);
        }
    }
}
