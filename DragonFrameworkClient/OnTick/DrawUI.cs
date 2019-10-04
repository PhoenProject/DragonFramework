using System;
using System.Collections.Generic;
using MenuAPI;
using CitizenFX.Core;
using CitizenFX.Core.UI;
using static CitizenFX.Core.UI.Screen;
using static CitizenFX.Core.Native.API;
using System.Threading.Tasks;

namespace DragonFrameworkClient
{
    class DrawUI : BaseScript
    {
        public DrawUI()
        {
            Tick += tDrawTextUI;
        }
        private async Task tDrawTextUI() { DrawTextUI(); }

        private void DrawTextUI()
        {
            if (Game.PlayerPed.IsDead || Game.PlayerPed.Position.Z < -1) { HideHudAndRadarThisFrame(); return; }
            if (!Hud.IsVisible || !IsScreenFadedIn() || IsPauseMenuActive() || IsFrontendFading() || IsPauseMenuRestarting() || IsHudHidden() || IsPlayerSwitchInProgress()) return;

            HideHudComponentThisFrame((int)HudComponent.StreetName);

            DrawText($"{Direction(Game.PlayerPed.Heading)} | {World.GetStreetName(Game.PlayerPed.Position)}",
                        0.208f + (1 / GetSafeZoneSize() / 3.1f) - 0.377f, GetSafeZoneSize() - GetTextScaleHeight(0.3f, 1), 0.8f, 0.5f, (int)Alignment.Left, 4, 255, 255, 255, 255);

            if (Game.PlayerPed.IsInVehicle()) DrawText($"{Math.Round(GetEntitySpeed(Game.PlayerPed.Handle) * 2.23694f)} MPH",
                 0.208f + (1 / GetSafeZoneSize() / 3.1f) - 0.377f, GetSafeZoneSize() - GetTextScaleHeight(0.8f, 1), 0.8f, 0.5f, (int)Alignment.Left, 4, 255, 255, 255, 255);

            if (Game.PlayerPed.IsInVehicle()) DrawText($"${Game.PlayerPed.Money}", 0.208f + (1 / GetSafeZoneSize() / 3.1f) - 0.377f, GetSafeZoneSize() - GetTextScaleHeight(1.3f, 1), 0.8f, 0.5f, (int)Alignment.Left, 4, 150, 255, 150, 255);
            else DrawText($"${Game.PlayerPed.Money}", 0.208f + (1 / GetSafeZoneSize() / 3.1f) - 0.377f, GetSafeZoneSize() - GetTextScaleHeight(0.8f, 1), 0.8f, 0.5f, (int)Alignment.Left, 4, 150, 255, 150, 255);
        }

        private void DrawText(string Text, float xPos, float yPos, float Scale, float size, int alignment, int font, int r, int g, int b, int alpha)
        {
            SetTextFont(font);
            SetTextScale(Scale, size);
            SetTextJustification(alignment);
            SetTextOutline();
            BeginTextCommandDisplayText("STRING");
            AddTextComponentSubstringPlayerName(Text);
            SetTextColour(r, g, b, alpha);
            EndTextCommandDisplayText(xPos, yPos);
        }
        private string Direction(float heading)
        {
            if (heading >= 22.5 && heading < 67.5) return "NE";
            else if (heading >= 67.5 && heading < 112.5) return "E";
            else if (heading >= 112.5 && heading < 157.5) return "SE";
            else if (heading >= 157.5 && heading < 202.5) return "S";
            else if (heading >= 202.5 && heading < 247.5) return "SW";
            else if (heading >= 247.5 && heading < 292.5) return "W";
            else if (heading >= 292.5 && heading < 337.5) return "NW";
            else if (heading >= 337.5 || heading < 22.5) return "N";

            else return "Oh no, how'd on earth did i get here? Do you know? Guess not...\nYou should probably go and tell someone that i'm here, perhaps they know";
        }
    }
}
