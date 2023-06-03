/*using System;
using ClientGameState = Hubsson.Hackathon.Arcade.Client.Dotnet.Domain.ClientGameState;

namespace Hubsson.Hackathon.Arcade.Client.Dotnet.Services
{
    public class MatchService
    {
        private MatchRepository _matchRepository;
        private ArcadeSettings _arcadeSettings;
        
        public MatchService(ArcadeSettings settings)
        {
            _matchRepository = new MatchRepository();
            _arcadeSettings = settings;
        }
        
        public void Init()
        {
            // On Game Init
            throw new NotImplementedException();
        }

        public Hubsson.Hackathon.Arcade.Client.Dotnet.Domain.Action Update(ClientGameState gameState)
        {
            // On Each Frame Update return an Action for your player

            throw new NotImplementedException();
        }

        private class MatchRepository
        {
            // Write your data fields here what you would like to store between the match rounds
        }
    }
}*/

/*using System;
using System.Collections.Generic;
using ClientGameState = Hubsson.Hackathon.Arcade.Client.Dotnet.Domain.ClientGameState;

namespace Hubsson.Hackathon.Arcade.Client.Dotnet.Services
{
    public class MatchService
    {
        private MatchRepository _matchRepository;
        private ArcadeSettings _arcadeSettings;

        public MatchService(ArcadeSettings settings)
        {
            _matchRepository = new MatchRepository();
            _arcadeSettings = settings;
        }

        public void Init()
        {
            // On Game Init
            // Implement your initialization logic here
            // This method is called when the game starts
            // You can initialize any necessary data or resources for the match
            // For example:
            _matchRepository.Reset(); // Reset the match data
        }

        public Hubsson.Hackathon.Arcade.Client.Dotnet.Domain.Action Update(ClientGameState gameState)
        {
            // On Each Frame Update return an Action for your player
            // Implement your game logic here
            // This method is called on each frame update of the game
            // You should process the current game state and return an appropriate action for your player

            // Example game logic:
            if (gameState != null && gameState.Properties is Dictionary<string, object> properties)
            {
                if (properties.TryGetValue("Health", out var healthObj) && healthObj is int health)
                {
                    if (health < 50)
                    {
                        // Perform healing action
                        return new Hubsson.Hackathon.Arcade.Client.Dotnet.Domain.Action
                        {
                            ActionType = "Heal"
                        };
                    }
                    else
                    {
                        // Perform attacking action
                        return new Hubsson.Hackathon.Arcade.Client.Dotnet.Domain.Action
                        {
                            ActionType = "Attack"
                        };
                    }
                }
            }

            // Default action if the game state is invalid or missing necessary properties
            return new Hubsson.Hackathon.Arcade.Client.Dotnet.Domain.Action
            {
                ActionType = "Default"
            };
        }

        private class MatchRepository
        {
            // Write your data fields here that you would like to store between match rounds
            private int _roundCount;

            public void Reset()
            {
                // Implement the reset logic to clear or initialize the match data
                _roundCount = 0;
            }
        }
    }
}*/

/*using System;
using System.Collections.Generic;
using System.Linq;

namespace Hubsson.Hackathon.Arcade.Client.Dotnet.Services
{
    public class MatchService
    {
        private List<List<TileType>>? _map;
        private Position? _playerPosition;

        public List<Action> FindBestRoute()
        {
            var route = new List<Action>();
            var visited = new HashSet<Position>();

            if (_map == null || _playerPosition == null)
                return route;

            var queue = new Queue<Position>();
            var parentMap = new Dictionary<Position, Position>();
            queue.Enqueue(_playerPosition);
            visited.Add(_playerPosition);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                if (_map[current.Row][current.Column] == TileType.Finish)
                {
                    route = GetRouteToDestination(parentMap, current);
                    break;
                }

                var neighbors = GetNeighbors(current);

                foreach (var neighbor in neighbors)
                {
                    if (!visited.Contains(neighbor))
                    {
                        queue.Enqueue(neighbor);
                        visited.Add(neighbor);
                        parentMap[neighbor] = current;
                    }
                }
            }

            return route;
        }

        private List<Action> GetRouteToDestination(Dictionary<Position, Position> parentMap, Position destination)
        {
            var route = new List<Action>();
            var current = destination;

            while (parentMap.ContainsKey(current))
            {
                var parent = parentMap[current];
                var direction = GetDirection(parent, current);
                route.Add(direction);
                current = parent;
            }

            route.Reverse();
            return route;
        }

        private List<Position> GetNeighbors(Position position)
        {
            var neighbors = new List<Position>();

            var directions = new List<(int, int)>
            {
                (-1, 0), // Up
                (1, 0),  // Down
                (0, -1), // Left
                (0, 1)   // Right
            };

            foreach (var direction in directions)
            {
                var newRow = position.Row + direction.Item1;
                var newColumn = position.Column + direction.Item2;

                if (IsValidPosition(newRow, newColumn) && _map![newRow][newColumn] != TileType.Wall)
                {
                    neighbors.Add(new Position(newRow, newColumn));
                }
            }

            return neighbors;
        }

        private bool IsValidPosition(int row, int column)
        {
            return row >= 0 && row < _map!.Count && column >= 0 && column < _map![row].Count;
        }

        private Action GetDirection(Position source, Position destination)
        {
            if (source.Row < destination.Row)
                return Action.MoveDown;
            if (source.Row > destination.Row)
                return Action.MoveUp;
            if (source.Column < destination.Column)
                return Action.MoveRight;
            if (source.Column > destination.Column)
                return Action.MoveLeft;

            throw new Exception("Invalid direction.");
        }
    }

    public enum TileType
    {
        Empty,
        Wall,
        Start,
        Finish
    }

    public enum Action
    {
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight
    }

    public class Position
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public Position(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}*/

