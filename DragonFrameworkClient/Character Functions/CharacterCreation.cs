using CitizenFX.Core;
using MenuAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using static CitizenFX.Core.Native.API;

namespace DragonFrameworkClient
{
    class CharacterCreation : BaseScript
    {
        ManageInteractionMenu ManageInteractionMenu = new ManageInteractionMenu();

        #region Character Save
        int junk;
        PedOverlays OverlayData = new PedOverlays();
        Face FaceData = new Face();
        Components ComponentData = new Components();
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
        #endregion



        Random random = new Random();
        List<float> mixValues = new List<float>() { 0.0f, 0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 0.6f, 0.7f, 0.8f, 0.9f, 1.0f };
        List<float> faceFeaturesValuesList = new List<float>()
            {
               -1.0f,    // 0
        	   -0.9f,    // 1
        	   -0.8f,    // 2
        	   -0.7f,    // 3
        	   -0.6f,    // 4
        	   -0.5f,    // 5
        	   -0.4f,    // 6
        	   -0.3f,    // 7
        	   -0.2f,    // 8
        	   -0.1f,    // 9
        		0.0f,    // 10
        		0.1f,    // 11
        		0.2f,    // 12
        		0.3f,    // 13
        		0.4f,    // 14
        		0.5f,    // 15
        		0.6f,    // 16
        		0.7f,    // 17
        		0.8f,    // 18
        		0.9f,    // 19
        		1.0f     // 20
        	};
        string[] faceFeaturesNamesList = new string[20]
        {
                "Nose Width",               // 0
        		"Noes Peak Height",         // 1
        		"Nose Peak Length",         // 2
        		"Nose Bone Height",         // 3
        		"Nose Peak Lowering",       // 4
        		"Nose Bone Twist",          // 5
        		"Eyebrows Height",          // 6
        		"Eyebrows Depth",           // 7
        		"Cheekbones Height",        // 8
        		"Cheekbones Width",         // 9
        		"Cheeks Width",             // 10
        		"Eyes Opening",             // 11
        		"Lips Thickness",           // 12
        		"Jaw Bone Width",           // 13
        		"Jaw Bone Depth/Length",    // 14
        		"Chin Height",              // 15
        		"Chin Depth/Length",        // 16
        		"Chin Width",               // 17
        		"Chin Hole Size",           // 18
        		"Neck Thickness"            // 19
        };

        Menu CCMenu;
        Camera MainCamera;
        Camera FaceCam;
        Camera BodyCam;

        #region Ped settings
        int InitialFatherFace;
        int InitialMotherFace;
        int InitialHairStyle;
        int InitialHairColour;
        int InitialEyeColour;
        public int Gender;
        #endregion
        #region Camera locations
        Vector3 BodyPos = new Vector3(402.85f, -997.85f, -98.5f); // R E S E T   T O   98.5
        Vector3 FullBodyLookAt = new Vector3(402.85f, -996.5f, -98.5f);

        Vector3 FacePos = new Vector3(402.85f, -997f, -98.35f);
        Vector3 FaceLookAt = new Vector3(402.85f, -996.5f, -98.35f);
        #endregion
        #region Spawn points
        Tuple<float, float, float, float> StationOne = new Tuple<float, float, float, float>(428, -978.5f, 30, 90);
        Tuple<float, float, float, float> StationTwo = new Tuple<float, float, float, float>(341.5f, -1558f, 29, 320);
        Tuple<float, float, float, float> StationThree = new Tuple<float, float, float, float>(-1053, -816f, 19, 321);
        Tuple<float, float, float, float> StationFour = new Tuple<float, float, float, float>(-557.50f, -142, 38, 200);
        #endregion
        #region Components
        public List<string> Opacity = new List<string>() { "0%", "10%", "20%", "30%", "40%", "50%", "60%", "70%", "80%", "90%", "100%" };
        public List<string> Hair = new List<string>();
        public List<string> MaleHair = new List<string>();
        public List<string> FemaleHair = new List<string>();
        public List<string> OverlayColours = new List<string>();
        public List<string> Blemish = new List<string>();
        public List<string> BeardList = new List<string>();
        public List<string> EyebrowsList = new List<string>();
        public List<string> AgeingList = new List<string>();
        public List<string> MakeupList = new List<string>();
        public List<string> BlushList = new List<string>();
        public List<string> ComplexionList = new List<string>();
        public List<string> SundamageList = new List<string>();
        public List<string> LipstickList = new List<string>();
        public List<string> MolesFrecklesList = new List<string>();
        public List<string> ChestHairList = new List<string>();
        public List<string> BodyBlemishList = new List<string>();
        public List<string> EyeColours = new List<string>();

        public List<string> Faces = new List<string>();
        #endregion

        public CharacterCreation()
        {
            //EventHandlers.Add("dFrame:PlayerFirstSpawn", new Action(PlayerFirstSpawn));
        }

        public async void CreationStart()
        {
            MenuController.CloseAllMenus();

            await Delay(500);

            Gender = random.Next(0, 1);
            if (Gender == 0) CreatePed(Game.Player, InitialFatherFace = random.Next(0, 46), InitialMotherFace = random.Next(0, 46), InitialHairStyle = random.Next(0, 36),
                 InitialHairColour = random.Next(0, GetNumHairColors() - 4), InitialEyeColour = random.Next(0, 31), "mp_m_freemode_01");

            else CreatePed(Game.Player, InitialFatherFace = random.Next(0, 46), InitialMotherFace = random.Next(0, 46), InitialHairStyle = random.Next(0, 38),
                 InitialHairColour = random.Next(0, GetNumHairColors() - 4), InitialEyeColour = random.Next(0, 31), "mp_f_freemode_01");

            SetCamera();
            ManageMenu();
        }

