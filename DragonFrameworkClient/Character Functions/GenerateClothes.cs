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
    class GenerateClothes : BaseScript
    {
        public static List<Torso> MaleTorso;
        public static List<Pants> MalePants;

        public static List<Torso> FemaleTorso;
        public static List<Pants> FemalePants;

        public class Torso
        {
            public int ArmsUpper { get; set; }
            public int UnderShirt { get; set; }
            public int Shirt { get; set; }
            public List<int> Textures { get; set; }
            public string Type { get; set; }
        }
        public class Pants
        {
            public int Bottoms { get; set; }
            public List<int> Textures { get; set; }
            public string Type { get; set; }
        }

        /*
         * Hands:
         * Gloveless 
         * 0 = Tshirt
         * 1 = Jacket
         * 2 = tank
         * 3 = nothing
         * 4 = jacket (Small neck)
         * 5 = idk
         * 6 = Jacket (large neck)
         * 7 = Nothing (Same as 4)
         * 8 = long tshirt
         * 9 = nothing (Same as 4)
         * 10 = nothing (Same as 4)
         * 11 = Short tee/shirt
         * 12 = Jacket (Large neck, not same as 6)
         * 13 = nothing (Same as 4)
         * 14 = Long sleve open jacket
         * 15 = Topless
         * 
         * 1 = Tshirt
         * 2 = Jacket (Medium neck)
         * 3 = Tank
         * 4 = Jacket (Small neck)
         * 5 = Topless-ish
         * 6 = Jacket (Large neck)
         * 7 = Long Tee/Shirt
         * 8 = Short Tee/Shirt
         * 9 = Jacket (Medium/Large neck)
         * 10 = Open jacket
         * 11 = Topless
         * 
         * 19 = Driving gloves 1,2
         * 30 = Leather gloves 1,2
         * 41 = Wool gloves 1,2
         * 52 = Fingerless wool gloves 1,2
         * 85 = Sergeon gloves 1,2
         * 99 = coloured wool gloves1,2,3,4,5,6,7,8,9
         * 
         */

        public async void GenerateEverything()
        {
            GenerateTorso();
            GeneratePants();

            await Delay(2500);

            await Delay(2500);
        }

        private void GenerateTorso()
        {
            MaleTorso = new List<Torso>
            { 
                #region Tees
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 1, Textures = new List<int>() { 0, 1, 3, 4, 5, 6, 7, 8, 11 }, Type = "Tee"},
                new Torso { ArmsUpper = 5, UnderShirt = 15, Shirt = 5, Textures = new List<int>() { 0, 1, 2, 7 }, Type = "Tee"},
                new Torso { ArmsUpper = 8, UnderShirt = 15, Shirt = 8, Textures = new List<int>() { 0, 10, 13, 14 }, Type = "Tee"},
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 16, Textures = new List<int>() { 0, 1, 2 }, Type = "Tee"},
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 22, Textures = new List<int>() { 0, 1, 2 }, Type = "Tee"},
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 33, Textures = new List<int>() { 0 }, Type = "Tee"},
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 34, Textures = new List<int>() { 0, 1 }, Type = "Tee"},
                new Torso { ArmsUpper = 8, UnderShirt = 15, Shirt = 38, Textures = new List<int>() { 0, 1, 2, 3, 4 }, Type = "Tee"},
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 44, Textures = new List<int>() { 0, 1, 2, 3 }, Type = "Tee"},
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 73, Textures = new List<int>() { 4, 7, 8, 10, 11, 12, 13, 14, 15, 16, 17, 18 }, Type = "Tee"},
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 80, Textures = new List<int>() { 0, 1, 2 }, Type = "Tee"},
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 81, Textures = new List<int>() { 0, 1, 2 }, Type = "Tee"},
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 83, Textures = new List<int>() { 0, 1, 2, 3, 4 }, Type = "Tee"},
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 146, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8 }, Type = "Tee"},
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 193, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25 }, Type = "Tee"},
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 208, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 }, Type = "Tee"},
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 226, Textures = new List<int>() { 0 }, Type = "Tee"},
                new Torso { ArmsUpper = 5, UnderShirt = 15, Shirt = 238, Textures = new List<int>() { 0, 1, 2, 3, 4, 5 }, Type = "Tee"},
                new Torso { ArmsUpper = 5, UnderShirt = 15, Shirt = 239, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 }, Type = "Tee"},
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 271, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 }, Type = "Tee"},
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 273, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 14, 15, 16 }, Type = "Tee"}, 
                #endregion
                #region Tanks
                new Torso { ArmsUpper = 5, UnderShirt = 15, Shirt = 3, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tank"},
                new Torso { ArmsUpper = 5, UnderShirt = 15, Shirt = 17, Textures = new List<int>() { 0, 1, 2, 3, 4, 5 }, Type = "Tank"},
                new Torso { ArmsUpper = 5, UnderShirt = 15, Shirt = 36, Textures = new List<int>() { 0, 1, 2, 3, 4, 5 }, Type = "Tank"},
                new Torso { ArmsUpper = 5, UnderShirt = 15, Shirt = 237, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25 }, Type = "Tank"}, 
                #endregion
                #region Polos
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 9, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 10, 11, 12, 13, 14, 15 }, Type = "Polo"},
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 39, Textures = new List<int>() { 0, 1 }, Type = "Polo"},
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 82, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 11, 12, 13, 14, 15 }, Type = "Polo"},
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 93, Textures = new List<int>() { 0, 1, 2 }, Type = "Polo"},
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 94, Textures = new List<int>() { 0, 1, 2 }, Type = "Polo"},
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 131, Textures = new List<int>() { 0 }, Type = "Polo"},
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 132, Textures = new List<int>() { 0 }, Type = "Polo"},
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 235, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 11 }, Type = "Polo"},
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 236, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 11 }, Type = "Polo"},
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 241, Textures = new List<int>() { 0, 1, 2, 3, 4, 5 }, Type = "Polo"},
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 242, Textures = new List<int>() { 0, 1, 2, 3, 4, 5 }, Type = "Polo"}, 
                #endregion
                #region Shirts
                new Torso { ArmsUpper = 1, UnderShirt = 15, Shirt = 12, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 10, 11 }, Type = "Shirt"},
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 13, Textures = new List<int>() { 0, 1, 2, 3, 5, 13 }, Type = "Shirt"},
                new Torso { ArmsUpper = 1, UnderShirt = 15, Shirt = 14, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 10, 11, 12, 13, 14, 15 }, Type = "Shirt"},
                new Torso { ArmsUpper = 11, UnderShirt = 15, Shirt = 26, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 10 }, Type = "Shirt"},
                new Torso { ArmsUpper = 1, UnderShirt = 15, Shirt = 41, Textures = new List<int>() { 0, 1, 2, 3 }, Type = "Shirt"},
                new Torso { ArmsUpper = 11, UnderShirt = 15, Shirt = 42, Textures = new List<int>() { 0 }, Type = "Shirt"},
                new Torso { ArmsUpper = 11, UnderShirt = 15, Shirt = 43, Textures = new List<int>() { 0 }, Type = "Shirt"},
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 63, Textures = new List<int>() { 0 }, Type = "Shirt"},
                new Torso { ArmsUpper = 11, UnderShirt = 15, Shirt = 95, Textures = new List<int>() { 0, 1, 2 }, Type = "Shirt"},
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 123, Textures = new List<int>() { 0, 1, 2 }, Type = "Shirt"},
                new Torso { ArmsUpper = 1, UnderShirt = 15, Shirt = 126, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 11, 12, 13, 14, 15 }, Type = "Shirt"},
                new Torso { ArmsUpper = 11, UnderShirt = 15, Shirt = 133, Textures = new List<int>() { 0 }, Type = "Shirt"},
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 234, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25 }, Type = "Shirt"},
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 260, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25 }, Type = "Shirt"}, 
                #endregion
                #region Sweaters
                new Torso { ArmsUpper = 1, UnderShirt = 15, Shirt = 50, Textures = new List<int>() { 0, 1, 2, 3, 4 }, Type = "Sweater"},
                new Torso { ArmsUpper = 1, UnderShirt = 15, Shirt = 78, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 11, 12, 13, 14, 15 }, Type = "Sweater"},
                new Torso { ArmsUpper = 2, UnderShirt = 15, Shirt = 89, Textures = new List<int>() { 0, 1, 2, 3 }, Type = "Sweater"},
                new Torso { ArmsUpper = 1, UnderShirt = 15, Shirt = 111, Textures = new List<int>() { 0, 1, 2, 3, 4, 5 }, Type = "Sweater"},
                new Torso { ArmsUpper = 1, UnderShirt = 15, Shirt = 139, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7 }, Type = "Sweater"},
                new Torso { ArmsUpper = 2, UnderShirt = 15, Shirt = 190, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25 }, Type = "Sweater"},
                new Torso { ArmsUpper = 1, UnderShirt = 15, Shirt = 259, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25 }, Type = "Sweater"}, 
                #endregion
                #region Combat
                new Torso { ArmsUpper = 1, UnderShirt = 15, Shirt = 49, Textures = new List<int>() { 0, 1, 2, 3, 4 }, Type = "Combat"},
                new Torso { ArmsUpper = 1, UnderShirt = 15, Shirt = 53, Textures = new List<int>() { 0, 1, 2, 3 }, Type = "Combat"},
                new Torso { ArmsUpper = 1, UnderShirt = 15, Shirt = 98, Textures = new List<int>() { 0, 1 }, Type = "Combat"},
                new Torso { ArmsUpper = 1, UnderShirt = 15, Shirt = 220, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25 }, Type = "Combat"},
                new Torso { ArmsUpper = 1, UnderShirt = 15, Shirt = 221, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25 }, Type = "Combat"},
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 222, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25 }, Type = "Combat"},
                new Torso { ArmsUpper = 2, UnderShirt = 15, Shirt = 247, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25 }, Type = "Combat"},
                new Torso { ArmsUpper = 1, UnderShirt = 15, Shirt = 248, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25 }, Type = "Combat"}, 
                #endregion
                #region Hoodie
                new Torso { ArmsUpper = 1, UnderShirt = 15, Shirt = 57, Textures = new List<int>() { 0 }, Type = "Hoodie"},
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 86, Textures = new List<int>() { 0, 1, 2, 3, 4 }, Type = "Hoodie"},
                new Torso { ArmsUpper = 2, UnderShirt = 15, Shirt = 205, Textures = new List<int>() { 0 }, Type = "Hoodie"},
                new Torso { ArmsUpper = 2, UnderShirt = 15, Shirt = 206, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 25 }, Type = "Hoodie"},
                #endregion
                #region Jacket
                new Torso { ArmsUpper = 1, UnderShirt = 15, Shirt = 61, Textures = new List<int>() { 0, 1, 2, 3 }, Type = "Jacket"},
                new Torso { ArmsUpper = 1, UnderShirt = 23, Shirt = 62, Textures = new List<int>() { 0 }, Type = "Jacket"},
                new Torso { ArmsUpper = 1, UnderShirt = 15, Shirt = 110, Textures = new List<int>() { 0 }, Type = "Jacket"},
                new Torso { ArmsUpper = 1, UnderShirt = 23, Shirt = 122, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 11, 12, 13 }, Type = "Jacket"},
                new Torso { ArmsUpper = 1, UnderShirt = 15, Shirt = 125, Textures = new List<int>() { 0 }, Type = "Jacket"},
                new Torso { ArmsUpper = 1, UnderShirt = 15, Shirt = 138, Textures = new List<int>() { 0, 1, 2 }, Type = "Jacket"},
                new Torso { ArmsUpper = 6, UnderShirt = 15, Shirt = 224, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 11, 12, 13, 14, 15 }, Type = "Jacket"},
                new Torso { ArmsUpper = 1, UnderShirt = 15, Shirt = 230, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 11 }, Type = "Jacket" }
                #endregion
            };

            FemaleTorso = new List<Torso>
            {
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },
                new Torso { ArmsUpper = 0, UnderShirt = 15, Shirt = 0, Textures = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 8, 11 }, Type = "Tee" },

            };
        }
        private void GeneratePants()
        {
            MalePants = new List<Pants>
            {
                new Pants { Bottoms = 0, Textures = new List<int>{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 }, Type = "Jeans" },
                new Pants { Bottoms = 1, Textures = new List<int>{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 }, Type = "Jeans" },
                new Pants { Bottoms = 3, Textures = new List<int>{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 }, Type = "Sweat" },
                new Pants { Bottoms = 4, Textures = new List<int>{ 0, 1, 2, 4, }, Type = "Jeans" },
                new Pants { Bottoms = 5, Textures = new List<int>{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 }, Type = "Sweat" },
                new Pants { Bottoms = 6, Textures = new List<int>{ 0, 1, 2, 10 }, Type = "Shorts" },
                new Pants { Bottoms = 7, Textures = new List<int>{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 }, Type = "Cargo" },
                new Pants { Bottoms = 8, Textures = new List<int>{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 }, Type = "Cargo" },
                new Pants { Bottoms = 9, Textures = new List<int>{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 }, Type = "Cargo" },
                new Pants { Bottoms = 10, Textures = new List<int>{ 0, 1, 2 }, Type = "Suit" },
                new Pants { Bottoms = 12, Textures = new List<int>{ 0, 4, 5, 7, 12 }, Type = "Shorts" },
                new Pants { Bottoms = 13, Textures = new List<int>{ 0, 1, 2, }, Type = "Suit" },
                new Pants { Bottoms = 14, Textures = new List<int>{ 0, 1, 3, 12 }, Type = "Shorts" },
                new Pants { Bottoms = 15, Textures = new List<int>{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 }, Type = "Shorts" },
                new Pants { Bottoms = 16, Textures = new List<int>{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 }, Type = "Shorts" },
                new Pants { Bottoms = 17, Textures = new List<int>{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 }, Type = "Shorts" },
                new Pants { Bottoms = 18, Textures = new List<int>{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 }, Type = "Shorts" },
                new Pants { Bottoms = 20, Textures = new List<int>{ 0, 1, 2, 3 }, Type = "Suit" },
                new Pants { Bottoms = 22, Textures = new List<int>{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 }, Type = "Suit" },
                new Pants { Bottoms = 23, Textures = new List<int>{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 }, Type = "Suit" },
                new Pants { Bottoms = 24, Textures = new List<int>{ 0, 1, 2, 3, 4, 5, 6 }, Type = "Suit" },
                new Pants { Bottoms = 25, Textures = new List<int>{ 0, 1, 2, 3, 4, 5, 6 }, Type = "Suit" },
                new Pants { Bottoms = 26, Textures = new List<int>{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 }, Type = "Jeans" },
                new Pants { Bottoms = 27, Textures = new List<int>{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 }, Type = "Cargo" },
                new Pants { Bottoms = 28, Textures = new List<int>{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 }, Type = "Suit" },
                new Pants { Bottoms = 31, Textures = new List<int>{ 0, 1, 2, 3, 4 }, Type = "Cargo" },
                new Pants { Bottoms = 33, Textures = new List<int>{ 0 }, Type = "Cargo" },
                new Pants { Bottoms = 34, Textures = new List<int>{ 0 }, Type = "Combat" },
                new Pants { Bottoms = 35, Textures = new List<int>{ 0 }, Type = "Suit" },
                new Pants { Bottoms = 37, Textures = new List<int>{ 0, 1, 2, 3 }, Type = "Suit" },
                new Pants { Bottoms = 42, Textures = new List<int>{ 0, 1, 2, 3, 4, 5, 6, 7 }, Type = "Shorts" },
                new Pants { Bottoms = 43, Textures = new List<int>{ 0, 1 }, Type = "Jeans" },
                new Pants { Bottoms = 45, Textures = new List<int>{ 0, 1, 2, 3, 4, 5, 6 }, Type = "Suit" },
                new Pants { Bottoms = 46, Textures = new List<int>{ 0, 1 }, Type = "Combat" },
                new Pants { Bottoms = 47, Textures = new List<int>{ 0, 1 }, Type = "Cargo" },
                new Pants { Bottoms = 48, Textures = new List<int>{ 0, 1, 2, 3, 4 }, Type = "Suit" },
                new Pants { Bottoms = 49, Textures = new List<int>{ 0, 1, 2, 3, 4 }, Type = "Suit" },
                new Pants { Bottoms = 50, Textures = new List<int>{ 0, 1, 2, 3 }, Type = "Suit" },
                new Pants { Bottoms = 51, Textures = new List<int>{ 0 }, Type = "Suit" },
                new Pants { Bottoms = 52, Textures = new List<int>{ 0, 1, 2, 3 }, Type = "Suit" },
                new Pants { Bottoms = 53, Textures = new List<int>{ 0 }, Type = "Suit" },
                new Pants { Bottoms = 54, Textures = new List<int>{ 0, 1, 2, 3, 4, 5, 6 }, Type = "Shorts" },
                new Pants { Bottoms = 55, Textures = new List<int>{ 0, 1, 2, 3 }, Type = "Sweats" },
                new Pants { Bottoms = 59, Textures = new List<int>{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, Type = "Cargo" },
                new Pants { Bottoms = 61, Textures = new List<int>{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 }, Type = "Misc" }, // Underwear
                new Pants { Bottoms = 62, Textures = new List<int>{ 0, 1, 2, 3 }, Type = "Shorts" },
                new Pants { Bottoms = 63, Textures = new List<int>{ 0 }, Type = "Jeans" },
                new Pants { Bottoms = 64, Textures = new List<int>{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, Type = "Sweats" },
                new Pants { Bottoms = 65, Textures = new List<int>{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 }, Type = "Suit" },
                new Pants { Bottoms = 69, Textures = new List<int>{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 }, Type = "Sweats" },
                new Pants { Bottoms = 71, Textures = new List<int>{ 0, 1, 2, 3, 4, 5 }, Type = "Leather" },
                new Pants { Bottoms = 73, Textures = new List<int>{ 0, 1, 2, 3, 4, 5 }, Type = "Leather" },
                new Pants { Bottoms = 75, Textures = new List<int>{ 0, 1, 2, 3, 4, 5, 6, 7 }, Type = "Jeans" },
                new Pants { Bottoms = 76, Textures = new List<int>{ 0, 1, 2, 3, 4, 5, 6, 7 }, Type = "Jeans" },
                new Pants { Bottoms = 78, Textures = new List<int>{ 0, 1, 2, 3, 4, 5, 6, 7 }, Type = "Sweats" },
                new Pants { Bottoms = 79, Textures = new List<int>{ 0, 1, 2 }, Type = "Leather" },
                new Pants { Bottoms = 80, Textures = new List<int>{ 0, 1, 2, 3, 4, 5, 6, 7 }, Type = "Sweats"  },
                new Pants { Bottoms = 81, Textures = new List<int>{ 0, 1, 2 }, Type = "Leather" },
                new Pants { Bottoms = 82, Textures = new List<int>{ 0, 1, 2, 3, 4, 5, 6, 7, 8 }, Type = "Jeans" },
                new Pants { Bottoms = 83, Textures = new List<int>{ 0, 1, 2, 3 }, Type = "Leather" },
                new Pants { Bottoms = 86, Textures = new List<int>{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 }, Type = "Cargo" },
                new Pants { Bottoms = 88, Textures = new List<int>{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 }, Type = "Cargo" },
                new Pants { Bottoms = 98, Textures = new List<int>{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25 }, Type = "Cargo" },
                new Pants { Bottoms = 102, Textures = new List<int>{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 }, Type = "Cargo" },
                new Pants { Bottoms = 103, Textures = new List<int>{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 }, Type = "Cargo" },
                new Pants { Bottoms = 105, Textures = new List<int>{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 }, Type = "Misc." }
            };
        }
    }
}
