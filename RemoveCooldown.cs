using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace Fast_Feedbacker
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class RemoveCooldown : BaseUnityPlugin
    {
        internal static new ManualLogSource Logger { get; private set; } = null!;

        private void Awake()
        {
            // Plugin startup logic
            Logger = base.Logger;
            Logger.LogInfo($"Plugin Fast Feedbacker is loaded!");
            gameObject.hideFlags = HideFlags.DontSaveInEditor;
            Harmony.CreateAndPatchAll(typeof(RemoveCooldown));
        }

        [HarmonyPostfix][HarmonyPatch(typeof(Punch), "Start")]
        public static void Postfix(Punch __instance)
        {
            if (__instance.type == FistType.Standard)
                AccessTools.Field(typeof(Punch), "cooldownCost").SetValue(__instance, 0f);
        }
    }
}