        #region Camera stuffs
        private async void SetCamera()
        {
            DisplayRadar(false);

            Player player = Game.Player;

            SetEntityCoords(player.Character.Handle, 402.85f, -996.5f, -100, true, false, false, true);
            player.Character.Heading = 180f;

            FreezeEntityPosition(player.Handle, true);
            SetEntityCollision(player.Handle, false, false);
            SetEntityInvincible(player.Handle, true);
            Game.Player.CanControlCharacter = false;

            int cam = CreateCam("DEFAULT_SCRIPTED_CAMERA", true);
            MainCamera = new Camera(cam);
            MainCamera.Position = BodyPos;
            MainCamera.PointAt(FullBodyLookAt);

            Game.PlayerPed.Task.ClearAllImmediately();

            RenderScriptCams(true, false, 0, true, true);

            await Delay(0);

            return;
        }
        private async void ResetCamera()
        {
            await Delay(550);

            MenuController.CloseAllMenus();

            CCMenu.ClearMenuItems();

            DisplayRadar(true);
            DisplayHud(true);

            int ped = Game.PlayerPed.Handle;

            FreezeEntityPosition(ped, false);
            SetEntityCollision(ped, true, true);
            SetEntityInvincible(ped, false);
            Game.Player.CanControlCharacter = true;

            RenderScriptCams(false, false, 0, false, false);
            DestroyAllCams(true);

            ManageInteractionMenu.CreateInteractionMenu();

            await Delay(1000);

            DoScreenFadeIn(500);

            return;
        }
        #endregion

        private async void DoFinish(string Name)
        {
            DoScreenFadeOut(500);

            await Delay(500);

            Ped player = Game.PlayerPed;
            int ped = Game.PlayerPed.Handle;

            if (Game.PlayerPed.Model == "mp_m_freemode_01")
            {
                SetPedComponentVariation(ped, 3, 0, 0, 1); // Arms
                SetPedComponentVariation(ped, 4, 13, 1, 1); // Pants
                SetPedComponentVariation(ped, 5, 9, 0, 1); // Bags
                SetPedComponentVariation(ped, 6, 4, 0, 1); // Feet
                SetPedComponentVariation(ped, 8, 2, 0, 1); // Top
                SetPedComponentVariation(ped, 11, 0, 0, 1); // Top

            }
            else if (Game.PlayerPed.Model == "mp_f_freemode_01")
            {
                SetPedComponentVariation(ped, 3, 14, 0, 1); // Arms
                SetPedComponentVariation(ped, 4, 45, 1, 1); // Pants
                SetPedComponentVariation(ped, 5, 9, 0, 1); // Bags
                SetPedComponentVariation(ped, 6, 33, 1, 1); // Feet
                SetPedComponentVariation(ped, 8, 3, 0, 1); // Top
                SetPedComponentVariation(ped, 11, 73, 0, 1); // Top
            }

            await Delay(50);

            int SelectSpawn = random.Next(0, 5);

            if (SelectSpawn == 1) { SetEntityCoords(ped, StationOne.Item1, StationOne.Item2, StationOne.Item3, true, false, false, true); player.Heading = StationOne.Item4; }
            else if (SelectSpawn == 2) { SetEntityCoords(ped, StationTwo.Item1, StationTwo.Item2, StationTwo.Item3, true, false, false, true); player.Heading = StationTwo.Item4; }
            else if (SelectSpawn == 3) { SetEntityCoords(ped, StationThree.Item1, StationThree.Item2, StationThree.Item3, true, false, false, true); player.Heading = StationThree.Item4; }
            else { SetEntityCoords(ped, StationFour.Item1, StationFour.Item2, StationFour.Item3, true, false, false, true); player.Heading = StationFour.Item4; }

            GetOverlayData();
            GetFaceData();
            GetComponentData();

            if (Game.PlayerPed.Model == "mp_m_freemode_01")
                TriggerServerEvent("dFrame:SaveNewPed", Name,"Male", JsonConvert.SerializeObject(OverlayData), JsonConvert.SerializeObject(FaceData), JsonConvert.SerializeObject(ComponentData), player.Position.X, player.Position.Y, player.Position.Z, player.Heading, player.Money, "");
            else if (Game.PlayerPed.Model == "mp_f_freemode_01")
            {
                TriggerServerEvent("dFrame:SaveNewPed", Name, "Female", JsonConvert.SerializeObject(OverlayData), JsonConvert.SerializeObject(FaceData), JsonConvert.SerializeObject(ComponentData), player.Position.X, player.Position.Y, player.Position.Z, player.Heading, player.Money, "");
            }

            ResetCamera();
        }

        private async void CreatePed(Player player, int InitialFatherFace, int InitialMotherFace, int InitialHairStyle, int InitialHairColour, int InitialEyeColour, string Model)
        {
            uint model = (uint)GetHashKey(Model);
            RequestModel(model);


            await player.ChangeModel(Model);

            int ped = player.Character.Handle;

            SetPedHeadBlendData(ped, InitialFatherFace, InitialMotherFace, 0, InitialFatherFace, InitialMotherFace, 0, mixValues[5], mixValues[5], 0, false);

            SetPedComponentVariation(ped, 2, InitialHairStyle, 0, 1);
            SetPedHairColor(ped, InitialHairColour, InitialHairColour);

            if (Model == "mp_m_freemode_01")
            {
                SetPedComponentVariation(ped, 3, 15, 0, 1); // Arms
                SetPedComponentVariation(ped, 4, 14, 1, 1); // Pants
                SetPedComponentVariation(ped, 5, 9, 0, 1); // Bags
                SetPedComponentVariation(ped, 6, 34, 0, 1); // Feet
                SetPedComponentVariation(ped, 11, 15, 0, 1); // Top
                SetPedComponentVariation(ped, 8, 15, 0, 1); // Top
            }
            else if (Model == "mp_f_freemode_01")
            {
                SetPedComponentVariation(ped, 3, 4, 0, 1); // Arms
                SetPedComponentVariation(ped, 4, 14, 8, 1); // Pants
                SetPedComponentVariation(ped, 5, 9, 0, 1); // Bags
                SetPedComponentVariation(ped, 6, 35, 0, 1); // Feet
                SetPedComponentVariation(ped, 11, 5, 0, 1); // Top
                SetPedComponentVariation(ped, 8, 3, 0, 1); // Top
            }

            SetPedEyeColor(ped, InitialEyeColour);

            return;
        }

