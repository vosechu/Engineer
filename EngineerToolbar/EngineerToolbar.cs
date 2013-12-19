﻿using System;
using UnityEngine;
using Engineer;
using Toolbar;

namespace EngineerToolbar
{
    [KSPAddon(KSPAddon.Startup.EditorAny | KSPAddon.Startup.Flight, false)]
    public class EngineerToolbar : MonoBehaviour
    {
        public const string VERSION = "1.0.0.0";

        private string enabledTexturePath = "Engineer/ToolbarEnabled";
        private string disabledTexturePath = "Engineer/ToolbarDisabled";
        private IButton button;

        private void Start()
        {
            this.button = ToolbarManager.Instance.add("KER", "engineerButton");
            this.button.ToolTip = "Kerbal Engineer Redux";

            if (HighLogic.LoadedSceneIsEditor)
            {
                SetButtonState(BuildEngineer.isVisible);
                this.button.OnClick += (e) =>
                {
                    TogglePluginVisibility(ref BuildEngineer.isVisible);
                };
            }
            else if (HighLogic.LoadedSceneIsFlight)
            {
                SetButtonState(FlightEngineer.isVisible);
                this.button.OnClick += (e) =>
                {
                    TogglePluginVisibility(ref FlightEngineer.isVisible);
                };
            }
        }

        private void LateUpdate()
        {
            if (HighLogic.LoadedSceneIsEditor)
            {
                SetButtonVisibility(BuildEngineer.isActive);
                BuildEngineer.isActive = false;
            }
            else if (HighLogic.LoadedSceneIsFlight)
            {
                SetButtonVisibility(FlightEngineer.isActive);
                FlightEngineer.isActive = false;
            }
        }

        private void OnDestroy()
        {
            button.Destroy();
        }

        private void TogglePluginVisibility(ref bool toggle)
        {
            if (toggle)
            {
                toggle = false;
            }
            else
            {
                toggle = true;
            }
            SetButtonState(toggle);
        }

        private void SetButtonVisibility(bool visible)
        {
            if (this.button.Visible != visible)
            {
                this.button.Visible = visible;
            }
        }

        private void SetButtonState(bool state)
        {
            if (state)
            {
                this.button.TexturePath = this.enabledTexturePath;
            }
            else
            {
                this.button.TexturePath = this.disabledTexturePath;
            }
        }
    }
}
