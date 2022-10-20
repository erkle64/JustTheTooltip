using BepInEx;
using UnhollowerRuntimeLib;
using HarmonyLib;
using UnityEngine;

namespace JustTheTooltip
{
    [BepInPlugin(GUID, MODNAME, VERSION)]
    public class JustTheTooltipLoader : BepInEx.IL2CPP.BasePlugin
    {
        public const string
            MODNAME = "JustTheTooltip",
            AUTHOR = "erkle64",
            GUID = "com." + AUTHOR + "." + MODNAME,
            VERSION = "1.1.0";

        public static BepInEx.Logging.ManualLogSource log;

        public JustTheTooltipLoader()
        {
            PluginComponent.log = log = Log;
        }

        public override void Load()
        {
            log.LogMessage("Registering PluginComponent in Il2Cpp");

            try
            {
                ClassInjector.RegisterTypeInIl2Cpp<PluginComponent>();

                var go = new GameObject("Erkle64_JustTheTooltip_PluginObject");
                go.AddComponent<PluginComponent>();
                Object.DontDestroyOnLoad(go);
            }
            catch
            {
                log.LogError("FAILED to Register Il2Cpp Type: PluginComponent!");
            }

            try
            {
                var harmony = new Harmony(GUID);

                var original = AccessTools.Method(typeof(TooltipFrame), "_generateItemContainer");
                var post = AccessTools.Method(typeof(PluginComponent), "TooltipFrame__generateItemContainer");
                harmony.Patch(original, postfix: new HarmonyMethod(post));
            }
            catch
            {
                log.LogError("Harmony - FAILED to Apply Patch's!");
            }
        }
    }
}