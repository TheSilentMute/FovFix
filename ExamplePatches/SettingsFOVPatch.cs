using SPT.Reflection.Patching;
using SPT.Reflection.Utils;
using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace FovFix.ExamplePatches
{
    internal class SettingsFOVPatch : ModulePatch // all patches must inherit ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            // one way methods can be patched is by targeting both their class name and the name of the method itself
            // the example in this patch is the Jump() method in the Player class
            return typeof(GClass1040.Class1690).GetMethod("method_0", BindingFlags.Instance | BindingFlags.Public);
        }

        [PatchPostfix]
        public static void Postfix(int x, ref int __result)
        {
            __result = Mathf.Clamp(x, Plugin.MinFov.Value, Plugin.MaxFov.Value);
        }

        // uncomment the 'new SimplePatch().Enable();' line in your Plugin.cs script to enable this patch.
    }
}
