using SPT.Reflection.Patching;
using EFT.UI.Settings;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using UnityEngine;
using EFT.UI;

namespace FovFix.ExamplePatches
{
    internal class ShowFOVPatch : ModulePatch { 
    protected override MethodBase GetTargetMethod()
    {
            // one way methods can be patched is by targeting both their class name and the name of the method itself
            // the example in this patch is the Jump() method in the Player class
            return typeof(GameSettingsTab).GetMethod("Show");
        }


    [PatchPrefix]
        private bool PatchPrefix()
        {
        // code in Prefix() method will run BEFORE original code is executed.
        // if 'true' is returned, the original code will still run.
        // if 'false' is returned, the original code will be skipped.
       if (Plugin.MaxFov.Value < Plugin.MinFov.Value)
            {
                Plugin.MinFov.Value = 50;
                Plugin.MaxFov.Value = 150;
            }
            return true;
            
    }

        [PatchPostfix]
        private static void PatchPostfix(ref NumberSlider ____fov, ref GClass1040 ___gclass1040_0)
        {
            SettingsTab.BindNumberSliderToSetting(____fov, ___gclass1040_0.FieldOfView, Plugin.MinFov.Value, Plugin.MaxFov.Value);
        }

        // uncomment the 'new SimplePatch().Enable();' line in your Plugin.cs script to enable this patch.
    }
}