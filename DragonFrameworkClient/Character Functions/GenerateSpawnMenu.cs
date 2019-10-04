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
using Newtonsoft.Json;

namespace DragonFrameworkClient
{
    class GenerateSpawnMenu : BaseScript
    {
        ManageInteractionMenu ManageInteractionMenu = new ManageInteractionMenu();
        CharacterCreation CharacterCreation = new CharacterCreation();
        public class CharData
        {
            public List<string[]> Character { get; set; }
        }

        public GenerateSpawnMenu()
        {
            EventHandlers["dFrame:MainMenu"] += new Action<string>(GenerateMainMenu);
        }
        public class PedOverlays
        {
            public int Blemish { get; set; }
            public float BlemishOpacity { get; set; }
            public int FacialHair { get; set; }
            public int FacialHairColour { get; set; }
            public float FacialHairOpacity { get; set; }
            public int Eyebrows { get; set; }
            public int EyebrowsColour { get; set; }
            public float EyebrowsOpacity { get; set; }
            public int Ageing { get; set; }
            public float AgeingOpacity { get; set; }
            public int Makeup { get; set; }
            public int MakeupColour { get; set; }
            public float MakeupOpacity { get; set; }
            public int Blush { get; set; }
            public int BlushColour { get; set; }
            public float BlushOpacity { get; set; }
            public int Complexion { get; set; }
            public float ComplexionOpacity { get; set; }
            public int Sundamage { get; set; }
            public float SundamageOpacity { get; set; }
            public int Lipstick { get; set; }
            public int LipstickColour { get; set; }
            public float LipstickOpacity { get; set; }
            public int MolesFreckles { get; set; }
            public float MolesFrecklesOpacity { get; set; }
            public int ChestHair { get; set; }
            public int ChestHairColour { get; set; }
            public float ChestHairOpacity { get; set; }
            public int BodyBlemish { get; set; }
            public float BodyBlemishOpacity { get; set; }
            public int EyeColour { get; set; }
        }
        public class Face
        {
            public int FatherFace { get; set; }
            public int MotherFace { get; set; }
            public int FatherSkin { get; set; }
            public int MotherSkin { get; set; }
            public float ShapePercent { get; set; }
            public float SkinPercent { get; set; }
            public float NoseWidth { get; set; }
            public float NoesPeakHeight { get; set; }
            public float NosePeakLength { get; set; }
            public float NoseBoneHeight { get; set; }
            public float NosePeakLowering { get; set; }
            public float NoseBoneTwist { get; set; }
            public float EyebrowsHeight { get; set; }
            public float EyebrowsDepth { get; set; }
            public float CheekbonesHeight { get; set; }
            public float CheekbonesWidth { get; set; }
            public float CheeksWidth { get; set; }
            public float EyesOpening { get; set; }
            public float LipsThickness { get; set; }
            public float JawBoneWidth { get; set; }
            public float JawBoneDepthLength { get; set; }
            public float ChinHeight { get; set; }
            public float ChinDepthLength { get; set; }
            public float ChinWidth { get; set; }
            public float ChinHoleSize { get; set; }
            public float NeckThickness { get; set; }
        }
        public class Components
        {
            public int Torso { get; set; }
            public int TorsoTexture { get; set; }
            public int Pants { get; set; }
            public int PantsTexture { get; set; }
            public int Shoes { get; set; }
            public int ShoesTexture { get; set; }
            public int Undershirt { get; set; }
            public int UndershirtTexture { get; set; }
            public int Shirt { get; set; }
            public int ShirtTexture { get; set; }
            public int Hair { get; set; }
            public int HairColour { get; set; }
            public int HairHighlight { get; set; }
        }

