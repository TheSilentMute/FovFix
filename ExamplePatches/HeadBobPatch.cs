using SPT.Reflection.Patching;
using SPT.Reflection.Utils;
using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace FovFix.ExamplePatches
{
    internal class HeadBobPatch : ModulePatch // all patches must inherit ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            // one way methods can be patched is by targeting both their class name and the name of the method itself
            // the example in this patch is the Jump() method in the Player class
            return typeof(GClass1040.Class1690).GetMethod("method_1", BindingFlags.Instance | BindingFlags.Public);
        }

        [PatchPostfix]
        public static void Postfix(float x, ref float __result)
        {
            __result = Mathf.Clamp(x, 0f, 1f);
        }

        // uncomment the 'new SimplePatch().Enable();' line in your Plugin.cs script to enable this patch.
    }
}
