using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MenuAPI;
using CitizenFX.Core;
using CitizenFX.Core.UI;
using static CitizenFX.Core.UI.Screen;
using static CitizenFX.Core.Native.API;
using System.Drawing;
using Newtonsoft.Json;

namespace DragonFrameworkClient.OnTick
{
    class SavePlayer : BaseScript
    {
        DateTime LastSave = DateTime.Now;
        Data PlayerData = new Data();
        public class Data
        {
            public int Money { get; set; }
            public float Xpos { get; set; }
            public float Ypos { get; set; }
            public float Zpos { get; set; }
            public float Heading { get; set; }
        }

        public SavePlayer()
        {
            Tick += tSavePlayerCheck;
        }
        async Task tSavePlayerCheck() => SavePlayerCheck();

        private async void SavePlayerCheck()
        {
            if (Game.IsLoading || Game.PlayerPed.Position.Z < 0 || (DateTime.Now - LastSave).TotalMinutes < 2) return;

            ShowLoadingPrompt((int)LoadingSpinnerType.SocialClubSaving);

            LastSave = DateTime.Now;

            PlayerData.Money = Game.PlayerPed.Money;
            PlayerData.Xpos = Game.PlayerPed.Position.X;
            PlayerData.Ypos = Game.PlayerPed.Position.Y;
            PlayerData.Zpos = Game.PlayerPed.Position.Z;
            PlayerData.Heading = Game.PlayerPed.Heading;

            string Json = JsonConvert.SerializeObject(PlayerData);

            TriggerServerEvent("dFrame:SavePlayer", Json);

            await Delay(1000);

            RemoveLoadingPrompt();
        }
    }
}