        private async void GenerateMainMenu(string Json)
        {
            CharData charData = JsonConvert.DeserializeObject<CharData>(Json);

            while (IsNetworkLoadingScene()) await Delay(1);

            await Delay(1500);

            Player player = Game.Player;

            SetEntityCoords(player.Character.Handle, 410, -999f, -100, true, false, false, true);
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

            foreach (string[] charString in charData.Character)
            {
                MenuItem CharacterItem = new MenuItem($"{charString[0]}", null) { Label = $"({charString[1]})" };
                CharacterMenu.AddMenuItem(CharacterItem);
            }

            MenuItem NewCharacter = new MenuItem($"New character", "Create a new character");
            CharacterMenu.AddMenuItem(NewCharacter);

            MenuController.AddMenu(CharacterMenu);

            await Delay(800);

            CharacterMenu.OnItemSelect += async (menu, item, index) =>
            {
                if (item == NewCharacter)
                {
                    DoScreenFadeOut(500);
                    await Delay(500);
                    CharacterCreation.CreationStart();
                }
                else
                {
                    foreach(string[] Character in charData.Character)
                    {
                        if (item.Text != Character[0]) return;

                        else SpawnCharacter(Character);
                    }
                }
            };

            CharacterMenu.OnMenuOpen += (menu) =>
            {
                MenuController.DisableBackButton = true;
                MenuController.PreventExitingMenu = true;
            };
            CharacterMenu.OnMenuClose += (menu) =>
            {
                MenuController.DisableBackButton = false;
                MenuController.PreventExitingMenu = false;
            };

            CharacterMenu.OpenMenu();

            DoScreenFadeIn(500);
        }

