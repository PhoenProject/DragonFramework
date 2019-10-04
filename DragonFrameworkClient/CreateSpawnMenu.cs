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
    class MainMenu : BaseScript
    {
        CharacterData CharData = new CharacterData();
        public class CharacterData { public List<string[]> characters { get; set; } }
        public async void GenerateMainMenu(ExpandoObject Json)
        {
            while (IsNetworkLoadingScene()) await Delay(1);

            Player player = Game.Player;

            SetEntityCoords(player.Character.Handle, 2.3f, -667.8f, 15, true, false, false, true);
            player.Character.Heading = 180f;

            FreezeEntityPosition(player.Handle, true);
            SetEntityCollision(player.Handle, false, false);
            SetEntityInvincible(player.Handle, true);
            Game.Player.CanControlCharacter = false;

            int cam = CreateCam("DEFAULT_SCRIPTED_CAMERA", true);
            Camera MainCamera = new Camera(cam);
            MainCamera.Position = new Vector3(-306.7f, 202.5f, 200);
            MainCamera.PointAt(new Vector3(305.4f, -1816.3f, 110));

            Game.PlayerPed.Task.ClearAllImmediately();

            RenderScriptCams(true, false, 0, true, true);

            Menu CharacterMenu = new Menu("Character menu", "Select or create a character");

            Debug.WriteLine(Json.ToString());

            MenuItem NewCharacter = new MenuItem($"New character", "Create a new character");
            CharacterMenu.AddMenuItem(NewCharacter);

            MenuController.AddMenu(CharacterMenu);

            await Delay(800);

            CharacterMenu.OpenMenu();

            DoScreenFadeIn(500);
        }
    }
}
