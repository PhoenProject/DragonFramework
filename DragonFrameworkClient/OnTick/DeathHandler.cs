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

namespace DragonFrameworkClient
{
    class DeathHandler : BaseScript
    {
        Random random = new Random();
        int Money;
        int Bill;
        bool isDead;
        public DeathHandler()
        {
            Tick += tDeathHandler;
        }
        async Task tDeathHandler() => CheckIsDead();

        private void CheckIsDead()
        {
            if (Game.PlayerPed.IsDead && !isDead) { HandleDeath(); }
            else if (Game.PlayerPed.IsAlive && isDead) { isDead = false; }

        }
        private async void HandleDeath()
        {
            isDead = true;

            Money = Game.PlayerPed.Money;
            Game.PlayerPed.Money = 0;

            await Delay(2500);
            DoScreenFadeOut(500);
            await Delay(500);
            Game.PlayerPed.Position = new Vector3(random.Next(-460, -450), random.Next(-352, -327), 33);

            Bill = random.Next(500, 850);

            if (Money > Bill)
            {
                BeginTextCommandDisplayHelp("THREESTRINGS");
                AddTextComponentSubstringPlayerName($"${Bill} has been deducted from your account to cover your medical costs");
                Game.PlayerPed.Money = Money - Bill;
            }
            else
            {
                BeginTextCommandDisplayHelp("THREESTRINGS");
                AddTextComponentSubstringPlayerName($"${Money} has been deducted from your account to cover your medical costs");
                Game.PlayerPed.Money = 0;
            }

            Game.PlayerPed.ClearBloodDamage();

            DoScreenFadeIn(500);

            EndTextCommandDisplayHelp(0, false, false, 6000);
        }
    }
}
