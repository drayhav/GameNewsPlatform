namespace GameService.Domain.ValueObjects
{
    public sealed class Genre : IEquatable<Genre>
    {
        public string Name { get; }

        private Genre(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("Genre's name cannot be empty");
            }

            //if (ListAll().Any(a => a.Name == name))
            //{
            //    throw new ArgumentException($"Genre {name} is not supported");
            //}

            Name = name;
        }

        public static readonly Genre Strategy = new("Strategy");
        public static readonly Genre Sports = new("Sports");
        public static readonly Genre FPS = new("FPS");
        public static readonly Genre RPG = new("RPG");
        public static readonly Genre Simulation = new("Simulation");
        public static readonly Genre CityBuilder = new("CityBuilder");
        public static readonly Genre Sandbox = new("Sandbox");
        public static readonly Genre Adventure = new("Adventure");
        public static readonly Genre Puzzle = new("Puzzle");
        public static readonly Genre Racing = new("Racing");
        public static readonly Genre Fighting = new("Fighting");
        public static readonly Genre Platformer = new("Platformer");
        public static readonly Genre Stealth = new("Stealth");
        public static readonly Genre Survival = new("Survival");
        public static readonly Genre Horror = new("Horror");
        public static readonly Genre BattleRoyale = new("BattleRoyale");
        public static readonly Genre MOBA = new("MOBA");
        public static readonly Genre MMORPG = new("MMORPG");
        public static readonly Genre Roguelike = new("Roguelike");
        public static readonly Genre CardGame = new("CardGame");
        public static readonly Genre BoardGame = new("BoardGame");
        public static readonly Genre Music = new("Music");
        public static readonly Genre Party = new("Party");
        public static readonly Genre Educational = new("Educational");
        public static readonly Genre Idle = new("Idle");
        public static readonly Genre VR = new("VR");

        public static IEnumerable<Genre> ListAll() => new[]
        {
                Strategy, Sports, FPS, RPG, Simulation, CityBuilder, Sandbox, Adventure, Puzzle, Racing,
                Fighting, Platformer, Stealth, Survival, Horror, BattleRoyale, MOBA, MMORPG, Roguelike,
                CardGame, BoardGame, Music, Party, Educational, Idle, VR
            };

        public override bool Equals(object? obj)
        {
            return obj is Genre genre && Name == genre.Name;
        }

        public bool Equals(Genre? other)
        {
            return other is not null && Name == other.Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }

        public static implicit operator Genre(string name)
        {
            return new Genre(name);
        }

        public static explicit operator string(Genre genre)
        {
            if (genre is null)
                throw new ArgumentNullException(nameof(genre), "Genre cannot be null.");

            return genre.Name;
        }
    }
}