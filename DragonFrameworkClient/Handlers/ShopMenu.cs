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
    class ShopMenus : BaseScript
    {
        DrawMarkers DrawMarkers = new DrawMarkers();
        ManageInteractionMenu ManageInteractionMenu = new ManageInteractionMenu();
        CharacterCreation CharacterCreation = new CharacterCreation();
        GenerateClothes GenerateClothes = new GenerateClothes();

        public ShopMenus()
        {
            Tick += tCheckInShop;
        }
        private async Task tCheckInShop() => CheckInShop();

        public void CheckInShop()
        {
            if (DrawMarkers.Blips.Count < 1 && !DrawMarkers.AreBlipsMade) DrawMarkers.LoadBlipData();

            Vector3 PlayerLocation = Game.PlayerPed.Position;

            foreach (DrawMarkers.BlipMarkerData data in DrawMarkers.Blips)
            {
                float distance = World.GetDistance(Game.PlayerPed.Position, new Vector3(data.X, data.Y, data.Z));

                if (distance < 3)
                {
                    switch (data.SpriteID)
                    {
                        case null: return;
                        case 71: BarberShop(); return;
                        case 73: ClothesShop(); return;
                        case 75: TattooShop(); return;
                        case 110: BarberShop(); return;
                        case 313: BarberShop(); return;
                        default: return;
                    }
                }
                else if (distance > 3 && distance < 3.5)
                {
                    MenuController.CloseAllMenus();
                    MenuController.MainMenu = ManageInteractionMenu.CreateInteractionMenu();
                }
            }
        }

        private void BarberShop()
        {
            if (MenuController.IsAnyMenuOpen()) return;

            int ped = Game.PlayerPed.Handle;

            Menu BarberMenu = new Menu("Barbers", null);
            MenuListItem HairStyle;


            CharacterCreation.GenerateParts();

            #region Ref ints/Floats
            int junk = 0;

            int intBeardStyle = 0;
            int intBeardColour = 0;
            float floatBeardOpacity = 0;
            GetPedHeadOverlayData(ped, 1, ref intBeardStyle, ref junk, ref intBeardColour, ref junk, ref floatBeardOpacity);

            int intEyebrowsStyle = 0;
            int intEyebrowsColour = 0;
            float floatEyebrowsOpacity = 0;
            GetPedHeadOverlayData(ped, 1, ref intEyebrowsStyle, ref junk, ref intEyebrowsColour, ref junk, ref floatEyebrowsOpacity);

            int intMakeupStyle = 0;
            int intMakeupColour = 0;
            float floatMakeupOpacity = 0;
            GetPedHeadOverlayData(ped, 1, ref intMakeupStyle, ref junk, ref intMakeupColour, ref junk, ref floatMakeupOpacity);

            int intBlushStyle = 0;
            int intBlushColour = 0;
            float floatBlushOpacity = 0;
            GetPedHeadOverlayData(ped, 1, ref intBlushStyle, ref junk, ref intBlushColour, ref junk, ref floatBlushOpacity);

            int intLipstickStyle = 0;
            int intLipstickColour = 0;
            float floatLipstickOpacity = 0;
            GetPedHeadOverlayData(ped, 1, ref intLipstickStyle, ref junk, ref intLipstickColour, ref junk, ref floatLipstickOpacity);
            #endregion

            #region MenuItems
            if (Game.PlayerPed.Model == "mp_m_freemode_01") HairStyle = new MenuListItem("Hair", CharacterCreation.MaleHair, GetPedDrawableVariation(ped, 2));
            else HairStyle = new MenuListItem("Hair", CharacterCreation.FemaleHair, GetPedDrawableVariation(ped, 2));

            MenuListItem HairColour = new MenuListItem("Hair Colour", CharacterCreation.OverlayColours, GetPedHairColor(ped)) { ShowColorPanel = true, ColorPanelColorType = MenuListItem.ColorPanelType.Hair };
            MenuListItem HairHighlights = new MenuListItem("Hair Highlights", CharacterCreation.OverlayColours, GetPedHairHighlightColor(ped)) { ShowColorPanel = true, ColorPanelColorType = MenuListItem.ColorPanelType.Hair };

            MenuListItem BeardStyle = new MenuListItem("Beard", CharacterCreation.BeardList, intBeardStyle);
            MenuListItem BeardOpacity = new MenuListItem("Beard Opacity", CharacterCreation.Opacity, 5) { ShowOpacityPanel = true };
            MenuListItem BeardColour = new MenuListItem("Beard Colour", CharacterCreation.OverlayColours, intBeardColour) { ShowColorPanel = true, ColorPanelColorType = MenuListItem.ColorPanelType.Hair };

            MenuListItem EyebrowsStyle = new MenuListItem("Eyebrows", CharacterCreation.EyebrowsList, intEyebrowsStyle);
            MenuListItem EyebrowsOpacity = new MenuListItem("Eyebrow Opacity", CharacterCreation.Opacity, 5) { ShowOpacityPanel = true };
            MenuListItem EyebrowsColour = new MenuListItem("Eyebrow Colour", CharacterCreation.OverlayColours, intEyebrowsColour) { ShowColorPanel = true, ColorPanelColorType = MenuListItem.ColorPanelType.Hair };

            MenuListItem MakeupStyle = new MenuListItem("Makeup", CharacterCreation.MakeupList, intMakeupStyle);
            MenuListItem MakeupOpacity = new MenuListItem("Makeup Opacity", CharacterCreation.Opacity, 5) { ShowOpacityPanel = true };
            MenuListItem MakeupColour = new MenuListItem("Makeup Colour", CharacterCreation.OverlayColours, intMakeupColour) { ShowColorPanel = true, ColorPanelColorType = MenuListItem.ColorPanelType.Hair };

            MenuListItem BlushStyle = new MenuListItem("Blush", CharacterCreation.BlushList, intBlushStyle);
            MenuListItem BlushOpacity = new MenuListItem("Blush Opacity", CharacterCreation.Opacity, 5) { ShowOpacityPanel = true };
            MenuListItem BlushColour = new MenuListItem("Blush Colour", CharacterCreation.OverlayColours, intBlushColour) { ShowColorPanel = true, ColorPanelColorType = MenuListItem.ColorPanelType.Hair };

            MenuListItem LipstickStyle = new MenuListItem("Lipstick", CharacterCreation.LipstickList, intLipstickStyle);
            MenuListItem LipstickOpacity = new MenuListItem("Lipstick Opacity", CharacterCreation.Opacity, 5) { ShowOpacityPanel = true };
            MenuListItem LipstickColour = new MenuListItem("Lipstick Colour", CharacterCreation.OverlayColours, intLipstickColour) { ShowColorPanel = true, ColorPanelColorType = MenuListItem.ColorPanelType.Hair };

            MenuListItem EyeColour = new MenuListItem("Eye Colour", CharacterCreation.EyeColours, GetPedEyeColor(ped));

            BarberMenu.AddMenuItem(HairStyle);
            BarberMenu.AddMenuItem(HairColour);
            BarberMenu.AddMenuItem(HairHighlights);
            if ((int)Game.PlayerPed.Model.NativeValue == 1885233650)
            {
                BarberMenu.AddMenuItem(BeardStyle);
                BarberMenu.AddMenuItem(BeardOpacity);
                BarberMenu.AddMenuItem(BeardColour);
            }
            BarberMenu.AddMenuItem(EyebrowsStyle);
            BarberMenu.AddMenuItem(EyebrowsOpacity);
            BarberMenu.AddMenuItem(EyebrowsColour);
            BarberMenu.AddMenuItem(MakeupStyle);
            BarberMenu.AddMenuItem(MakeupOpacity);
            BarberMenu.AddMenuItem(MakeupColour);
            BarberMenu.AddMenuItem(BlushStyle);
            BarberMenu.AddMenuItem(BlushOpacity);
            BarberMenu.AddMenuItem(BlushColour);
            BarberMenu.AddMenuItem(LipstickStyle);
            BarberMenu.AddMenuItem(LipstickOpacity);
            BarberMenu.AddMenuItem(LipstickColour);
            BarberMenu.AddMenuItem(EyeColour);
            #endregion

            MenuController.AddMenu(BarberMenu);
            BarberMenu.OpenMenu();

            BarberMenu.OnListIndexChange += async (menu, listItem, oldIndex, newIndex, itemIndex) =>
            {
                switch (listItem.Text)
                {
                    case "Hair":
                        SetPedComponentVariation(Game.PlayerPed.Handle, 2, HairStyle.ListIndex, 0, 1);
                        break;
                    case "Hair Colour":
                    case "Hair Highlights":
                        SetPedHairColor(Game.PlayerPed.Handle, HairColour.ListIndex, HairHighlights.ListIndex);
                        break;
                    case "Beard":
                    case "Beard Opacity":
                    case "Beard Colour":
                        SetPedHeadOverlay(Game.PlayerPed.Handle, 1, BeardStyle.ListIndex, (float)BeardOpacity.ListIndex / 10);
                        SetPedHeadOverlayColor(Game.PlayerPed.Handle, 1, 1, BeardColour.ListIndex, BeardColour.ListIndex);
                        break;
                    case "Eyebrows":
                    case "Eyebrow Opacity":
                    case "Eyebrow Colour":
                        SetPedHeadOverlay(Game.PlayerPed.Handle, 2, EyebrowsStyle.ListIndex, (float)EyebrowsOpacity.ListIndex / 10);
                        SetPedHeadOverlayColor(Game.PlayerPed.Handle, 2, 1, EyebrowsColour.ListIndex, EyebrowsColour.ListIndex);
                        break;
                    case "Makeup":
                    case "Makeup Opacity":
                    case "Makeup Colour":
                        SetPedHeadOverlay(Game.PlayerPed.Handle, 4, MakeupStyle.ListIndex, (float)MakeupOpacity.ListIndex / 10);
                        SetPedHeadOverlayColor(Game.PlayerPed.Handle, 4, 1, MakeupColour.ListIndex, MakeupColour.ListIndex);
                        break;
                    case "Blush":
                    case "Blush Opacity":
                    case "Blush Colour":
                        SetPedHeadOverlay(Game.PlayerPed.Handle, 5, BlushStyle.ListIndex, (float)BlushOpacity.ListIndex / 10);
                        SetPedHeadOverlayColor(Game.PlayerPed.Handle, 5, 1, BlushColour.ListIndex, BlushColour.ListIndex);
                        break;
                    case "Lipstick":
                    case "Lipstick Opacity":
                    case "Lipstick Colour":
                        SetPedHeadOverlay(Game.PlayerPed.Handle, 8, LipstickStyle.ListIndex, (float)LipstickOpacity.ListIndex / 10);
                        SetPedHeadOverlayColor(Game.PlayerPed.Handle, 8, 1, LipstickColour.ListIndex, LipstickColour.ListIndex);
                        break;
                    case "Eye Colour":
                        SetPedEyeColor(Game.PlayerPed.Handle, EyeColour.ListIndex);
                        break;
                }
            };
            BarberMenu.OnMenuOpen += async (menu) => { MenuController.DisableBackButton = true; MenuController.PreventExitingMenu = true; };
            BarberMenu.OnMenuClose += async (menu) => { MenuController.DisableBackButton = false; MenuController.PreventExitingMenu = false; };

        }

        #region Clothes shop
        private void ClothesShop()
        {
            if (MenuController.IsAnyMenuOpen()) return;

            if (Game.PlayerPed.Model == "mp_m_freemode_01") MaleShop();
            else FemaleShop();
        }
        private void MaleShop()
        {
            int ped = Game.PlayerPed.Handle;

            List<GenerateClothes.Torso> Tees = new List<GenerateClothes.Torso>();
            List<GenerateClothes.Torso> Tanks = new List<GenerateClothes.Torso>();
            List<GenerateClothes.Torso> Polos = new List<GenerateClothes.Torso>();
            List<GenerateClothes.Torso> Shirts = new List<GenerateClothes.Torso>();
            List<GenerateClothes.Torso> Sweaters = new List<GenerateClothes.Torso>();
            List<GenerateClothes.Torso> Hoodies = new List<GenerateClothes.Torso>();
            List<GenerateClothes.Torso> Jackets = new List<GenerateClothes.Torso>();

            Tees.Clear();
            Tanks.Clear();
            Polos.Clear();
            Shirts.Clear();
            Sweaters.Clear();
            Hoodies.Clear();
            Jackets.Clear();


            Menu ClothingMenu = new Menu("Clothes store", "Clothing");

            Menu TeeMenu = new Menu("T-Shirts", "T-Shirts");
            MenuItem TeeMenuItem = new MenuItem("T-Shirt", "Selection of T-Shirt");
            Menu TankMenu = new Menu("Tank Tops", "Tank Tops");
            MenuItem TankMenuItem = new MenuItem("Tank Tops", "Selection of Tank Tops");
            Menu PoloMenu = new Menu("Polos", "Polos");
            MenuItem PoloMenuItem = new MenuItem("Polos", "Selection of Polos");
            Menu ShirtMenu = new Menu("Shirts", "Shirts");
            MenuItem ShirtMenuItem = new MenuItem("Shirts", "Selection of Shirts");
            Menu SweaterMenu = new Menu("Sweaters", "Sweaters");
            MenuItem SweaterMenuItem = new MenuItem("Sweaters", "Selection of Sweaters");
            Menu HoodieMenu = new Menu("Hoodies", "Hoodies");
            MenuItem HoodieMenuItem = new MenuItem("Hoodies", "Selection of Hoodies");
            Menu JacketMenu = new Menu("Jackets", "Jackets");
            MenuItem JacketMenuItem = new MenuItem("Jackets", "Selection of Jackets");

            ClothingMenu.OnMenuOpen += (menu) => MenuController.DisableBackButton = true;
            ClothingMenu.OnMenuClose += (menu) => MenuController.DisableBackButton = false;

            foreach (GenerateClothes.Torso data in GenerateClothes.MaleTorso)
            {
                if (data.Type == "Tee")
                {
                    List<int> ListInt = data.Textures;
                    List<string> list = new List<string>();
                    foreach (int texture in ListInt) list.Add($"Texture #{list.Count + 1}");

                    Tees.Add(data);

                    TeeMenu.AddMenuItem(new MenuListItem($"T-shirt #{TeeMenu.GetMenuItems().Count + 1}", list, 0));
                }
                else if (data.Type == "Tank")
                {
                    List<int> ListInt = data.Textures;
                    List<string> list = new List<string>();
                    foreach (int texture in ListInt) list.Add($"Texture #{list.Count + 1}");

                    Tanks.Add(data);

                    TankMenu.AddMenuItem(new MenuListItem($"Tank #{TeeMenu.GetMenuItems().Count + 1}", list, 0));
                }
                else if (data.Type == "Polo")
                {
                    List<int> ListInt = data.Textures;
                    List<string> list = new List<string>();
                    foreach (int texture in ListInt) list.Add($"Texture #{list.Count + 1}");

                    Polos.Add(data);

                    PoloMenu.AddMenuItem(new MenuListItem($"Polo #{TeeMenu.GetMenuItems().Count + 1}", list, 0));
                }
                else if (data.Type == "Shirt")
                {
                    List<int> ListInt = data.Textures;
                    List<string> list = new List<string>();
                    foreach (int texture in ListInt) list.Add($"Texture #{list.Count + 1}");

                    Shirts.Add(data);

                    ShirtMenu.AddMenuItem(new MenuListItem($"Shirt #{TeeMenu.GetMenuItems().Count + 1}", list, 0));
                }
                else if (data.Type == "Sweater")
                {
                    List<int> ListInt = data.Textures;
                    List<string> list = new List<string>();
                    foreach (int texture in ListInt) list.Add($"Texture #{list.Count + 1}");

                    Sweaters.Add(data);

                    SweaterMenu.AddMenuItem(new MenuListItem($"Sweater #{TeeMenu.GetMenuItems().Count + 1}", list, 0));
                }
                else if (data.Type == "Hoodie")
                {
                    List<int> ListInt = data.Textures;
                    List<string> list = new List<string>();
                    foreach (int texture in ListInt) list.Add($"Texture #{list.Count + 1}");

                    Hoodies.Add(data);

                    HoodieMenu.AddMenuItem(new MenuListItem($"Hoodie #{TeeMenu.GetMenuItems().Count + 1}", list, 0));
                }
                else if (data.Type == "Jacket")
                {
                    List<int> ListInt = data.Textures;
                    List<string> list = new List<string>();
                    foreach (int texture in ListInt) list.Add($"Texture #{list.Count + 1}");

                    Jackets.Add(data);

                    JacketMenu.AddMenuItem(new MenuListItem($"Jacket #{TeeMenu.GetMenuItems().Count + 1}", list, 0));
                }
            }


            TeeMenu.OnListIndexChange += (menu, listItem, oldIndex, newIndex, itemIndex) =>
            {
                SetTorso(Tees[itemIndex].ArmsUpper, Tees[itemIndex].Shirt, Tees[itemIndex].UnderShirt, Tees[itemIndex].Textures[listItem.ListIndex], Game.PlayerPed.Handle);
            };
            TankMenu.OnListIndexChange += (menu, listItem, oldIndex, newIndex, itemIndex) =>
            {
                SetTorso(Tanks[itemIndex].ArmsUpper, Tanks[itemIndex].Shirt, Tanks[itemIndex].UnderShirt, Tanks[itemIndex].Textures[listItem.ListIndex], Game.PlayerPed.Handle);
            };
            PoloMenu.OnListIndexChange += (menu, listItem, oldIndex, newIndex, itemIndex) =>
            {
                SetTorso(Polos[itemIndex].ArmsUpper, Polos[itemIndex].Shirt, Polos[itemIndex].UnderShirt, Polos[itemIndex].Textures[listItem.ListIndex], Game.PlayerPed.Handle);
            };
            ShirtMenu.OnListIndexChange += (menu, listItem, oldIndex, newIndex, itemIndex) =>
            {
                SetTorso(Shirts[itemIndex].ArmsUpper, Shirts[itemIndex].Shirt, Shirts[itemIndex].UnderShirt, Shirts[itemIndex].Textures[listItem.ListIndex], Game.PlayerPed.Handle);
            };
            SweaterMenu.OnListIndexChange += (menu, listItem, oldIndex, newIndex, itemIndex) =>
            {
                SetTorso(Sweaters[itemIndex].ArmsUpper, Sweaters[itemIndex].Shirt, Sweaters[itemIndex].UnderShirt, Sweaters[itemIndex].Textures[listItem.ListIndex], Game.PlayerPed.Handle);
            };
            HoodieMenu.OnListIndexChange += (menu, listItem, oldIndex, newIndex, itemIndex) =>
            {
                SetTorso(Hoodies[itemIndex].ArmsUpper, Hoodies[itemIndex].Shirt, Hoodies[itemIndex].UnderShirt, Hoodies[itemIndex].Textures[listItem.ListIndex], Game.PlayerPed.Handle);
            };
            JacketMenu.OnListIndexChange += (menu, listItem, oldIndex, newIndex, itemIndex) =>
            {
                SetTorso(Jackets[itemIndex].ArmsUpper, Jackets[itemIndex].Shirt, Jackets[itemIndex].UnderShirt, Jackets[itemIndex].Textures[listItem.ListIndex], Game.PlayerPed.Handle);
            };

            ClothingMenu.AddMenuItem(TeeMenuItem);
            MenuController.BindMenuItem(ClothingMenu, TeeMenu, TeeMenuItem);

            ClothingMenu.AddMenuItem(TankMenuItem);
            MenuController.BindMenuItem(ClothingMenu, TankMenu, TankMenuItem);

            ClothingMenu.AddMenuItem(PoloMenuItem);
            MenuController.BindMenuItem(ClothingMenu, PoloMenu, PoloMenuItem);

            ClothingMenu.AddMenuItem(ShirtMenuItem);
            MenuController.BindMenuItem(ClothingMenu, ShirtMenu, ShirtMenuItem);

            ClothingMenu.AddMenuItem(SweaterMenuItem);
            MenuController.BindMenuItem(ClothingMenu, SweaterMenu, SweaterMenuItem);

            ClothingMenu.AddMenuItem(HoodieMenuItem);
            MenuController.BindMenuItem(ClothingMenu, HoodieMenu, HoodieMenuItem);

            ClothingMenu.AddMenuItem(JacketMenuItem);
            MenuController.BindMenuItem(ClothingMenu, JacketMenu, JacketMenuItem);

            MenuController.AddMenu(ClothingMenu);

            ClothingMenu.OpenMenu();

        }
        private void FemaleShop()
        {
            int ped = Game.PlayerPed.Handle;

            List<GenerateClothes.Torso> Tees = new List<GenerateClothes.Torso>();
            List<GenerateClothes.Torso> Tanks = new List<GenerateClothes.Torso>();
            List<GenerateClothes.Torso> Polos = new List<GenerateClothes.Torso>();
            List<GenerateClothes.Torso> Shirts = new List<GenerateClothes.Torso>();
            List<GenerateClothes.Torso> Sweaters = new List<GenerateClothes.Torso>();
            List<GenerateClothes.Torso> Hoodies = new List<GenerateClothes.Torso>();
            List<GenerateClothes.Torso> Jackets = new List<GenerateClothes.Torso>();

            Tees.Clear();
            Tanks.Clear();
            Polos.Clear();
            Shirts.Clear();
            Sweaters.Clear();
            Hoodies.Clear();
            Jackets.Clear();


            Menu ClothingMenu = new Menu("Clothes store", "Clothing");

            Menu TeeMenu = new Menu("T-Shirts", "T-Shirts");
            MenuItem TeeMenuItem = new MenuItem("T-Shirt", "Selection of T-Shirt");
            Menu TankMenu = new Menu("Tank Tops", "Tank Tops");
            MenuItem TankMenuItem = new MenuItem("Tank Tops", "Selection of Tank Tops");
            Menu PoloMenu = new Menu("Polos", "Polos");
            MenuItem PoloMenuItem = new MenuItem("Polos", "Selection of Polos");
            Menu ShirtMenu = new Menu("Shirts", "Shirts");
            MenuItem ShirtMenuItem = new MenuItem("Shirts", "Selection of Shirts");
            Menu SweaterMenu = new Menu("Sweaters", "Sweaters");
            MenuItem SweaterMenuItem = new MenuItem("Sweaters", "Selection of Sweaters");
            Menu HoodieMenu = new Menu("Hoodies", "Hoodies");
            MenuItem HoodieMenuItem = new MenuItem("Hoodies", "Selection of Hoodies");
            Menu JacketMenu = new Menu("Jackets", "Jackets");
            MenuItem JacketMenuItem = new MenuItem("Jackets", "Selection of Jackets");

            ClothingMenu.OnMenuOpen += (menu) => MenuController.DisableBackButton = true;
            ClothingMenu.OnMenuClose += (menu) => MenuController.DisableBackButton = false;

            foreach (GenerateClothes.Torso data in GenerateClothes.MaleTorso)
            {
                if (data.Type == "Tee")
                {
                    List<int> ListInt = data.Textures;
                    List<string> list = new List<string>();
                    foreach (int texture in ListInt) list.Add($"Texture #{list.Count + 1}");

                    Tees.Add(data);

                    TeeMenu.AddMenuItem(new MenuListItem($"T-shirt #{TeeMenu.GetMenuItems().Count + 1}", list, 0));
                }
                else if (data.Type == "Tank")
                {
                    List<int> ListInt = data.Textures;
                    List<string> list = new List<string>();
                    foreach (int texture in ListInt) list.Add($"Texture #{list.Count + 1}");

                    Tanks.Add(data);

                    TankMenu.AddMenuItem(new MenuListItem($"Tank #{TeeMenu.GetMenuItems().Count + 1}", list, 0));
                }
                else if (data.Type == "Polo")
                {
                    List<int> ListInt = data.Textures;
                    List<string> list = new List<string>();
                    foreach (int texture in ListInt) list.Add($"Texture #{list.Count + 1}");

                    Polos.Add(data);

                    PoloMenu.AddMenuItem(new MenuListItem($"Polo #{TeeMenu.GetMenuItems().Count + 1}", list, 0));
                }
                else if (data.Type == "Shirt")
                {
                    List<int> ListInt = data.Textures;
                    List<string> list = new List<string>();
                    foreach (int texture in ListInt) list.Add($"Texture #{list.Count + 1}");

                    Shirts.Add(data);

                    ShirtMenu.AddMenuItem(new MenuListItem($"Shirt #{TeeMenu.GetMenuItems().Count + 1}", list, 0));
                }
                else if (data.Type == "Sweater")
                {
                    List<int> ListInt = data.Textures;
                    List<string> list = new List<string>();
                    foreach (int texture in ListInt) list.Add($"Texture #{list.Count + 1}");

                    Sweaters.Add(data);

                    SweaterMenu.AddMenuItem(new MenuListItem($"Sweater #{TeeMenu.GetMenuItems().Count + 1}", list, 0));
                }
                else if (data.Type == "Hoodie")
                {
                    List<int> ListInt = data.Textures;
                    List<string> list = new List<string>();
                    foreach (int texture in ListInt) list.Add($"Texture #{list.Count + 1}");

                    Hoodies.Add(data);

                    HoodieMenu.AddMenuItem(new MenuListItem($"Hoodie #{TeeMenu.GetMenuItems().Count + 1}", list, 0));
                }
                else if (data.Type == "Jacket")
                {
                    List<int> ListInt = data.Textures;
                    List<string> list = new List<string>();
                    foreach (int texture in ListInt) list.Add($"Texture #{list.Count + 1}");

                    Jackets.Add(data);

                    JacketMenu.AddMenuItem(new MenuListItem($"Jacket #{TeeMenu.GetMenuItems().Count + 1}", list, 0));
                }
            }


            TeeMenu.OnListIndexChange += (menu, listItem, oldIndex, newIndex, itemIndex) =>
            {
                SetTorso(Tees[itemIndex].ArmsUpper, Tees[itemIndex].Shirt, Tees[itemIndex].UnderShirt, Tees[itemIndex].Textures[listItem.ListIndex], Game.PlayerPed.Handle);
            };
            TankMenu.OnListIndexChange += (menu, listItem, oldIndex, newIndex, itemIndex) =>
            {
                SetTorso(Tanks[itemIndex].ArmsUpper, Tanks[itemIndex].Shirt, Tanks[itemIndex].UnderShirt, Tanks[itemIndex].Textures[listItem.ListIndex], Game.PlayerPed.Handle);
            };
            PoloMenu.OnListIndexChange += (menu, listItem, oldIndex, newIndex, itemIndex) =>
            {
                SetTorso(Polos[itemIndex].ArmsUpper, Polos[itemIndex].Shirt, Polos[itemIndex].UnderShirt, Polos[itemIndex].Textures[listItem.ListIndex], Game.PlayerPed.Handle);
            };
            ShirtMenu.OnListIndexChange += (menu, listItem, oldIndex, newIndex, itemIndex) =>
            {
                SetTorso(Shirts[itemIndex].ArmsUpper, Shirts[itemIndex].Shirt, Shirts[itemIndex].UnderShirt, Shirts[itemIndex].Textures[listItem.ListIndex], Game.PlayerPed.Handle);
            };
            SweaterMenu.OnListIndexChange += (menu, listItem, oldIndex, newIndex, itemIndex) =>
            {
                SetTorso(Sweaters[itemIndex].ArmsUpper, Sweaters[itemIndex].Shirt, Sweaters[itemIndex].UnderShirt, Sweaters[itemIndex].Textures[listItem.ListIndex], Game.PlayerPed.Handle);
            };
            HoodieMenu.OnListIndexChange += (menu, listItem, oldIndex, newIndex, itemIndex) =>
            {
                SetTorso(Hoodies[itemIndex].ArmsUpper, Hoodies[itemIndex].Shirt, Hoodies[itemIndex].UnderShirt, Hoodies[itemIndex].Textures[listItem.ListIndex], Game.PlayerPed.Handle);
            };
            JacketMenu.OnListIndexChange += (menu, listItem, oldIndex, newIndex, itemIndex) =>
            {
                SetTorso(Jackets[itemIndex].ArmsUpper, Jackets[itemIndex].Shirt, Jackets[itemIndex].UnderShirt, Jackets[itemIndex].Textures[listItem.ListIndex], Game.PlayerPed.Handle);
            };

            ClothingMenu.AddMenuItem(TeeMenuItem);
            MenuController.BindMenuItem(ClothingMenu, TeeMenu, TeeMenuItem);

            ClothingMenu.AddMenuItem(TankMenuItem);
            MenuController.BindMenuItem(ClothingMenu, TankMenu, TankMenuItem);

            ClothingMenu.AddMenuItem(PoloMenuItem);
            MenuController.BindMenuItem(ClothingMenu, PoloMenu, PoloMenuItem);

            ClothingMenu.AddMenuItem(ShirtMenuItem);
            MenuController.BindMenuItem(ClothingMenu, ShirtMenu, ShirtMenuItem);

            ClothingMenu.AddMenuItem(SweaterMenuItem);
            MenuController.BindMenuItem(ClothingMenu, SweaterMenu, SweaterMenuItem);

            ClothingMenu.AddMenuItem(HoodieMenuItem);
            MenuController.BindMenuItem(ClothingMenu, HoodieMenu, HoodieMenuItem);

            ClothingMenu.AddMenuItem(JacketMenuItem);
            MenuController.BindMenuItem(ClothingMenu, JacketMenu, JacketMenuItem);

            MenuController.AddMenu(ClothingMenu);

            ClothingMenu.OpenMenu();

        }

        private void SetTorso(int hands, int shirt, int undershirt, int texture, int ped)
        {
            SetPedComponentVariation(ped, 11, shirt, texture, 1); // shirt
            SetPedComponentVariation(ped, 8, undershirt, 1, 1); // Undershirt
            SetPedComponentVariation(ped, 3, hands, 0, 1); // Arms
        }
        #endregion

        private void TattooShop()
        {

        }
        private void GunShop()
        {

        }
        private void GunRangeShop()
        {

        }
    }
}
