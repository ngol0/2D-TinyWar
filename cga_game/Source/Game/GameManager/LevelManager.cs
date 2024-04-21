using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input.Touch;
using System.Reflection.Metadata;
using System.IO.Pipes;

namespace Strategy
{
    class LevelInfo
    {
        public int timeToSpawn;
        public int laneNumber;

        public LevelInfo(int laneNumber, int timeToSpawn)
        {  
            this.timeToSpawn = timeToSpawn; 
            this.laneNumber = laneNumber; 
        }
    }
    class LevelManager
    {
        int currentLevel = 0;
        public List<LevelInfo> levelInfos = new List<LevelInfo>();
        public void LoadTextFile()
        {
            // Load the level.
            string levelPath = string.Format("Content/level/{0}.txt", currentLevel);
            using (Stream fileStream = TitleContainer.OpenStream(levelPath))
            {
                ParseInfo(fileStream);
            }
        }

        public void ParseInfo(Stream fileStream)
        {
            using (StreamReader reader = new StreamReader(fileStream))
            {
                // Read each line from the file
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    // Split the line by comma to get lane and time
                    string[] parts = line.Split(',');

                    // Parse lane and time
                    if (parts.Length == 2 && int.TryParse(parts[0], out int lane) && int.TryParse(parts[1], out int time))
                    {
                        // Create a new LevelInfo object and add it to the list
                        LevelInfo enemyInfo = new LevelInfo(lane, time);
                        levelInfos.Add(enemyInfo);
                    }
                    else
                    {
                        // Handle invalid line format
                        throw new Exception("Invalid line format in the file.");
                    }
                }
            }
        }
    }
}
