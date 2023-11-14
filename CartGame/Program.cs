using System;
using System.Data;
using static System.Console;

namespace CardGame
{
    enum CardValue
    {
        six=1,
        seven,
        eight,
        nine,
        ten,
        jack,
        queen,
        king,
        ace
    }
   class PlayingCard
   {
        public string Suit { get; set; } = string.Empty;
        public CardValue Value { get; set; }

        public static implicit operator PlayingCard(CardValue x)
        { return new PlayingCard { Value = x }; }
        public override string ToString()
        {
            return $"\tКарта: ({Value} - {Suit})";
        }
    }
    class Player
    {
        public string Name { get; set; } = string.Empty;

        public int IdPlayer { get; set; } 


        List<PlayingCard>_setOfCards = new List<PlayingCard>();
       

        public List<PlayingCard> SetOfCards
        {
            get { return _setOfCards; }
            set { _setOfCards = value; }
        }
        public Player(string name, List<PlayingCard> setOfCards)
        {
            Name = name;
            _setOfCards = setOfCards;
        }
        public void ShowSet()
        {
            WriteLine($"Игрок {Name}\nЕго карты:");
            foreach (var item in _setOfCards)
                WriteLine(item);
        }
    }
    class Game
    {
        List<PlayingCard> _allCards = new() {
             new PlayingCard {Value = CardValue.six, Suit = "Diamonds"},
             new PlayingCard {Value = CardValue.seven, Suit = "Diamonds"},
             new PlayingCard {Value = CardValue.eight, Suit = "Diamonds "},
             new PlayingCard {Value = CardValue.nine, Suit = "Diamonds"},
             new PlayingCard {Value = CardValue.ten, Suit = "Diamonds"},
             new PlayingCard {Value = CardValue.jack, Suit = "Diamonds"},
             //new PlayingCard {Value = CardValue.queen, Suit = "Diamonds"},
             //new PlayingCard {Value = CardValue.king, Suit = "Diamonds"},
             //new PlayingCard {Value = CardValue.ace, Suit = "Diamonds"},
             //new PlayingCard {Value = CardValue.six, Suit = "Hearts"},
             //new PlayingCard {Value = CardValue.seven, Suit = "Hearts"},
             //new PlayingCard {Value = CardValue.eight, Suit = "Hearts"},
             //new PlayingCard {Value = CardValue.nine, Suit = "Hearts"},
             //new PlayingCard {Value = CardValue.ten, Suit = "Hearts"},
             //new PlayingCard {Value = CardValue.jack, Suit = "Hearts"},
             //new PlayingCard {Value = CardValue.queen, Suit = "Hearts"},
             //new PlayingCard {Value = CardValue.king, Suit = "Hearts"},
             //new PlayingCard {Value = CardValue.ace, Suit = "Hearts"},
             //new PlayingCard {Value = CardValue.six, Suit = "Clubs"},
             //new PlayingCard {Value = CardValue.seven, Suit = "Clubs"},
             //new PlayingCard {Value = CardValue.eight, Suit = "Clubs"},
             //new PlayingCard {Value = CardValue.nine, Suit = "Clubs"},
             //new PlayingCard {Value = CardValue.ten, Suit = "Clubs"},
             //new PlayingCard {Value = CardValue.jack, Suit = "Clubs"},
             //new PlayingCard {Value = CardValue.queen, Suit = "Clubs"},
             //new PlayingCard {Value = CardValue.king, Suit = "Clubs"},
             //new PlayingCard {Value = CardValue.ace, Suit = "Clubs"},
             //new PlayingCard {Value = CardValue.six, Suit = "Spades"},
             //new PlayingCard {Value = CardValue.seven, Suit = "Spades"},
             //new PlayingCard {Value = CardValue.eight, Suit = "Spades"},
             //new PlayingCard {Value = CardValue.nine, Suit = "Spades"},
             //new PlayingCard {Value = CardValue.ten, Suit = "Spades"},
             //new PlayingCard {Value = CardValue.jack, Suit = "Spades"},
             //new PlayingCard {Value = CardValue.queen, Suit = "Spades"},
             //new PlayingCard {Value = CardValue.king, Suit = "Spades"},
             //new PlayingCard {Value = CardValue.ace, Suit = "Spades"},
            };

