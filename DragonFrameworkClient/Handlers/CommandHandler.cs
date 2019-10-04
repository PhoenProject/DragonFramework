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

namespace DragonFrameworkClient
{
    class CommandHandler : BaseScript
    {
        public void RegisterCommands()
        {
            RegisterCommand("me", new Action<int, List<object>, string>((source, args, raw) =>
            {
                string sentence = "";
                if (args.Count == 0) return;

                foreach (string part in args)
                {
                    sentence += " " + part;
                }

                TriggerEvent("chat:addMessage", new
                {
                    color = new[] { 210, 161, 247 },
                    args = new[] { $"*{Game.Player.Name}{sentence}" }
                });
            }), false);
            RegisterCommand("ooc", new Action<int, List<object>, string>((source, args, raw) =>
            {

                string sentence = "";
                if (args.Count == 0) return;

                foreach (string part in args)
                {
                    sentence += " " + part;
                }

                TriggerEvent("chat:addMessage", new
                {
                    color = new[] { 139, 252, 218 },
                    args = new[] { $"[OOC] {Game.Player.Name}{sentence}" }
                });
            }), false);

            RegisterCommand("car", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                // account for the argument not being passed
                var model = "adder";
                if (args.Count > 0)
                {
                    model = args[0].ToString();
                }

                // check if the model actually exists
                // assumes the directive `using static CitizenFX.Core.Native.API;`
                var hash = (uint)GetHashKey(model);
                if (!IsModelInCdimage(hash) || !IsModelAVehicle(hash))
                {
                    TriggerEvent("chat:addMessage", new
                    {
                        color = new[] { 255, 0, 0 },
                        args = new[] { "[CarSpawner]", $"It might have been a good thing that you tried to spawn a {model}. Who even wants their spawning to actually ^*succeed?" }
                    });
                    return;
                }

                // create the vehicle
                var vehicle = await World.CreateVehicle(model, Game.PlayerPed.Position, Game.PlayerPed.Heading);

                // set the player ped into the vehicle and driver seat
                Game.PlayerPed.SetIntoVehicle(vehicle, VehicleSeat.Driver);

                // tell the player
                TriggerEvent("chat:addMessage", new
                {
                    color = new[] { 255, 0, 0 },
                    args = new[] { "[CarSpawner]", $"Woohoo! Enjoy your new ^*{model}!" }
                });
            }), false);

            RegisterCommand("money", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                Debug.WriteLine(Game.PlayerPed.Money.ToString());
                Game.PlayerPed.Money += 50000;
                Debug.WriteLine(Game.PlayerPed.Money.ToString());
            }), false);
        }
    }
}
