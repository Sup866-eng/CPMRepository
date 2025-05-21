using System;
using System.Collections.Generic;

namespace BlackjackPr
{
    public class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Start();
        }
    }

    public class Card
    {
        public int Value { get; private set; }

        public Card(int value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }

    public class Player
    {
        public string Name { get; private set; }
        public List<Card> Hand;
        public bool HasStopped = false;

        public int SwapOneCardTokens = 1;
        public int SwapDeckTokens = 1;

        public event Action<string> OnCardDrawn;
        public event Action<string> OnTokenUsed;
        public event Action<string> OnTurnStarted;

        public Player(string name, List<Card> startingHand)
        {
            Name = name;
            Hand = startingHand;
        }

        public int GetHandSum()
        {
            int sum = 0;
            foreach (Card card in Hand)
            {
                sum += card.Value;
            }
            return sum;
        }

        public void DrawCard(List<Card> commonDeck)
        {
            if (commonDeck.Count > 0)
            {
                Card card = commonDeck[0];
                commonDeck.RemoveAt(0);
                Hand.Add(card);
                if (OnCardDrawn != null)
                {
                    OnCardDrawn("" + Name + " added a card: " + card.Value);
                }
            }
        }

        public void UseSwapOneCardToken(Player opponent, List<Card> commonDeck)
        {
            if (SwapOneCardTokens <= 0)
            {
                Console.WriteLine("You can't use this token!!!");
                return;
            }

            Console.WriteLine(Name + ", choose your card to offer (0-" + (Hand.Count - 1) + "):");
            for (int i = 0; i < Hand.Count; i++)
            {
                Console.WriteLine(i + " - " + Hand[i].Value);
            }

            int ownIndex;
            if (!int.TryParse(Console.ReadLine(), out ownIndex) || ownIndex < 0 || ownIndex >= Hand.Count)
            {
                Console.WriteLine("Answer correct please.");
                return;
            }

            Console.WriteLine(opponent.Name + ", " + Name + " wants to change a card with. Choose one of your cards to exchange (0-" + (opponent.Hand.Count - 1) + "):");
            for (int i = 0; i < opponent.Hand.Count; i++)
            {
                Console.WriteLine(i + " - " + opponent.Hand[i].Value);
            }

            int oppIndex;
            if (!int.TryParse(Console.ReadLine(), out oppIndex) || oppIndex < 0 || oppIndex >= opponent.Hand.Count)
            {
                Console.WriteLine("Invalid index.");
                return;
            }

            Card temp = Hand[ownIndex];
            Hand[ownIndex] = opponent.Hand[oppIndex];
            opponent.Hand[oppIndex] = temp;

            SwapOneCardTokens--;
            if (OnTokenUsed != null)
            {
                OnTokenUsed(Name + " exchanged card " + temp.Value + " with " + opponent.Name + "'s card " + Hand[ownIndex].Value);
            }
        }

        public void UseSwapDeckToken(Player opponent)
        {
            if (SwapDeckTokens <= 0)
            {
                Console.WriteLine("You can't use this token anymore!!!");
                return;
            }

            List<Card> temp = Hand;
            Hand = opponent.Hand;
            opponent.Hand = temp;

            SwapDeckTokens--;
            if (OnTokenUsed != null)
            {
                OnTokenUsed(Name + " used Swap-Hand token with " + opponent.Name);
            }
        }

        public void StartTurn()
        {
            if (OnTurnStarted != null)
            {
                OnTurnStarted("--- " + Name + "'s turn ---");
            }
        }
    }

    public class Game
    {
        private List<Card> commonDeck;
        private List<Player> players = new List<Player>();
        private bool gameOver = false;
        private const int Goal = 60;

        public event Action<string> OnGameEvent;

        public void Start()
        {
            InitializeDeck();
            InitializePlayers();

            foreach (Player player in players)
            {
                player.OnCardDrawn += Report;
                player.OnTokenUsed += Report;
                player.OnTurnStarted += Report;
            }

            PlayGame();
        }

        private void InitializeDeck()
        {
            commonDeck = new List<Card>();
            for (int i = 1; i <= 10; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    commonDeck.Add(new Card(i));
                }
            }
            Shuffle(commonDeck);
        }

        private void InitializePlayers()
        {
            for (int i = 1; i <= 2; i++)
            {
                List<Card> hand = new List<Card>();
                for (int j = 0; j < 5; j++)
                {
                    hand.Add(commonDeck[0]);
                    commonDeck.RemoveAt(0);
                }

                players.Add(new Player("Player " + i, hand));
            }
        }

        private void Shuffle(List<Card> cards)
        {
            Random rand = new Random();
            for (int i = cards.Count - 1; i > 0; i--)
            {
                int j = rand.Next(i + 1);
                Card temp = cards[i];
                cards[i] = cards[j];
                cards[j] = temp;
            }
        }

        private void Report(string message)
        {
            Console.WriteLine(message);
            if (OnGameEvent != null)
            {
                OnGameEvent(message);
            }
        }

        private void PlayGame()
        {
            while (!gameOver)
            {
                foreach (Player player in players)
                {
                    if (gameOver)
                    {
                        break;
                    }
                    if (player.HasStopped)
                    {
                        continue;
                    }

                    player.StartTurn();
                    TakeTurn(player);
                    CheckAutoLose(player);
                    CheckEndGameCondition();
                }
            }
        }

        private void TakeTurn(Player currentPlayer)
        {
            Console.Write("Your hand: ");
            for (int i = 0; i < currentPlayer.Hand.Count; i++)
            {
                Console.Write("[" + i + ":" + currentPlayer.Hand[i] + "] ");
            }
            Console.WriteLine("(Total: " + currentPlayer.GetHandSum() + ")");

            Console.WriteLine("Choose an action:");
            Console.WriteLine("1 - Add a card");
            Console.WriteLine("2 - Use Swap-One-Card token");
            Console.WriteLine("3 - Use Swap-Hand token");
            Console.WriteLine("4 - Stop adding cards");

            string input = Console.ReadLine();
            if (input == "1")
            {
                if (!currentPlayer.HasStopped)
                {
                    currentPlayer.DrawCard(commonDeck);
                }
                else
                {
                    Console.WriteLine("You've already stopped and cannot add.");
                }
            }
            else if (input == "2")
            {
                ChooseOpponentAndUseToken(currentPlayer, "one");
            }
            else if (input == "3")
            {
                ChooseOpponentAndUseToken(currentPlayer, "deck");
            }
            else if (input == "4")
            {
                currentPlayer.HasStopped = true;
                Console.WriteLine(currentPlayer.Name + " has stopped adding cards.");
            }
        }

        private void ChooseOpponentAndUseToken(Player player, string tokenType)
        {
            Console.WriteLine("Choose opponent:");
            List<Player> opponents = new List<Player>();
            foreach (Player p in players)
            {
                if (p != player)
                {
                    opponents.Add(p);
                }
            }
            for (int i = 0; i < opponents.Count; i++)
            {
                Console.WriteLine(i + " - " + opponents[i].Name);
            }

            int index;
            if (!int.TryParse(Console.ReadLine(), out index) || index < 0 || index >= opponents.Count)
            {
                Console.WriteLine("Answer correct please.");
                return;
            }

            Player opponent = opponents[index];

            if (tokenType == "one")
            {
                player.UseSwapOneCardToken(opponent, commonDeck);
            }
            else
            {
                player.UseSwapDeckToken(opponent);
            }
        }

        private void CheckAutoLose(Player player)
        {
            int sum = player.GetHandSum();
            if (sum > Goal)
            {
                Console.WriteLine("\n" + player.Name + " got more than " + Goal + " and loses!");
                DeclareWinner(FindOpponent(player));
                gameOver = true;
            }
        }

        private Player FindOpponent(Player current)
        {
            foreach (Player p in players)
            {
                if (p != current)
                {
                    return p;
                }
            }
            return null;
        }

        private void CheckEndGameCondition()
        {
            if (gameOver)
            {
                return;
            }

            bool allStopped = true;
            foreach (Player p in players)
            {
                if (!p.HasStopped)
                {
                    allStopped = false;
                    break;
                }
            }

            if (allStopped)
            {
                Player winner = null;
                int best = -1;
                bool tie = false;

                foreach (Player p in players)
                {
                    int s = p.GetHandSum();
                    if (s <= Goal)
                    {
                        if (s > best)
                        {
                            best = s;
                            winner = p;
                            tie = false;
                        }
                        else if (s == best)
                        {
                            tie = true;
                        }
                    }
                }

                if (tie || winner == null)
                {
                    Console.WriteLine("\nIt's a tie!");
                    if (OnGameEvent != null)
                    {
                        OnGameEvent("The game ended in a tie.");
                    }
                }
                else
                {
                    DeclareWinner(winner);
                }

                gameOver = true;
            }
        }

        private void DeclareWinner(Player winner)
        {
            Console.WriteLine("\n" + winner.Name + " wins with a total of " + winner.GetHandSum() + "!");
            if (OnGameEvent != null)
            {
                OnGameEvent(winner.Name + " has won the game!");
            }
        }
    }
}
