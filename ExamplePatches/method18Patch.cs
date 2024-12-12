using Bsg.GameSettings;
using EFT.UI.Settings;
using EFT.UI;
using HarmonyLib;
using SPT.Reflection.Patching;
using SPT.Reflection.Utils;
using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace FovFix.ExamplePatches
{
    internal class method18Patch : ModulePatch // all patches must inherit ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            // one way methods can be patched is by targeting both their class name and the name of the method itself
            // the example in this patch is the Jump() method in the Player class
            return AccessTools.Method(typeof(CameraClass), nameof(CameraClass.method_18));
        }

        [PatchPrefix]
        public static bool Prefix()
        {

            return true;
        }
        [PatchPostfix]
        private static void PatchPostfix(int x,CameraClass __instance)
        {
            __instance.method_4(x, Plugin.MinFov.Value, Plugin.MaxFov.Value);
        }
        // uncomment the 'new SimplePatch().Enable();' line in your Plugin.cs script to enable this patch.
    }
}
