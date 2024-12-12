using EFT.Animations;
using HarmonyLib;
using SPT.Reflection.Patching;
using SPT.Reflection.Utils;
using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace FovFix.ExamplePatches
{
    internal class AimFOVPatch : ModulePatch // all patches must inherit ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            // one way methods can be patched is by targeting both their class name and the name of the method itself
            // the example in this patch is the Jump() method in the Player class
            return AccessTools.Method(typeof(ProceduralWeaponAnimation), nameof(ProceduralWeaponAnimation.method_23));
        }

        [PatchPrefix]
        public static bool Prefix(ProceduralWeaponAnimation __instance, GInterface162 ____firearmAnimationData, bool ____isAiming,bool forced = false)
        {
            __instance.UpdateTacticalReload();
            __instance.method_17();
            if (__instance.FirstPersonPointOfView)
            {
                if (!__instance.Sprint && __instance.AimIndex < __instance.ScopeAimTransforms.Count)
                {
                    float num = (__instance.IsAiming ? (__instance.CurrentScope.IsOptic ? Plugin.OpticsFov.Value : (__instance.Single_2 - Plugin.AimDelta.Value)) : __instance.Single_2);
                    if (____firearmAnimationData != null && !____firearmAnimationData.MouseLookControl)
                    {
                        CameraClass.Instance.SetFov(num, 1f, !____isAiming);
                    }
                }
                __instance.method_1(); 
            }
            __instance.Shootingg.Pose = __instance.Pose;
            __instance.Shootingg.CurrentRecoilEffect.HandRotationRecoilEffect.Intensity = __instance.IntensityByAiming;
            __instance.Breath.Intensity = __instance.IntensityByPoseLevel * __instance.IntensityByAiming;
            __instance.UpdateSwayFactors();
            __instance.Breath.IsAiming = __instance.IsAiming;
            __instance.HandShakeEffector.IsAiming = __instance.IsAiming;
            __instance.HandsContainer.HandsRotation.InputIntensity = (__instance.HandsContainer.HandsPosition.InputIntensity = __instance.IntensityByAiming * __instance.IntensityByAiming);
            __instance.TurnAway.IsInPronePose = __instance.Pose == 0;
            __instance.Walk.AdjustPose();

            return false;
        }
        [PatchPostfix]
        private static void PatchPostfix()
        {
            
        }
        // uncomment the 'new SimplePatch().Enable();' line in your Plugin.cs script to enable this patch.
    }
}
