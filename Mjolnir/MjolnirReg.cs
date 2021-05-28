using UnityEngine;
using BepInEx;
using BepInEx.Logging;
using ValheimLib.ODB;
using BepInEx.Configuration;
using System;

namespace Mjolnir
{
    [BepInPlugin(ModGuid, ModName, ModVer)]
    [BepInProcess("valheim.exe")]
    [BepInDependency("ValheimModdingTeam.ValheimLib", BepInDependency.DependencyFlags.HardDependency)]

    public class Mjolnir : BaseUnityPlugin
    {
        public const string ModGuid = ModName;
        public static ConfigEntry<int> CrystalRequired; //10
        public static ConfigEntry<int> IronRequired; //30
        public static ConfigEntry<int> HardAntler; //20
        public static ConfigEntry<int> SerpentScale; //20
        public static ConfigEntry<string> lightningWarpKey;
        public static KeyCode warpKey;
        private static ConfigEntry<int> nexusID;

        public const string AuthorName = "mugshot";
        public const string ModName = "Mjolnir";
        public const string ModVer = "1.0.1";
        internal static Mjolnir Instance { get; private set; }
        private void Awake()
        {
            Instance = this;
            LanguageData.Init();
            InitConfigData();
            ObjectDBHelper.OnBeforeCustomItemsAdded += InitStatusEffects;
            ObjectDBHelper.OnBeforeCustomItemsAdded += ItemData.Init;
            ObjectDBHelper.OnAfterInit += Patch.Init;
        }

        private void OnDestroy()
        {
            Patch.Disable();
        }
        private void InitConfigData()
        {
            nexusID = Config.Bind<int>("General", "NexusID", 913, "Nexus mod ID for updates");
            IronRequired = Config.Bind("Crafting", "IronRequired", 30, "Iron required");
            SerpentScale = Config.Bind("Crafting", "SerpentScale", 10, "Serpent scale requirement");
            HardAntler = Config.Bind("Crafting", "HardAntler", 20, "Hard antler required");
            CrystalRequired = Config.Bind("Crafting", "CrystalRequired", 10, "Crystal required");
            lightningWarpKey = Config.Bind("Keybinds", "lightningWarpKey", "Z", "Keybind for lightning warp (requires restart to take effect)");

            if (!Enum.TryParse<KeyCode>(lightningWarpKey.Value, out var warpkeycode))
            {
                Debug.Log("Failed to get key, please enter a valid bind that is not empty.");
                return;
            }
            warpKey = warpkeycode;
        }

        private void InitStatusEffects()
        {
            Debug.Log("Initializing status effects");
            var effect = ScriptableObject.CreateInstance<SE_Shocked>();
            var effect2 = ScriptableObject.CreateInstance<SE_Electric>();
            effect.m_name = LanguageData.EffectValue;
            effect.name = LanguageData.EffectValue;
            effect.m_tooltip = LanguageData.MjolnirTooltipName;

            effect2.m_name = LanguageData.EffectValue2;
            effect2.name = LanguageData.EffectValue2;
            effect2.m_tooltip = LanguageData.MjolnirTooltipName;
            ObjectDBHelper.Add(new CustomStatusEffect(effect, true));
            ObjectDBHelper.Add(new CustomStatusEffect(effect2, true));
        }
    }
}
