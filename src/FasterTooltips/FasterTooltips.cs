using System;
using System.Reflection;
using BattleTech.UI.Tooltips;
using Newtonsoft.Json;
using Harmony;

namespace FasterTooltips
{
    public class ModSettings
    {
        public float TooltipDelay = 0.33f;
    }

    [HarmonyPatch(typeof(TooltipManager), "SpawnTooltip")]
    public static class TooltipManager_SpawnTooltip_Patch
    {
        static void Prefix(TooltipManager __instance, ref float DelayOverride)
        {
            DelayOverride = Mod.settings.TooltipDelay;
        }
    }

    public static class Mod
    {
        public static ModSettings settings;

        public static void Init(string directory, string settingsJSON)
        {
            try
            {
                settings = JsonConvert.DeserializeObject<ModSettings>(settingsJSON);
            }
            catch (Exception)
            {
                settings = new ModSettings();
            }

            var harmony = HarmonyInstance.Create("com.github.kgoransson.Battletech_FasterTooltips");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}
