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
using System.Dynamic;

namespace DragonFrameworkClient
{
    class ClientResourceStart : BaseScript
    {
        GenerateClothes GenerateClothes = new GenerateClothes();
        CommandHandler CommandHandler = new CommandHandler();

        internal void OnClientResourceStart(string resourceName)
        {
            if (GetCurrentResourceName() != resourceName) return;

            GenerateClothes.GenerateEverything();
            CommandHandler.RegisterCommands();

            TriggerServerEvent("dFrame:GetPlayerData");
        }
    }
}
