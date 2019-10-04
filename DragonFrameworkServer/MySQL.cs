using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.IO;
using Newtonsoft.Json;
using System.Dynamic;
using System.Runtime.InteropServices;

namespace DragonFrameworkServer
{
    class MySQL : BaseScript
    {
        string connStr;

        public class ConnData
        {
            public string server { get; set; }
            public string user { get; set; }
            public string database { get; set; }
            public string port { get; set; }
            public string password { get; set; }
        }
        public class CharData
        {
            public List<string[]> Character { get; set; }
        }
        public class Data
        {
            public int Money { get; set; }
            public float Xpos { get; set; }
            public float Ypos { get; set; }
            public float Zpos { get; set; }
            public float Heading { get; set; }
        }

        public void GetConnData()
        {
            using (StreamReader r = new StreamReader("./resources/DragonFramework/config.json"))
            {
                string json = r.ReadToEnd();

                ConnData connData = JsonConvert.DeserializeObject<ConnData>(json);

                connStr = $"server={connData.server};user={connData.user};database={connData.database};port={connData.port};password={connData.password}";

            }
        }
        public void GetPlayerData(Player connectingPlayer)
        {
            CharData charData = new CharData();
            charData.Character = new List<string[]>();

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"SELECT * FROM PlayerData WHERE Licence = @searchitem", conn);
                cmd.Parameters.AddWithValue("@searchitem", connectingPlayer.Identifiers["License"]);

                cmd.Prepare();

                using (MySqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        charData.Character.Add(new string[] {
                            rdr.GetString("Name"), rdr.GetString("Gender"), rdr.GetString("OverlayData"), rdr.GetString("FaceshapeData"), rdr.GetString("ComponentData"),
                            rdr.GetString("XPos"), rdr.GetString("YPos"), rdr.GetString("ZPOS"), rdr.GetString("Heading"), rdr.GetString("Money"), rdr.GetString("PersonalVehicle"),
                        });
                    }
                }
            }

            string Json = JsonConvert.SerializeObject(charData);

            connectingPlayer.TriggerEvent("dFrame:MainMenu", Json);
        }

        public void SaveNewPed(Player source, string name, string gender, string overlaydata, string facedata, string componentdata, float xpos, float ypos, float zpos, float heading, int money, string personalvehicle)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"INSERT INTO `PlayerData` (`Licence`, `Name`, `Gender`, `OverlayData`, `FaceshapeData`, `ComponentData`, `XPos`, `YPos`, `ZPos`, `Heading`, `Money`, `PersonalVehicle`) VALUES " +
                    $"(@Licence, @Name, @Gender, @OverlayData, @FaceshapeData, @ComponentData, @Xpos, @Ypos, @Zpos, @Heading, @Money, 'a');", conn);

                cmd.Parameters.AddWithValue("@Licence", source.Identifiers["License"]);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Gender", gender);
                cmd.Parameters.AddWithValue("@OverlayData", overlaydata);
                cmd.Parameters.AddWithValue("@FaceshapeData", facedata);
                cmd.Parameters.AddWithValue("@ComponentData", componentdata);
                cmd.Parameters.AddWithValue("@Xpos", xpos);
                cmd.Parameters.AddWithValue("@Ypos", ypos);
                cmd.Parameters.AddWithValue("@Zpos", zpos);
                cmd.Parameters.AddWithValue("@Heading", heading);
                cmd.Parameters.AddWithValue("@Money", money);

                cmd.Prepare();

                cmd.ExecuteNonQuery();
            }
        }
        public void SavePlayer(Player source, string json)
        {
            Data PlayerData = JsonConvert.DeserializeObject<Data>(json);

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"UPDATE `PlayerData` SET `XPos` = @Xpos, `YPos` = @Ypos, `ZPos` = @Zpos, `Heading` = @Heading, `Money` = @Money WHERE `Licence` = @Licence", conn);

                cmd.Parameters.AddWithValue("@Licence", source.Identifiers["License"]);
                cmd.Parameters.AddWithValue("@Xpos", PlayerData.Xpos);
                cmd.Parameters.AddWithValue("@Ypos", PlayerData.Ypos);
                cmd.Parameters.AddWithValue("@Zpos", PlayerData.Zpos);
                cmd.Parameters.AddWithValue("@Heading", PlayerData.Heading);
                cmd.Parameters.AddWithValue("@Money", PlayerData.Money);

                cmd.Prepare();

                cmd.ExecuteNonQuery();
            }
        }
    }
}