        private int _numberOfPlayers;

        public int NumberOfPlayers
        {
            get { return _numberOfPlayers; }
            set
            { 
                if(value<2||36%value!=0)
                    throw new ArgumentException(String.Format($"Некорректное количество игроков!"));
                else
                    _numberOfPlayers = value;
            }
        }
        private List<Player> _players = new();
        public List<Player> Players
        {
            get { return _players; }
            set { _players = value; }
        }
        public void Shuffling()
        {
            Random r = new();
            for (int i = _allCards.Count - 1; i>0; i--)
            {
                int j = r.Next(i);
                PlayingCard ?tmp = null;
                tmp = _allCards[i];
                _allCards[i] = _allCards[j];
                _allCards[j]=tmp;
            }
        }
        public void Dealing()
        {
            int fateOfCards = _allCards.Count / NumberOfPlayers;
            List<PlayingCard>forOnePlayer = new(fateOfCards);
            for (int i = 0; i < NumberOfPlayers; i++)
            {
                WriteLine($"Введите имя игрока под номером {i + 1}:");

                Players.Add(new Player(ReadLine(), forOnePlayer = _allCards.GetRange(i * fateOfCards, fateOfCards)));
                Players[i].IdPlayer = i;
            }
        }
        private List<PlayingCard> OnTable(List<Player> players)
        {
            List<PlayingCard> cardOnTable = new List<PlayingCard>();
                  for (int i = 0; i < players.Count; i++)
                  {
                    if (players[i].SetOfCards.Count > 0)
                    {
                        cardOnTable.Add(players[i].SetOfCards[0]);

                        players[i].SetOfCards.RemoveAt(0);
                    }
                  }
                    return cardOnTable;
        }
        public void GameOn()
        {
            
            List<PlayingCard> cardOnTable = OnTable(Players);
            int idWinner = -1;
            for (int i = 0; i < cardOnTable.Count; i++)
            {
                if (cardOnTable[i].Value == cardOnTable.Max(player => player.Value))
                {
                    idWinner = i;
                    break;
                }
            }
            Players[idWinner].SetOfCards.AddRange(cardOnTable);
            for (int i = 0; i < Players.Count; i++)
            {
                if(Players[i].SetOfCards.Count == 0)
                {
                    Players.Remove(Players[i]);
                }
            }
          }
    }
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                try
                {
                    Game game = new();
                    WriteLine("Введите количество игроков или ENTER для выхода из программы: ");
                    string str = ReadLine();
                    if (string.IsNullOrEmpty(str))
                        return;
                    game.NumberOfPlayers = int.Parse(str);
                    game.Shuffling();
                    game.Dealing();
                    Clear();
                    WriteLine("\t\tПервоночальная раздача:");
                    foreach (var item in game.Players)
                    {
                        item.ShowSet();
                    }
                    WriteLine("*********************************************");
                    int count = 0;
                    while (game.Players.Count > 1)
                    {
                        game.GameOn();
                        WriteLine("__________________________________________");
                        foreach (var item in game.Players)
                            item.ShowSet();
                        count++;
                        if (count == 100)
                        {
                            break;
                        }
                    }
                    if (game.Players.Count > 1)
                    {
                        int winner = 0;
                        int max = 0;
                        for (int i = 0; i < game.Players.Count; i++)
                        {
                            if(game.Players[i].SetOfCards.Count>max)
                            {
                                max = game.Players[i].SetOfCards.Count;
                                winner = i;
                            }
                        }
                        WriteLine($"\n\t\tДостигнуто максимальное количество ходов: {count}!!!\n\n" +
                             $"\tПоэтому победителем становится игрок под именем: {game.Players[winner].Name}\n"+
                             $"\tТак как собрал больше всех карт: {max}\n\n");
                    }    
                    else
                        WriteLine($"\t\tПобедитель игрок под именем: {game.Players[0].Name}" +
                            $"\n\tКоличество ходов: {count}\n\n");

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } while (true);
            
        }
    }
}
