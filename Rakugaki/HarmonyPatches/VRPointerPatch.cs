﻿using VRUIControls;
using HarmonyLib;

namespace Rakugaki.HarmonyPatches
{
    [HarmonyPatch(typeof(VRPointer), nameof(VRPointer.OnEnable))]
    internal class VRPointerPatch
    {
        public static VRPointer Instance { get; private set; }
        static void Postfix(VRPointer __instance)
        {
            Logger.log.Notice("VRPointer Found");
            Instance = __instance;
        }
    }
}
