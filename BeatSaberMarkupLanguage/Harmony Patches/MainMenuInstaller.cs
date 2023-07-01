﻿using BeatSaberMarkupLanguage.Animations;
using BeatSaberMarkupLanguage.Settings;
using HarmonyLib;
using Zenject;

namespace BeatSaberMarkupLanguage.Harmony_Patches
{
    [HarmonyPatch(typeof(MainSettingsMenuViewControllersInstaller), nameof(MainSettingsMenuViewControllersInstaller.InstallBindings))]
    internal static class MainSettingsMenuViewControllersInstaller_InstallBindings
    {
        private static void Prefix(MainSettingsMenuViewControllersInstaller __instance)
        {
            DiContainer container = __instance.Container;

            BeatSaberUI.DiContainer = container;
            BeatSaberUI.MainTextFont.boldSpacing = 2.2f; // default bold spacing is rather  w i d e

            // Eventually this should go in an installer & not use static instances but for now this is good enough. This is kind of janky since the
            // instance persists across restarts (like PersistentSingleton did) so Initialize/Dispose can be called multiple times on the same instance.
            container.Bind(typeof(AnimationController), typeof(ITickable)).FromInstance(AnimationController.instance);
            container.Bind(typeof(BSMLSettings), typeof(IInitializable)).FromInstance(BSMLSettings.instance);
            container.Bind(typeof(BSMLParser), typeof(IInitializable)).FromInstance(BSMLParser.instance);
            container.Bind(typeof(MenuButtons.MenuButtons), typeof(IInitializable)).FromInstance(MenuButtons.MenuButtons.instance);
            container.Bind(typeof(GameplaySetup.GameplaySetup), typeof(IInitializable)).FromInstance(GameplaySetup.GameplaySetup.instance);

            // initialize all our stuff late
            container.BindInitializableExecutionOrder<BSMLSettings>(1000);
            container.BindInitializableExecutionOrder<BSMLParser>(1000);
            container.BindInitializableExecutionOrder<MenuButtons.MenuButtons>(1000);
            container.BindInitializableExecutionOrder<GameplaySetup.GameplaySetup>(1000);

#if DEBUG
            container.Bind(typeof(IInitializable)).To<Util.TestPresenter>().AsSingle();
#endif
        }
    }
}