/*using System;                                                                     2nd code
using System.Collections.Generic;
using System.Linq;

namespace Hubsson.Hackathon.Arcade.Client.Dotnet.Services
{
    public class MatchService
    {
        private List<List<TileType>>? _map;
        private Position? _playerPosition;
        private int _score;

        public List<Action> FindBestRoute()
        {
            var route = new List<Action>();
            var visited = new HashSet<Position>();

            if (_map == null || _playerPosition == null)
                return route;

            var queue = new Queue<Position>();
            var parentMap = new Dictionary<Position, Position>();
            queue.Enqueue(_playerPosition);
            visited.Add(_playerPosition);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                if (_map[current.Row][current.Column] == TileType.Finish)
                {
                    route = GetRouteToDestination(parentMap, current);
                    break;
                }

                var neighbors = GetNeighbors(current);

                foreach (var neighbor in neighbors)
                {
                    if (!visited.Contains(neighbor))
                    {
                        queue.Enqueue(neighbor);
                        visited.Add(neighbor);
                        parentMap[neighbor] = current;
                    }
                }
            }

            return route;
        }

        private List<Action> GetRouteToDestination(Dictionary<Position, Position> parentMap, Position destination)
        {
            var route = new List<Action>();
            var current = destination;

            while (parentMap.ContainsKey(current))
            {
                var parent = parentMap[current];
                var direction = GetDirection(parent, current);
                route.Add(direction);
                current = parent;
            }

            route.Reverse();
            return route;
        }

        private List<Position> GetNeighbors(Position position)
        {
            var neighbors = new List<Position>();

            var directions = new List<(int, int)>
            {
                (-1, 0), // Up
                (1, 0),  // Down
                (0, -1), // Left
                (0, 1)   // Right
            };

            foreach (var direction in directions)
            {
                var newRow = position.Row + direction.Item1;
                var newColumn = position.Column + direction.Item2;

                if (IsValidPosition(newRow, newColumn) && _map![newRow][newColumn] != TileType.Wall)
                {
                    neighbors.Add(new Position(newRow, newColumn));
                }
            }

            return neighbors;
        }

        private bool IsValidPosition(int row, int column)
        {
            return row >= 0 && row < _map!.Count && column >= 0 && column < _map![row].Count;
        }

        private Action GetDirection(Position source, Position destination)
        {
            if (source.Row < destination.Row)
                return Action.MoveDown;
            if (source.Row > destination.Row)
                return Action.MoveUp;
            if (source.Column < destination.Column)
                return Action.MoveRight;
            if (source.Column > destination.Column)
                return Action.MoveLeft;

            throw new Exception("Invalid direction.");
        }

        public void SetMap(List<List<TileType>> map)
        {
            _map = map;
        }

        public void SetPlayerPosition(Position playerPosition)
        {
            _playerPosition = playerPosition;
        }

        public void SetScore(int score)
        {
            _score = score;
        }

        public int GetScore()
        {
            return _score;
        }
    }

    public enum TileType
    {
        Empty,
        Wall,
        Start,
        Finish
    }

    public enum Action
    {
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight
    }

    public class Position
    {
        public int Row { get; }
        public int Column { get; }

        public Position(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }

    public class LeaderboardService
    {
        private List<(string, int)> _scores = new List<(string, int)>();

        public void AddScore(string playerName, int score)
        {
            _scores.Add((playerName, score));
        }

        public void DisplayLeaderboard()
        {
            Console.WriteLine("Leaderboard:");

            var sortedScores = _scores.OrderByDescending(x => x.Item2).ToList();
            for (var i = 0; i < sortedScores.Count; i++)
            {
                var score = sortedScores[i];
                Console.WriteLine($"{i + 1}. {score.Item1}: {score.Item2}");
            }
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var leaderboardService = new LeaderboardService();
            var matchService = new MatchService();

            // Set up the map and player position
            var map = new List<List<TileType>>
            {
                new List<TileType> { TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty },
                new List<TileType> { TileType.Empty, TileType.Wall, TileType.Empty, TileType.Empty },
                new List<TileType> { TileType.Start, TileType.Empty, TileType.Empty, TileType.Finish },
                new List<TileType> { TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty },
            };
            var playerPosition = new Position(2, 0);
            matchService.SetMap(map);
            matchService.SetPlayerPosition(playerPosition);

            // Calculate the best route
            var bestRoute = matchService.FindBestRoute();

            // Update the score
            var score = bestRoute.Count;
            matchService.SetScore(score);

            // Display the leaderboard
            leaderboardService.AddScore("Player 1", matchService.GetScore());
            leaderboardService.AddScore("Player 2", 15);
            leaderboardService.AddScore("Player 3", 8);
            leaderboardService.DisplayLeaderboard();
        }
    }
}*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace Hubsson.Hackathon.Arcade.Client.Dotnet.Services
{
    public class MatchService
    {
        private List<List<TileType>>? _map;
        private Position? _playerPosition;
        private int _score;

        private List<EnemyNPC> _enemyNPCs;
        private List<PowerUp> _powerUps;

        public MatchService()
        {
            _enemyNPCs = new List<EnemyNPC>();
            _powerUps = new List<PowerUp>();
        }

        public List<Action> FindBestRoute()
        {
            var route = new List<Action>();
            var visited = new HashSet<Position>();

            if (_map == null || _playerPosition == null)
                return route;

            var queue = new Queue<Position>();
            var parentMap = new Dictionary<Position, Position>();
            queue.Enqueue(_playerPosition);
            visited.Add(_playerPosition);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                if (_map[current.Row][current.Column] == TileType.Finish)
                {
                    route = GetRouteToDestination(parentMap, current);
                    break;
                }

                var neighbors = GetNeighbors(current);

                foreach (var neighbor in neighbors)
                {
                    if (!visited.Contains(neighbor))
                    {
                        queue.Enqueue(neighbor);
                        visited.Add(neighbor);
                        parentMap[neighbor] = current;
                    }
                }
            }

            return route;
        }

        private List<Action> GetRouteToDestination(Dictionary<Position, Position> parentMap, Position destination)
        {
            var route = new List<Action>();
            var current = destination;

            while (parentMap.ContainsKey(current))
            {
                var parent = parentMap[current];
                var direction = GetDirection(parent, current);
                route.Add(direction);
                current = parent;
            }

            route.Reverse();
            return route;
        }

        private List<Position> GetNeighbors(Position position)
        {
            var neighbors = new List<Position>();

            var directions = new List<(int, int)>
            {
                (-1, 0), // Up
                (1, 0),  // Down
                (0, -1), // Left
                (0, 1)   // Right
            };

            foreach (var direction in directions)
            {
                var newRow = position.Row + direction.Item1;
                var newColumn = position.Column + direction.Item2;

                if (IsValidPosition(newRow, newColumn) && _map![newRow][newColumn] != TileType.Wall)
                {
                    neighbors.Add(new Position(newRow, newColumn));
                }
            }

            return neighbors;
        }

        private bool IsValidPosition(int row, int column)
        {
            return row >= 0 && row < _map!.Count && column >= 0 && column < _map![row].Count;
        }

        private Action GetDirection(Position source, Position destination)
        {
            if (source.Row < destination.Row)
                return Action.MoveDown;
            if (source.Row > destination.Row)
                return Action.MoveUp;
            if (source.Column < destination.Column)
                return Action.MoveRight;
            if (source.Column > destination.Column)
                return Action.MoveLeft;

            throw new Exception("Invalid direction.");
        }

        public void SetMap(List<List<TileType>> map)
        {
            _map = map;
        }

        public void SetPlayerPosition(Position playerPosition)
        {
            _playerPosition = playerPosition;
        }

        public void SetScore(int score)
        {
            _score = score;
        }

        public int GetScore()
        {
            return _score;
        }

        public List<EnemyNPC> GetEnemyNPCs()
        {
            return _enemyNPCs;
        }

        public List<PowerUp> GetPowerUps()
        {
            return _powerUps;
        }

        public void GenerateEnemyNPCs(int count)
        {
            var random = new Random();

            for (int i = 0; i < count; i++)
            {
                var row = random.Next(_map!.Count);
                var column = random.Next(_map[row].Count);

                var enemyNPC = new EnemyNPC(new Position(row, column));
                _enemyNPCs.Add(enemyNPC);
            }
        }

        public void GeneratePowerUps(int count)
        {
            var random = new Random();

            for (int i = 0; i < count; i++)
            {
                var row = random.Next(_map!.Count);
                var column = random.Next(_map[row].Count);

                var powerUp = new PowerUp(new Position(row, column));
                _powerUps.Add(powerUp);
            }
        }

        // Additional feature: Handle collisions with enemy NPCs
        public bool HandleCollisionsWithEnemies()
        {
            foreach (var enemyNPC in _enemyNPCs)
            {
                if (enemyNPC.Position.Equals(_playerPosition))
                {
                    // Player collided with an enemy NPC
                    // Implement collision logic here
                    return true;
                }
            }

            return false;
        }

        // Additional feature: Handle collecting power-ups
        public bool HandlePowerUpCollection()
        {
            foreach (var powerUp in _powerUps)
            {
                if (powerUp.Position.Equals(_playerPosition))
                {
                    // Player collected a power-up
                    // Implement power-up collection logic here
                    _powerUps.Remove(powerUp);
                    return true;
                }
            }

            return false;
        }
    }

    public enum TileType
    {
        Empty,
        Wall,
        Start,
        Finish,
        PowerUp // Added PowerUp tile type
    }

    public enum Action
    {
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight
    }

    public class Position
    {
        public int Row { get; }
        public int Column { get; }

        public Position(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }

    // Additional feature: Power-up class
    public class PowerUp
    {
        public Position Position { get; }

        public PowerUp(Position position)
        {
            Position = position;
        }
    }

    // Additional feature: Enemy NPC class
    public class EnemyNPC
    {
        public Position Position { get; }

        public EnemyNPC(Position position)
        {
            Position = position;
        }
    }

    public class LeaderboardService
    {
        private List<(string, int)> _scores = new List<(string, int)>();

        public void AddScore(string playerName, int score)
        {
            _scores.Add((playerName, score));
        }

        public void DisplayLeaderboard()
        {
            Console.WriteLine("Leaderboard:");

            var sortedScores = _scores.OrderByDescending(x => x.Item2).ToList();
            for (var i = 0; i < sortedScores.Count; i++)
            {
                var score = sortedScores[i];
                Console.WriteLine($"{i + 1}. {score.Item1}: {score.Item2}");
            }
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var leaderboardService = new LeaderboardService();
            var matchService = new MatchService();

            // Set up the map and player position
            var map = new List<List<TileType>>
            {
                new List<TileType> { TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty },
                new List<TileType> { TileType.Empty, TileType.Wall, TileType.Empty, TileType.Empty },
                new List<TileType> { TileType.Start, TileType.Empty, TileType.Empty, TileType.Finish },
                new List<TileType> { TileType.Empty, TileType.Empty, TileType.Empty, TileType.Empty },
            };
            var playerPosition = new Position(2, 0);
            matchService.SetMap(map);
            matchService.SetPlayerPosition(playerPosition);

            // Additional features: Generate enemy NPCs and power-ups
            matchService.GenerateEnemyNPCs(3);
            matchService.GeneratePowerUps(5);

            // Calculate the best route
            var bestRoute = matchService.FindBestRoute();

            // Update the score
            var score = bestRoute.Count;
            matchService.SetScore(score);

            // Additional features: Handle collisions and power-up collection
            if (matchService.HandleCollisionsWithEnemies())
            {
                Console.WriteLine("Game Over! You collided with an enemy NPC.");
            }
            else if (matchService.HandlePowerUpCollection())
            {
                Console.WriteLine("Power-up collected!");
            }

            // Display the leaderboard
            leaderboardService.AddScore("Player 1", matchService.GetScore());
            leaderboardService.AddScore("Player 2", 15);
            leaderboardService.AddScore("Player 3", 8);
            leaderboardService.DisplayLeaderboard();
        }
    }
}









