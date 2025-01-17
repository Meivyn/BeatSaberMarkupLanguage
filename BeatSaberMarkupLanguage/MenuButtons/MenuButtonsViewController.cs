﻿using System.Collections.Generic;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Harmony_Patches;
using BeatSaberMarkupLanguage.ViewControllers;

namespace BeatSaberMarkupLanguage.MenuButtons
{
    /// <summary>
    /// This is the view controller presented to the left of the main menu.
    /// </summary>
    /// <remarks>
    /// This view is presented by <see cref="MainFlowCoordinator"/> through the <see cref="MainFlowCoordinator_DidActivate"/> and <see cref="MainFlowCoordinator_TopViewControllerWillChange"/> patches.
    /// </remarks>
    [ViewDefinition("BeatSaberMarkupLanguage.Views.main-left-screen.bsml")]
    internal class MenuButtonsViewController : BSMLAutomaticViewController
    {
        [UIValue("buttons")]
        public List<MenuButton> buttons => MenuButtons.instance.buttons;

        [UIValue("any-buttons")]
        private bool AnyButtons => buttons.Count > 0;

        public void RefreshView()
        {
            if (!isActivated)
            {
                return;
            }

            __Deactivate(false, false, false);
            ClearContents();
            _wasActivatedBefore = false;
            __Activate(false, false);
        }
    }
}
