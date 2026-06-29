using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using BBI.Unity.Game;
using Carbon.Localization.Core;

namespace GhostHelmetSticker;

[BepInPlugin("com.dcragusa.ghosthelmetsticker", "Ghost Helmet Sticker", "1.0.0")]
public class Plugin : BaseUnityPlugin
{
    internal static new ManualLogSource Logger;

    private void Awake()
    {
        Logger = base.Logger;
        Logger.LogInfo("Ghost Helmet Sticker by dcragusa - https://github.com/dcragusa/GhostHelmetSticker");
        var harmony = new Harmony("com.dcragusa.ghosthelmetsticker");
        harmony.PatchAll();
        Logger.LogInfo("Plugin loaded!");
    }
}

[HarmonyPatch(typeof(StickerAsset), "AmountRequiredForSticker", MethodType.Getter)]
public class PatchStickerAmountGetter
{
    public static void Postfix(StickerAsset __instance, ref double __result)
    {
        if (__instance.name == "GhostShip_Helmet_StickerAsset")
        {
            __result = 2;
        }
    }
}

[HarmonyPatch(typeof(SingleLanguageCacheLocalizationDatabase), "TryFindEntry", [typeof(ulong), typeof(string)], [ArgumentType.Normal, ArgumentType.Ref])]
public class DatabaseLookupPatch
{
    public static void Postfix(bool __result, ulong id, ref string entry)
    {
        if (__result && id == 9692 && entry != null)
        {
            entry = entry.Replace("3", "2");
        }
    }
}
