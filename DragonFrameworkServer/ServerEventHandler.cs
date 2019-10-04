using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using Newtonsoft.Json;
using System.Dynamic;

namespace DragonFrameworkServer
{
    public class ServerEvents : BaseScript
    {
        MySQL MySQL = new MySQL();
        public ServerEvents()
        {
            EventHandlers.Add("dFrame:KickPlayer", new Action<Player, int, string>(EventDropPlayer));
            EventHandlers.Add("dFrame:DisconnectPlayer", new Action<Player, int>(EventDisconnectPlayer));
            EventHandlers.Add("dFrame:GetPlayerData", new Action<Player>(GetPlayerData));
            EventHandlers.Add("dFrame:SaveNewPed", new Action<Player, string, string, string, string, string, float, float, float, float, int, string>(SaveNewPed));
            EventHandlers.Add("dFrame:SavePlayer", new Action<Player, string>(SavePlayer));

            EventHandlers["onResourceStart"] += new Action(MySQL.GetConnData);
            //EventHandlers["playerConnecting"] += new Action<Player>(OnPlayerConnecting);
        }

        public void EventDropPlayer([FromSource]Player source, int target, string reason) => DropPlayer(target.ToString(), "You have been kicked from the server!\nReason: " + reason);
        public void EventDisconnectPlayer([FromSource]Player source, int player) => DropPlayer(source.Handle, $"{source.Identifiers["license"]} + {source.Identifiers["steam"]} + {source.Identifiers["discord"]}");

        private void GetPlayerData([FromSource]Player connectingPlayer) => MySQL.GetPlayerData(connectingPlayer);

        public void SaveNewPed([FromSource]Player source, string name, string gender, string overlaydata, string facedata, string componentdata, float xpos, float ypos, float zpos, float heading, int money, string lastvehicle) =>
            MySQL.SaveNewPed(source, name, gender, overlaydata, facedata, componentdata, xpos, ypos, zpos, heading, money, lastvehicle);
        public void SavePlayer([FromSource]Player source, string json) => MySQL.SavePlayer(source, json);        
    }
}
