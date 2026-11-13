using System;
using OOP_Practice1.Core.GameSystem;

namespace OOP_Practice1.Core
{
    public class GameEngine
    {
        private Character _player;
        private Character _enemy;
        private GameLog _gameLog;
        private int _currentTurn;

        public GameEngine()
        {
            _gameLog = new GameLog();
            _currentTurn = 1;
        }

        public void Start()
        {
            Console.Clear();
            Console.WriteLine("=== TURN-BASED TEXT GAME ===");

            _player = new Warrior("Hero Warrior");
            _enemy = new Warrior("Wild Goblin");

            _gameLog.AddEntry($"Game started! {_player.Name} vs {_enemy.Name}.");

            MainGameLoop();
        }

        private void MainGameLoop()
        {
            bool exit = false;

            while (!exit && _player.Health > 0 && _enemy.Health > 0)
            {
                Console.WriteLine($"\n--- TURN #{_currentTurn} ---");
                Console.WriteLine($"Player: {_player.Name} [HP: {_player.Health}/{_player.MaxHealth}]");
                Console.WriteLine($"Enemy: {_enemy.Name} [HP: {_enemy.Health}/{_enemy.MaxHealth}]");
                Console.WriteLine("\nChoose an action:");
                Console.WriteLine("1. Attack enemy");
                Console.WriteLine("2. View turn history");
                Console.WriteLine("3. Save game");
                Console.WriteLine("4. Load game");
                Console.WriteLine("5. Exit to menu");
                Console.Write("Your choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        _player.Attack(_enemy);
                        string playerAction = $"{_player.Name} attacked {_enemy.Name}!";
                        _gameLog.AddEntry(playerAction);

                        if (_enemy.Health <= 0)
                        {
                            Console.WriteLine("\nVictory! You defeated the enemy!");
                            _gameLog.AddEntry($"{_player.Name} won the battle!");
                            exit = true;
                            break;
                        }

                        _enemy.Attack(_player);
                        string enemyAction = $"{_enemy.Name} counterattacked!";
                        _gameLog.AddEntry(enemyAction);

                        if (_player.Health <= 0)
                        {
                            Console.WriteLine("\nYou died... Game over.");
                            _gameLog.AddEntry($"{_player.Name} died in battle.");
                            exit = true;
                        }

                        _currentTurn++;
                        break;

                    case "2":
                        Console.WriteLine("\n--- TURN HISTORY ---");
                        foreach (var entry in _gameLog.GetHistory())
                        {
                            Console.WriteLine($"> {entry}");
                        }
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;

                    case "3":
                        var state = new GameStateData
                        {
                            PlayerName = _player.Name,
                            PlayerHealth = _player.Health,
                            EnemyHealth = _enemy.Health,
                            CurrentTurn = _currentTurn
                        };
                        GameStateManager.SaveGame(state);
                        Console.WriteLine("Game successfully saved!");
                        _gameLog.AddEntry("Game saved.");
                        break;

                    case "4":
                        var loadedState = GameStateManager.LoadGame();
                        if (loadedState != null)
                        {
                            _player.Health = loadedState.PlayerHealth;
                            _enemy.Health = loadedState.EnemyHealth;
                            _currentTurn = loadedState.CurrentTurn;
                            Console.WriteLine("Game successfully loaded!");
                            _gameLog.AddEntry("Game loaded from file.");
                        }
                        else
                        {
                            Console.WriteLine("Save file not found!");
                        }
                        break;

                    case "5":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            }

            Console.WriteLine("\nPress any key to exit to the main menu...");
            Console.ReadKey();
        }
    }
}