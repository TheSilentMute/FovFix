using SPT.Reflection.Patching;
using EFT.Animations;
using System.Reflection;
using UnityEngine;

namespace FovFix.ExamplePatches
{
    internal class SimplePatch : ModulePatch // all patches must inherit ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            // one way methods can be patched is by targeting both their class name and the name of the method itself
            // the example in this patch is the Jump() method in the Player class
            return typeof(PlayerSpring).GetMethod("Start", BindingFlags.Public | BindingFlags.Instance);
        }



        [PatchPostfix]
        private static void PatchPostfix(ref Vector3 ___CameraOffset)
        {
            ___CameraOffset = new Vector3(0.04f, 0.04f, Plugin.HudFov.Value);
        }

        // uncomment the 'new SimplePatch().Enable();' line in your Plugin.cs script to enable this patch.
    }
}