        private async void SpawnCharacter(string[] Character)
        {
            DoScreenFadeOut(500);

            await Delay(500);

            MenuController.CloseAllMenus();
            ManageInteractionMenu.CreateInteractionMenu();

            RenderScriptCams(false, false, 0, false, false);

            PedOverlays OverlayData = JsonConvert.DeserializeObject<PedOverlays>(Character[2]);
            Face FaceData = JsonConvert.DeserializeObject<Face>(Character[3]);
            Components ComponentData = JsonConvert.DeserializeObject<Components>(Character[4]);

            Model model;
            if (Character[1] == "Male") model = new Model("mp_m_freemode_01");
            else model = new Model("mp_f_freemode_01");

            RequestModel((uint)model.Hash);

            while (!HasModelLoaded((uint)model.Hash)) await Delay(0);

            await Game.Player.ChangeModel(model);

            int ped = Game.PlayerPed.Handle;
            SetPedHeadBlendData(ped, FaceData.FatherFace, FaceData.MotherFace, 0, FaceData.FatherSkin, FaceData.MotherSkin, 0, FaceData.ShapePercent, FaceData.SkinPercent, 0, false);

            #region FaceFeatures
            SetPedFaceFeature(ped, 0, FaceData.NoseWidth);
            SetPedFaceFeature(ped, 1, FaceData.NoesPeakHeight);
            SetPedFaceFeature(ped, 2, FaceData.NosePeakLength);
            SetPedFaceFeature(ped, 3, FaceData.NoseBoneHeight);
            SetPedFaceFeature(ped, 4, FaceData.NosePeakLowering);
            SetPedFaceFeature(ped, 5, FaceData.NoseBoneTwist);
            SetPedFaceFeature(ped, 6, FaceData.EyebrowsHeight);
            SetPedFaceFeature(ped, 7, FaceData.EyebrowsDepth);
            SetPedFaceFeature(ped, 8, FaceData.CheekbonesHeight);
            SetPedFaceFeature(ped, 9, FaceData.CheekbonesWidth);
            SetPedFaceFeature(ped, 10, FaceData.CheeksWidth);
            SetPedFaceFeature(ped, 11, FaceData.EyesOpening);
            SetPedFaceFeature(ped, 12, FaceData.LipsThickness);
            SetPedFaceFeature(ped, 13, FaceData.JawBoneWidth);
            SetPedFaceFeature(ped, 14, FaceData.JawBoneDepthLength);
            SetPedFaceFeature(ped, 15, FaceData.ChinHeight);
            SetPedFaceFeature(ped, 16, FaceData.ChinDepthLength);
            SetPedFaceFeature(ped, 17, FaceData.ChinWidth);
            SetPedFaceFeature(ped, 18, FaceData.ChinHoleSize);
            SetPedFaceFeature(ped, 19, FaceData.NeckThickness);
            #endregion
            #region OverlayData
            SetPedComponentVariation(ped, 2, ComponentData.Hair, 0, 1);
            SetPedHairColor(ped, ComponentData.HairColour, ComponentData.HairHighlight);

            SetPedHeadOverlay(ped, 0, OverlayData.Blemish, OverlayData.BlemishOpacity);

            SetPedHeadOverlay(ped, 1, OverlayData.FacialHair, OverlayData.FacialHairOpacity);
            SetPedHeadOverlayColor(ped, 1, 1, OverlayData.FacialHairColour, OverlayData.FacialHairColour);

            SetPedHeadOverlay(ped, 2, OverlayData.Eyebrows, OverlayData.EyebrowsOpacity);
            SetPedHeadOverlayColor(ped, 2, 1, OverlayData.EyebrowsColour, OverlayData.EyebrowsColour);

            SetPedHeadOverlay(ped, 3, OverlayData.Ageing, OverlayData.AgeingOpacity);

            SetPedHeadOverlay(ped, 4, OverlayData.Makeup, OverlayData.MakeupOpacity);
            SetPedHeadOverlayColor(ped, 4, 1, OverlayData.MakeupColour, OverlayData.MakeupColour);

            SetPedHeadOverlay(ped, 5, OverlayData.Blush, OverlayData.BlushOpacity);
            SetPedHeadOverlayColor(ped, 5, 1, OverlayData.BlushColour, OverlayData.BlushColour);

            SetPedHeadOverlay(ped, 6, OverlayData.Complexion, OverlayData.ComplexionOpacity);

            SetPedHeadOverlay(ped, 7, OverlayData.Sundamage, OverlayData.SundamageOpacity);

            SetPedHeadOverlay(ped, 8, OverlayData.Lipstick, OverlayData.LipstickOpacity);
            SetPedHeadOverlayColor(ped, 8, 1, OverlayData.LipstickColour, OverlayData.LipstickColour);

            SetPedHeadOverlay(ped, 9, OverlayData.MolesFreckles, OverlayData.MolesFrecklesOpacity);

            SetPedHeadOverlay(ped, 10, OverlayData.ChestHair, OverlayData.ChestHairOpacity);
            SetPedHeadOverlayColor(ped, 10, 1, OverlayData.ChestHairColour, OverlayData.ChestHairColour);

            SetPedHeadOverlay(ped, 11, OverlayData.BodyBlemish, OverlayData.BodyBlemishOpacity);

            SetPedEyeColor(ped, OverlayData.EyeColour);
            #endregion
            #region Components
            SetPedComponentVariation(ped, 0, ComponentData.Torso, 0, 1); // Torso
            SetPedComponentVariation(ped, 3, ComponentData.Pants, ComponentData.PantsTexture, 1); // Pants
            SetPedComponentVariation(ped, 6, ComponentData.Shoes, ComponentData.ShoesTexture, 1); // Shoes
            SetPedComponentVariation(ped, 8, ComponentData.Undershirt, ComponentData.UndershirtTexture, 1); // Undershirt
            SetPedComponentVariation(ped, 11, ComponentData.Shirt, ComponentData.ShirtTexture, 1); // Shirt
            #endregion

            

            Game.PlayerPed.Position = new Vector3(float.Parse(Character[5]), float.Parse(Character[6]), float.Parse(Character[7]));
            Game.PlayerPed.Heading = float.Parse(Character[8]);
            Game.PlayerPed.Money = int.Parse(Character[9]);

            await Delay(1000);

            DoScreenFadeIn(500);

            FreezeEntityPosition(ped, false);
            SetEntityCollision(ped, true, true);
            Game.Player.CanControlCharacter = true;

            await Delay(5000);

            SetPlayerInvincible(ped, false);
        }
    }
}
