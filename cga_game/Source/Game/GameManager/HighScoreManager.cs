using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Strategy
{
    class HighScoreManager
    {
        private const string PATH = "stats.json";
        public PlayerStat currentPlayerStat;
        public void Save(PlayerStat playerStat)
        {
            if (playerStat.Score > currentPlayerStat.Score)
            {
                currentPlayerStat.Score = playerStat.Score;
                string serializedText = JsonSerializer.Serialize<PlayerStat>(currentPlayerStat);
                Trace.WriteLine(serializedText);
                File.WriteAllText(PATH, serializedText);
            }
        }

        public void Load() 
        {
            var fileContent = File.ReadAllText(PATH);
            if (fileContent == "") 
            {
                currentPlayerStat = new PlayerStat();
            }
            else currentPlayerStat = JsonSerializer.Deserialize<PlayerStat>(fileContent);
        }
    }

    public class PlayerStat
    {
        public int Score { get; set; }
    }
}