        private async void SavePedAndCreate()
        {
            var spacer = "\t";
            AddTextEntry($"CHARACTERNAME_WINDOW_TITLE", $"Character Name (MAX 100 Characters)");

            // Display the input box.
            DisplayOnscreenKeyboard(1, $"CHARACTERNAME_WINDOW_TITLE", "", "Apples", "", "", "", 50);
            await Delay(0);
            // Wait for a result.
            while (true)
            {
                int keyboardStatus = UpdateOnscreenKeyboard();

                switch (keyboardStatus)
                {
                    case 3: // not displaying input field anymore somehow
                    case 2: // cancelled
                        return;
                    case 1: // finished editing
                        DoFinish(GetOnscreenKeyboardResult());
                        return;
                    default:
                        await Delay(0);
                        break;
                }
            }
        }

        #region Menus
        private async void ManageMenu()
        {
            CCMenu = new Menu("Character Creation", "Create your character");

            MenuListItem GenderItem = new MenuListItem("Gender", new List<string> { "Male", "Female" }, 0);
            MenuItem SaveAndCreate = new MenuItem("Save and Create", "Saves your character, and spawns you in");

            CCMenu.AddMenuItem(GenderItem);

            GenerateParts();
            GenerateSubMenus();

            CCMenu.AddMenuItem(SaveAndCreate);

            CCMenu.OnMenuOpen += async (menu) => MenuController.DisableBackButton = true;
            CCMenu.OnMenuClose += async (menu) => MenuController.DisableBackButton = false;
            CCMenu.OnListIndexChange += async (menu, listItem, oldIndex, newIndex, itemIndex) =>
            {
                if (listItem == GenderItem)
                {
                    if (listItem.ListIndex == 0)
                    {
                        DoScreenFadeOut(250);
                        await Delay(250);

                        Gender = 0;

                        CreatePed(Game.Player, InitialFatherFace = random.Next(0, 46), InitialMotherFace = random.Next(0, 46), InitialHairStyle = random.Next(0, 38),
                            InitialHairColour = random.Next(0, GetNumHairColors() - 4), InitialEyeColour = random.Next(0, 31), "mp_m_freemode_01");

                        CCMenu.ClearMenuItems();
                        CCMenu.AddMenuItem(GenderItem);
                        GenerateSubMenus();
                        CCMenu.AddMenuItem(SaveAndCreate);

                        await Delay(250);

                        CCMenu.RefreshIndex();
                        DoScreenFadeIn(500);
                    }
                    else if (listItem.ListIndex == 1)
                    {
                        DoScreenFadeOut(250);
                        await Delay(250);

                        Gender = 1;

                        CreatePed(Game.Player, InitialFatherFace = random.Next(0, 46), InitialMotherFace = random.Next(0, 46), InitialHairStyle = random.Next(0, 38),
                            InitialHairColour = random.Next(0, GetNumHairColors() - 4), InitialEyeColour = random.Next(0, 31), "mp_f_freemode_01");

                        CCMenu.ClearMenuItems();
                        CCMenu.AddMenuItem(GenderItem);
                        GenerateSubMenus();
                        CCMenu.AddMenuItem(SaveAndCreate);

                        await Delay(250);

                        CCMenu.RefreshIndex();
                        DoScreenFadeIn(500);
                    }
                }
            };
            CCMenu.OnItemSelect += async (menu, listItem, index) =>
            {
                if (listItem == SaveAndCreate)
                {
                    Debug.WriteLine(Gender.ToString());
                    if (GenderItem.ListIndex == 0 && GetPedDrawableVariation(Game.PlayerPed.Handle, 2) == 23)
                    {
                        SetNotificationTextEntry("CELL_EMAIL_BCON"); // 10x ~a~
                        AddTextComponentSubstringPlayerName($"~r~You are unable to create a character with that hair style");
                        DrawNotification(false, false);
                    }
                    else if (GenderItem.ListIndex == 1 && GetPedDrawableVariation(Game.PlayerPed.Handle, 2) == 24)
                    {
                        SetNotificationTextEntry("CELL_EMAIL_BCON"); // 10x ~a~
                        AddTextComponentSubstringPlayerName($"~r~You are unable to create a character with that hair style");
                        DrawNotification(false, false);
                    }
                    else SavePedAndCreate();
                }
            };
            await Delay(750);

            MenuController.AddMenu(CCMenu);

            DoScreenFadeIn(500);

            CCMenu.OpenMenu();
        }
        public void GenerateParts()
        {
            MaleHair.Clear();
            FemaleHair.Clear();
            OverlayColours.Clear();
            Blemish.Clear();
            BeardList.Clear();
            EyebrowsList.Clear();
            AgeingList.Clear();
            MakeupList.Clear();
            BlushList.Clear();
            ComplexionList.Clear();
            SundamageList.Clear();
            LipstickList.Clear();
            MolesFrecklesList.Clear();
            ChestHairList.Clear();
            BodyBlemishList.Clear();
            EyeColours.Clear();

            Faces.Clear();

            foreach (int number in Enumerable.Range(0, 36)) MaleHair.Add($"Style #{number + 1}");
            foreach (int number in Enumerable.Range(0, 38)) FemaleHair.Add($"Style #{number + 1}");
            foreach (int number in Enumerable.Range(0, GetNumHairColors())) OverlayColours.Add($"Colour #{number + 1}");
            foreach (int number in Enumerable.Range(0, GetNumHeadOverlayValues(0))) Blemish.Add($"Style #{number + 1}");
            foreach (int number in Enumerable.Range(0, GetNumHeadOverlayValues(1))) BeardList.Add($"Style #{number + 1}");
            foreach (int number in Enumerable.Range(0, GetNumHeadOverlayValues(2))) EyebrowsList.Add($"Style #{number + 1}");
            foreach (int number in Enumerable.Range(0, GetNumHeadOverlayValues(3))) AgeingList.Add($"Style #{number + 1}");
            foreach (int number in Enumerable.Range(0, GetNumHeadOverlayValues(4))) MakeupList.Add($"Style #{number + 1}");
            foreach (int number in Enumerable.Range(0, GetNumHeadOverlayValues(5))) BlushList.Add($"Style #{number + 1}");
            foreach (int number in Enumerable.Range(0, GetNumHeadOverlayValues(6))) ComplexionList.Add($"Style #{number + 1}");
            foreach (int number in Enumerable.Range(0, GetNumHeadOverlayValues(7))) SundamageList.Add($"Style #{number + 1}");
            foreach (int number in Enumerable.Range(0, GetNumHeadOverlayValues(8))) LipstickList.Add($"Style #{number + 1}");
            foreach (int number in Enumerable.Range(0, GetNumHeadOverlayValues(9))) MolesFrecklesList.Add($"Style #{number + 1}");
            foreach (int number in Enumerable.Range(0, GetNumHeadOverlayValues(10))) ChestHairList.Add($"Style #{number + 1}");
            foreach (int number in Enumerable.Range(0, GetNumHeadOverlayValues(11))) BodyBlemishList.Add($"Style #{number + 1}");
            foreach (int number in Enumerable.Range(0, 31)) EyeColours.Add($"Colour #{number + 1}");

            foreach (int number in Enumerable.Range(0, 45)) Faces.Add($"#{number + 1}");

            return;
        }
        private void GenerateSubMenus()
        {
            #region Inhetirance menu
            Menu InheritanceMenu = new Menu("Inheritance", "Inheritance options");
            MenuItem InheritanceMenuItem = new MenuItem("Inheritance", "Change your characters inheritance");
            InheritanceMenu.ClearMenuItems();

            MenuListItem Father = new MenuListItem("Father", Faces, InitialFatherFace);
            MenuListItem Mother = new MenuListItem("Mother", Faces, InitialMotherFace);
            MenuSliderItem ShapeMix = new MenuSliderItem("Head Shape Mix", "Select how much of your head shape should be inherited from your father or mother.", 0, 10, 5, true)
            { SliderLeftIcon = MenuItem.Icon.MALE, SliderRightIcon = MenuItem.Icon.FEMALE };
            MenuSliderItem SkinMix = new MenuSliderItem("Body Skin Mix", "Select how much of your body skin tone should be inherited from your father or mother.", 0, 10, 5, true)
            { SliderLeftIcon = MenuItem.Icon.MALE, SliderRightIcon = MenuItem.Icon.FEMALE };

            InheritanceMenu.AddMenuItem(Father);
            InheritanceMenu.AddMenuItem(Mother);
            InheritanceMenu.AddMenuItem(ShapeMix);
            InheritanceMenu.AddMenuItem(SkinMix);

            CCMenu.AddMenuItem(InheritanceMenuItem);
            MenuController.BindMenuItem(CCMenu, InheritanceMenu, InheritanceMenuItem);

            InheritanceMenu.OnListIndexChange += (menu, listItem, oldIndex, newIndex, itemIndex) =>
            {
                SetPedHeadBlendData(GetPlayerPed(-1), Father.ListIndex, Mother.ListIndex, 0, Father.ListIndex, Mother.ListIndex, 0, mixValues[ShapeMix.Position], mixValues[SkinMix.Position], 0f, false);
            };
            InheritanceMenu.OnSliderPositionChange += (menu, listItem, oldIndex, newIndex, itemIndex) =>
            {
                SetPedHeadBlendData(GetPlayerPed(-1), Father.ListIndex, Mother.ListIndex, 0, Father.ListIndex, Mother.ListIndex, 0, mixValues[ShapeMix.Position], mixValues[SkinMix.Position], 0f, false);
            };
            InheritanceMenu.OnMenuOpen += (menu) => SetFaceCam();
            InheritanceMenu.OnMenuClose += (menu) => SetBodyCam();
            #endregion

            #region Face Shape
            Menu FaceShape = new Menu("Face shape", "Face shape options");
            MenuItem FaceShapeItem = new MenuItem("Face shape", "Tweak the details of your characters face");
            FaceShape.ClearMenuItems();

            for (int i = 0; i < 20; i++)
            {
                MenuSliderItem faceFeature = new MenuSliderItem(faceFeaturesNamesList[i], $"Set the {faceFeaturesNamesList[i]} face feature value.", 0, 20, 10, true);
                FaceShape.AddMenuItem(faceFeature);
            }

            FaceShape.OnSliderPositionChange += async (sender, sliderItem, oldPosition, newPosition, itemIndex) =>
            {
                float value = faceFeaturesValuesList[newPosition];
                SetPedFaceFeature(Game.PlayerPed.Handle, itemIndex, value);
            };

            FaceShape.OnMenuOpen += (menu) => SetFaceCam();
            FaceShape.OnMenuClose += (menu) => SetBodyCam();


            CCMenu.AddMenuItem(FaceShapeItem);
            MenuController.BindMenuItem(CCMenu, FaceShape, FaceShapeItem);

            #endregion

            #region Appearance
            Menu AppearanceMenu = new Menu("Appearance", "Appearance options");
            MenuItem AppearanceMenuItem = new MenuItem("Appearance", "Change your characters appearance");
            AppearanceMenu.ClearMenuItems();

            if (Gender == 0) Hair = MaleHair;
            else Hair = FemaleHair;

            #region MenuList
            MenuListItem HairStyle = new MenuListItem("Hair", MaleHair, InitialHairStyle);
            MenuListItem HairColour = new MenuListItem("Hair Colour", OverlayColours, InitialHairColour) { ShowColorPanel = true, ColorPanelColorType = MenuListItem.ColorPanelType.Hair };
            MenuListItem HairHighlights = new MenuListItem("Hair Highlights", OverlayColours, InitialHairColour) { ShowColorPanel = true, ColorPanelColorType = MenuListItem.ColorPanelType.Hair };
            AppearanceMenu.AddMenuItem(HairStyle);
            AppearanceMenu.AddMenuItem(HairColour);
            AppearanceMenu.AddMenuItem(HairHighlights);

            MenuListItem BlemishStyle = new MenuListItem("Blemish", Blemish, random.Next(0, GetNumHeadOverlayValues(0) - 1));
            MenuListItem BlemishOpacity = new MenuListItem("Blemish Opacity", Opacity, 0) { ShowOpacityPanel = true };
            AppearanceMenu.AddMenuItem(BlemishStyle);
            AppearanceMenu.AddMenuItem(BlemishOpacity);


            MenuListItem BeardStyle = new MenuListItem("Beard", BeardList, 1);
            MenuListItem BeardOpacity = new MenuListItem("Beard Opacity", Opacity, 0) { ShowOpacityPanel = true };
            MenuListItem BeardColour = new MenuListItem("Beard Colour", OverlayColours, InitialHairColour) { ShowColorPanel = true, ColorPanelColorType = MenuListItem.ColorPanelType.Hair };
            if (Gender == 0)
            {
                AppearanceMenu.AddMenuItem(BeardStyle);
                AppearanceMenu.AddMenuItem(BeardOpacity);
                AppearanceMenu.AddMenuItem(BeardColour);
            }

            MenuListItem EyebrowsStyle = new MenuListItem("Eyebrows", EyebrowsList, random.Next(0, EyebrowsList.Count()));
            MenuListItem EyebrowsOpacity = new MenuListItem("Eyebrow Opacity", Opacity, 8) { ShowOpacityPanel = true };
            MenuListItem EyebrowsColour = new MenuListItem("Eyebrow Colour", OverlayColours, 0) { ShowColorPanel = true, ColorPanelColorType = MenuListItem.ColorPanelType.Hair };
            AppearanceMenu.AddMenuItem(EyebrowsStyle);
            AppearanceMenu.AddMenuItem(EyebrowsOpacity);
            AppearanceMenu.AddMenuItem(EyebrowsColour);

            MenuListItem Ageing = new MenuListItem("Ageing", AgeingList, 4);
            MenuListItem AgeingOpacity = new MenuListItem("Ageing Opacity", Opacity, 0) { ShowOpacityPanel = true };
            AppearanceMenu.AddMenuItem(Ageing);
            AppearanceMenu.AddMenuItem(AgeingOpacity);

            MenuListItem MakeupStyle = new MenuListItem("Makeup", MakeupList, 4);
            MenuListItem MakeupOpacity = new MenuListItem("Makeup Opacity", Opacity, 8) { ShowOpacityPanel = true };
            MenuListItem MakeupColour = new MenuListItem("Makeup Colour", OverlayColours, 0) { ShowColorPanel = true, ColorPanelColorType = MenuListItem.ColorPanelType.Hair };
            AppearanceMenu.AddMenuItem(MakeupStyle);
            AppearanceMenu.AddMenuItem(MakeupOpacity);
            AppearanceMenu.AddMenuItem(MakeupColour);

            MenuListItem BlushStyle = new MenuListItem("Blush", BlushList, 4);
            MenuListItem BlushOpacity = new MenuListItem("Blush Opacity", Opacity, 8) { ShowOpacityPanel = true };
            MenuListItem BlushColour = new MenuListItem("Blush Colour", OverlayColours, 0) { ShowColorPanel = true, ColorPanelColorType = MenuListItem.ColorPanelType.Hair };
            AppearanceMenu.AddMenuItem(BlushStyle);
            AppearanceMenu.AddMenuItem(BlushOpacity);
            AppearanceMenu.AddMenuItem(BlushColour);

            MenuListItem ComplexionStyle = new MenuListItem("Complexion", ComplexionList, 4);
            MenuListItem ComplexionOpacity = new MenuListItem("Complexion Opacity", Opacity, 0) { ShowOpacityPanel = true };
            AppearanceMenu.AddMenuItem(ComplexionStyle);
            AppearanceMenu.AddMenuItem(ComplexionOpacity);

            MenuListItem SunDamageStyle = new MenuListItem("Sun Damage", SundamageList, 4);
            MenuListItem SunDamageOpacity = new MenuListItem("Sun Damage Opacity", Opacity, 0) { ShowOpacityPanel = true };
            AppearanceMenu.AddMenuItem(SunDamageStyle);
            AppearanceMenu.AddMenuItem(SunDamageOpacity);

            MenuListItem LipstickStyle = new MenuListItem("Lipstick", LipstickList, 4);
            MenuListItem LipstickOpacity = new MenuListItem("Lipstick Opacity", Opacity, 0) { ShowOpacityPanel = true };
            MenuListItem LipstickColour = new MenuListItem("Lipstick Colour", OverlayColours, 0) { ShowColorPanel = true, ColorPanelColorType = MenuListItem.ColorPanelType.Hair };
            AppearanceMenu.AddMenuItem(LipstickStyle);
            AppearanceMenu.AddMenuItem(LipstickOpacity);
            AppearanceMenu.AddMenuItem(LipstickColour);

            MenuListItem MolesFrecklesStyle = new MenuListItem("Moles and Freckles", MolesFrecklesList, 4);
            MenuListItem MolesFrecklesOpacity = new MenuListItem("Moles and Freckles Opacity", Opacity, 0) { ShowOpacityPanel = true };
            AppearanceMenu.AddMenuItem(MolesFrecklesStyle);
            AppearanceMenu.AddMenuItem(MolesFrecklesOpacity);


            MenuListItem ChestHairStyle = new MenuListItem("Chest Hair", ChestHairList, 1);
            MenuListItem ChestHairOpacity = new MenuListItem("Chest Hair Opacity", Opacity, 0) { ShowOpacityPanel = true };
            MenuListItem ChestHairColour = new MenuListItem("Chest Hair Colour", OverlayColours, 0) { ShowColorPanel = true, ColorPanelColorType = MenuListItem.ColorPanelType.Hair };
            if (Gender == 0)
            {
                AppearanceMenu.AddMenuItem(ChestHairStyle);
                AppearanceMenu.AddMenuItem(ChestHairOpacity);
                AppearanceMenu.AddMenuItem(ChestHairColour);
            }

            MenuListItem BodyBlemishStyle = new MenuListItem("Body Blemish", BodyBlemishList, 4);
            MenuListItem BodyBlemishOpacity = new MenuListItem("Body Blemish Opacity", Opacity, 0) { ShowOpacityPanel = true };
            AppearanceMenu.AddMenuItem(BodyBlemishStyle);
            AppearanceMenu.AddMenuItem(BodyBlemishOpacity);

            MenuListItem EyeColour = new MenuListItem("Eye Colour", EyeColours, InitialEyeColour);
            AppearanceMenu.AddMenuItem(EyeColour);

            CCMenu.AddMenuItem(AppearanceMenuItem);
            MenuController.BindMenuItem(CCMenu, AppearanceMenu, AppearanceMenuItem);
            #endregion
            AppearanceMenu.OnListIndexChange += async (menu, listItem, oldIndex, newIndex, itemIndex) =>
            {
                switch (listItem.Text)
                {
                    case "Hair":
                        SetFaceCam();
                        SetPedComponentVariation(Game.PlayerPed.Handle, 2, HairStyle.ListIndex, 0, 1);
                        break;
                    case "Hair Colour":
                    case "Hair Highlights":
                        SetFaceCam();
                        SetPedHairColor(Game.PlayerPed.Handle, HairColour.ListIndex, HairHighlights.ListIndex);
                        break;
                    case "Blemish":
                    case "Blemish Opacity":
                        SetFaceCam();
                        SetPedHeadOverlay(Game.PlayerPed.Handle, 0, BlemishStyle.ListIndex, (float)BlemishOpacity.ListIndex / 10);
                        break;
                    case "Beard":
                    case "Beard Opacity":
                    case "Beard Colour":
                        SetFaceCam();
                        SetPedHeadOverlay(Game.PlayerPed.Handle, 1, BeardStyle.ListIndex, (float)BeardOpacity.ListIndex / 10);
                        SetPedHeadOverlayColor(Game.PlayerPed.Handle, 1, 1, BeardColour.ListIndex, BeardColour.ListIndex);
                        break;
                    case "Eyebrows":
                    case "Eyebrow Opacity":
                    case "Eyebrow Colour":
                        SetFaceCam();
                        SetPedHeadOverlay(Game.PlayerPed.Handle, 2, EyebrowsStyle.ListIndex, (float)EyebrowsOpacity.ListIndex / 10);
                        SetPedHeadOverlayColor(Game.PlayerPed.Handle, 2, 1, EyebrowsColour.ListIndex, EyebrowsColour.ListIndex);
                        break;
                    case "Ageing":
                    case "Ageing Opacity":
                        SetFaceCam();
                        SetPedHeadOverlay(Game.PlayerPed.Handle, 3, Ageing.ListIndex, (float)AgeingOpacity.ListIndex / 10);
                        break;
                    case "Makeup":
                    case "Makeup Opacity":
                    case "Makeup Colour":
                        SetFaceCam();
                        SetPedHeadOverlay(Game.PlayerPed.Handle, 4, MakeupStyle.ListIndex, (float)MakeupOpacity.ListIndex / 10);
                        SetPedHeadOverlayColor(Game.PlayerPed.Handle, 4, 1, MakeupColour.ListIndex, MakeupColour.ListIndex);
                        break;
                    case "Blush":
                    case "Blush Opacity":
                    case "Blush Colour":
                        SetFaceCam();
                        SetPedHeadOverlay(Game.PlayerPed.Handle, 5, BlushStyle.ListIndex, (float)BlushOpacity.ListIndex / 10);
                        SetPedHeadOverlayColor(Game.PlayerPed.Handle, 5, 1, BlushColour.ListIndex, BlushColour.ListIndex);
                        break;
                    case "Complexion":
                    case "Complexion Opacity":
                        SetFaceCam();
                        SetPedHeadOverlay(Game.PlayerPed.Handle, 6, ComplexionStyle.ListIndex, (float)ComplexionOpacity.ListIndex / 10);
                        break;
                    case "Sun Damage":
                    case "Sun Damage Opacity":
                        SetFaceCam();
                        SetPedHeadOverlay(Game.PlayerPed.Handle, 7, SunDamageStyle.ListIndex, (float)SunDamageOpacity.ListIndex / 10);
                        break;
                    case "Lipstick":
                    case "Lipstick Opacity":
                    case "Lipstick Colour":
                        SetFaceCam();
                        SetPedHeadOverlay(Game.PlayerPed.Handle, 8, LipstickStyle.ListIndex, (float)LipstickOpacity.ListIndex / 10);
                        SetPedHeadOverlayColor(Game.PlayerPed.Handle, 8, 1, LipstickColour.ListIndex, LipstickColour.ListIndex);
                        break;
                    case "Moles and Freckles":
                    case "Moles and Freckles Opacity":
                        SetFaceCam();
                        SetPedHeadOverlay(Game.PlayerPed.Handle, 9, MolesFrecklesStyle.ListIndex, (float)MolesFrecklesOpacity.ListIndex / 10);
                        break;
                    case "Chest Hair":
                    case "Chest Hair Opacity":
                    case "Chest Hair Colour":
                        SetBodyCam();
                        SetPedHeadOverlay(Game.PlayerPed.Handle, 10, ChestHairStyle.ListIndex, (float)ChestHairOpacity.ListIndex / 10);
                        SetPedHeadOverlayColor(Game.PlayerPed.Handle, 10, 1, ChestHairColour.ListIndex, ChestHairColour.ListIndex);
                        break;
                    case "Body Blemish":
                    case "Body Blemish Opacity":
                        SetBodyCam();
                        SetPedHeadOverlay(Game.PlayerPed.Handle, 11, BodyBlemishStyle.ListIndex, (float)BodyBlemishOpacity.ListIndex / 10);
                        break;
                    case "Eye Colour":
                        SetFaceCam();
                        SetPedEyeColor(Game.PlayerPed.Handle, EyeColour.ListIndex);
                        break;
                }
            };
            AppearanceMenu.OnMenuClose += (menu) => SetBodyCam();
            #endregion

            return;
        }

