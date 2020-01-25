using System;
using System.IO;

namespace Source
{
    static class FileIO
    {
        // Instantiate a FileStream object.
        //FileStream fileStream = new FileStream("idleSaveGame.txt",FileMode.Open,FileAccess.Read,FileShare.Read);
        static BinaryWriter writerObj;
        static BinaryReader readerObj;

        // Game saving method.
        static public void SaveGame()
        {
            //create the file
            try
            {
                writerObj = new BinaryWriter(new FileStream("savegame.idle", FileMode.Create));

                // Write points bank.
                writerObj.Write(Program.gamePoints.value);
                writerObj.Write(Program.gamePoints.echelon);

                // Loop through every agent.
                for (int i = 0; i < 10; i++)
                {
                    writerObj.Write(Program.agentObjsArr[i].count.value);
                    writerObj.Write(Program.agentObjsArr[i].count.echelon);
                }

                // Loop through every upgrade.
                for (int i = 0; i < 10; i++)
                {
                    writerObj.Write(Program.upgraObjsArr[i].count.value);
                    writerObj.Write(Program.upgraObjsArr[i].count.echelon);
                }

                // Finish writing.
                writerObj.Flush();
                writerObj.Close();
            }
            catch (IOException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message + "Failed to save the game.");

                return;
            }
        }

        // Game loading method.
        static public void LoadGame()
        {
            try
            {
                readerObj = new BinaryReader(new FileStream("savegame.idle", FileMode.Open));

                // Load the points bank.
                Program.gamePoints.value = readerObj.ReadDouble();
                Program.gamePoints.echelon = readerObj.ReadInt32();

                // Loop through every agent.
                for (int i = 0; i < 10; i++)
                {
                    Program.agentObjsArr[i].count.value = readerObj.ReadDouble();
                    Program.agentObjsArr[i].count.echelon = readerObj.ReadInt32();
                }

                // Loop through every upgrade.
                for (int i = 0; i < 10; i++)
                {
                    Program.upgraObjsArr[i].count.value = readerObj.ReadDouble();
                    Program.upgraObjsArr[i].count.echelon = readerObj.ReadInt32();
                }

                readerObj.Close();

                Program.UpdateConsole();
            }
            catch (IOException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message + "\n Failed to load the game.");
                return;
            }
        }
    }
}
