using BepInEx;
using BepInEx.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FovFix.ExamplePatches;
using BepInEx.Configuration;
using Comfort.Common;
using EFT;
using UnityEngine;

namespace FovFix
{
    // first string below is your plugin's GUID, it MUST be unique to any other mod. Read more about it in BepInEx docs. Be sure to update it if you copy this project.
    [BepInPlugin("FovFix.UniqueGUID", "FovFix", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        internal static ConfigEntry<int> MinFov;
        internal static ConfigEntry<int> MaxFov;
        internal static ConfigEntry<float> HudFov;
        internal static ConfigEntry<float> OpticsFov;
        internal static ConfigEntry<float> AimDelta;


        public static ManualLogSource LogSource;

        // BaseUnityPlugin inherits MonoBehaviour, so you can use base unity functions like Awake() and Update()
        private void Awake()
        {
            // save the Logger to variable so we can use it elsewhere in the project
            LogSource = Logger;
            LogSource.LogInfo("plugin loaded!");


            // uncomment line(s) below to enable desired example patch, then press F6 to build the project:
            new SimplePatch().Enable();
            new ShowFOVPatch().Enable();
            new SettingsFOVPatch().Enable();
            new method18Patch().Enable();
            new HeadBobPatch().Enable();
            new AimFOVPatch().Enable();

            MinFov = Config.Bind(
                "Main Section",
                "Min FOV Value",
                50,
                new ConfigDescription("Your desired minimum FOV value. Default is 50",
                new AcceptableValueRange<int>(1, 149)));

            MaxFov = Config.Bind(
                "Main Section",
                "Max FOV Value",
                150,
                new ConfigDescription("Your desired maximum FOV value. Default is 150",
                new AcceptableValueRange<int>(1, 150)));

            HudFov = Config.Bind(
                "Main Section",
                "HUD FOV value",
                0.05f,
                new ConfigDescription("Pseudo-value for HUD FOV, will change camera position relative to your body. The lower the value, the further away from your body.",
                new AcceptableValueRange<float>(-0.2f, 0.2f)));
            OpticsFov = Config.Bind(
                "Main Section",
                "Optics FOV",
                35f,
                new ConfigDescription("FOV when using optics",
                new AcceptableValueRange<float>(1f, 150)));
            AimDelta = Config.Bind(
                "Main Section",
                "Aim Delta",
                15f,
                new ConfigDescription("Value subtracted from FOV when aiming without optics",
                new AcceptableValueRange<float>(0f,150f)));

            HudFov.SettingChanged += HudFov_SettingChanged;
        }

        private void HudFov_SettingChanged(object sender, EventArgs e)
        {
            var gameWorld = Singleton<GameWorld>.Instance;

            if (gameWorld == null || gameWorld.RegisteredPlayers == null)
            {
                return;
            }
            gameWorld.MainPlayer.ProceduralWeaponAnimation.HandsContainer.CameraOffset = new UnityEngine.Vector3(0.04f, 0.04f, HudFov.Value);
        }

    }
}