        private void SetFaceCam()
        {
            if (MainCamera.Position == FacePos) return;
            int cam = CreateCam("DEFAULT_SCRIPTED_CAMERA", true);
            FaceCam = new Camera(cam);
            FaceCam.Position = FacePos;
            FaceCam.PointAt(FaceLookAt);

            MainCamera.InterpTo(FaceCam, 750, true, true);

            MainCamera = FaceCam;

            //BodyCam.InterpTo(FaceCam, 750, true, true);
        }
        private void SetBodyCam()
        {
            if (MainCamera.Position == BodyPos) return;

            int cam = CreateCam("DEFAULT_SCRIPTED_CAMERA", true);
            BodyCam = new Camera(cam);
            BodyCam.Position = BodyPos;
            BodyCam.PointAt(FullBodyLookAt);

            //FaceCam.InterpTo(BodyCam, 750, true, true);
            MainCamera.InterpTo(BodyCam, 750, true, true);

            MainCamera = BodyCam;
        }
        #endregion

        #region Get ped data
        private void GetOverlayData()
        {
            int blemish = 0; float blemishopacity = 0;
            GetPedHeadOverlayData(Game.PlayerPed.Handle, 0, ref blemish, ref junk, ref junk, ref junk, ref blemishopacity);
            OverlayData.Blemish = blemish; OverlayData.BlemishOpacity = blemishopacity;

            int facial = 0; int facialcolour = 0; float facialopacity = 0;
            GetPedHeadOverlayData(Game.PlayerPed.Handle, 1, ref facial, ref junk, ref facialcolour, ref junk, ref facialopacity);
            OverlayData.FacialHair = facial; OverlayData.FacialHairColour = facialcolour; OverlayData.FacialHairOpacity = facialopacity;

            int eyebrow = 0; int eyebrowcolour = 0; float eyebrowopacity = 0;
            GetPedHeadOverlayData(Game.PlayerPed.Handle, 2, ref eyebrow, ref junk, ref eyebrowcolour, ref junk, ref eyebrowopacity);
            OverlayData.Eyebrows = eyebrow; OverlayData.EyebrowsColour = eyebrowcolour; OverlayData.EyebrowsOpacity = eyebrowopacity;

            int ageing = 0; float ageingopacity = 0;
            GetPedHeadOverlayData(Game.PlayerPed.Handle, 3, ref ageing, ref junk, ref junk, ref junk, ref ageingopacity);
            OverlayData.Ageing = ageing; OverlayData.AgeingOpacity = ageingopacity;

            int makeup = 0; int makeupcolour = 0; float makeupopacity = 0;
            GetPedHeadOverlayData(Game.PlayerPed.Handle, 4, ref makeup, ref junk, ref makeupcolour, ref junk, ref makeupopacity);
            OverlayData.Makeup = makeup; OverlayData.MakeupColour = makeupcolour; OverlayData.MakeupOpacity = makeupopacity;

            int blush = 0; int blushcolour = 0; float blushopacity = 0;
            GetPedHeadOverlayData(Game.PlayerPed.Handle, 5, ref blush, ref junk, ref blushcolour, ref junk, ref blushopacity);
            OverlayData.Blush = blush; OverlayData.BlushColour = blushcolour; OverlayData.BlushOpacity = blushopacity;

            int complexion = 0; float complexionopacity = 0;
            GetPedHeadOverlayData(Game.PlayerPed.Handle, 6, ref complexion, ref junk, ref junk, ref junk, ref complexionopacity);
            OverlayData.Complexion = complexion; OverlayData.ComplexionOpacity = complexionopacity;

            int sundamage = 0; float sundamageopacity = 0;
            GetPedHeadOverlayData(Game.PlayerPed.Handle, 7, ref sundamage, ref junk, ref junk, ref junk, ref sundamageopacity);
            OverlayData.Sundamage = sundamage; OverlayData.SundamageOpacity = sundamageopacity;

            int lipstick = 0; int lipstickcolour = 0; float lipstickopacity = 0;
            GetPedHeadOverlayData(Game.PlayerPed.Handle, 8, ref lipstick, ref junk, ref lipstickcolour, ref junk, ref lipstickopacity);
            OverlayData.Lipstick = lipstick; OverlayData.LipstickColour = lipstickcolour; OverlayData.LipstickOpacity = lipstickopacity;

            int molesfreckles = 0; float molesfrecklesopacity = 0;
            GetPedHeadOverlayData(Game.PlayerPed.Handle, 9, ref molesfreckles, ref junk, ref junk, ref junk, ref molesfrecklesopacity);
            OverlayData.MolesFreckles = molesfreckles; OverlayData.MolesFrecklesOpacity = molesfrecklesopacity;

            int chesthair = 0; int chesthaircolour = 0; float chesthairopacity = 0;
            GetPedHeadOverlayData(Game.PlayerPed.Handle, 10, ref chesthair, ref junk, ref chesthaircolour, ref junk, ref chesthairopacity);
            OverlayData.ChestHair = chesthair; OverlayData.ChestHairOpacity = chesthairopacity;

            int bodyblemish = 0; float bodyblemishopacity = 0;
            GetPedHeadOverlayData(Game.PlayerPed.Handle, 11, ref bodyblemish, ref junk, ref junk, ref junk, ref bodyblemishopacity);
            OverlayData.BodyBlemish = bodyblemish; OverlayData.BodyBlemishOpacity = bodyblemishopacity;

            int eyecolour = GetPedEyeColor(Game.PlayerPed.Handle);
            OverlayData.EyeColour = eyecolour;
        }
        private void GetFaceData()
        {
            int ped = Game.PlayerPed.Handle;

            PedHeadBlendData a = Game.PlayerPed.GetHeadBlendData();
            FaceData.FatherFace = a.FirstFaceShape;
            FaceData.MotherFace = a.SecondFaceShape;
            FaceData.FatherSkin = a.FirstSkinTone;
            FaceData.MotherSkin = a.SecondSkinTone;
            FaceData.ShapePercent = a.ParentFaceShapePercent;
            FaceData.SkinPercent = a.ParentSkinTonePercent;

            FaceData.NoseWidth = GetPedFaceFeature(ped, 0);
            FaceData.NoesPeakHeight = GetPedFaceFeature(ped, 1);
            FaceData.NosePeakLength = GetPedFaceFeature(ped, 2);
            FaceData.NoseBoneHeight = GetPedFaceFeature(ped, 3);
            FaceData.NosePeakLowering = GetPedFaceFeature(ped, 4);
            FaceData.NoseBoneTwist = GetPedFaceFeature(ped, 5);
            FaceData.EyebrowsHeight = GetPedFaceFeature(ped, 6);
            FaceData.EyebrowsDepth = GetPedFaceFeature(ped, 7);
            FaceData.CheekbonesHeight = GetPedFaceFeature(ped, 8);
            FaceData.CheekbonesWidth = GetPedFaceFeature(ped, 9);
            FaceData.CheeksWidth = GetPedFaceFeature(ped, 10);
            FaceData.EyesOpening = GetPedFaceFeature(ped, 11);
            FaceData.LipsThickness = GetPedFaceFeature(ped, 12);
            FaceData.JawBoneWidth = GetPedFaceFeature(ped, 13);
            FaceData.JawBoneDepthLength = GetPedFaceFeature(ped, 14);
            FaceData.ChinHeight = GetPedFaceFeature(ped, 15);
            FaceData.ChinDepthLength = GetPedFaceFeature(ped, 16);
            FaceData.ChinWidth = GetPedFaceFeature(ped, 17);
            FaceData.ChinHoleSize = GetPedFaceFeature(ped, 18);
            FaceData.NeckThickness = GetPedFaceFeature(ped, 19);
        }
        private void GetComponentData()
        {
            int ped = Game.PlayerPed.Handle;

            ComponentData.Torso = GetPedDrawableVariation(ped, 0);
            ComponentData.TorsoTexture = GetPedTextureVariation(ped, 0);
            ComponentData.Pants = GetPedDrawableVariation(ped, 3);
            ComponentData.PantsTexture = GetPedTextureVariation(ped, 0);
            ComponentData.Shoes = GetPedDrawableVariation(ped, 6);
            ComponentData.ShoesTexture = GetPedTextureVariation(ped, 0);
            ComponentData.Undershirt = GetPedDrawableVariation(ped, 8);
            ComponentData.UndershirtTexture = GetPedTextureVariation(ped, 0);
            ComponentData.Shirt = GetPedDrawableVariation(ped, 11);
            ComponentData.ShirtTexture = GetPedTextureVariation(ped, 0);
            ComponentData.Hair = GetPedDrawableVariation(ped, 2);
            ComponentData.HairColour = GetPedHairColor(ped);
            ComponentData.HairHighlight = GetPedHairHighlightColor(ped);
        }
        #endregion
    }
}
