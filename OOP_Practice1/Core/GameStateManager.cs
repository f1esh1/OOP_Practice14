using System.IO;
using System.Text.Json;

namespace OOP_Practice1.Core
{
    public class GameStateData
    {
        public string PlayerName { get; set; }
        public int PlayerHealth { get; set; }
        public int EnemyHealth { get; set; }
        public int CurrentTurn { get; set; }
    }

    public static class GameStateManager
    {
        private const string SaveFilePath = "savegame.json";

        public static void SaveGame(GameStateData state)
        {
            string jsonString = JsonSerializer.Serialize(state, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(SaveFilePath, jsonString);
        }

        public static GameStateData LoadGame()
        {
            if (!File.Exists(SaveFilePath))
            {
                return null;
            }

            string jsonString = File.ReadAllText(SaveFilePath);
            return JsonSerializer.Deserialize<GameStateData>(jsonString);
        }
    }
}