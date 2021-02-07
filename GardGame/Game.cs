using Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame
{
    public class Game
    {
        public CardSet Table { get; }
        public List<Player> Players { get; }
        public CardSet Deck { get; }

        private Player activePlayer;
        public Player ActivePlayer
        {
            get => activePlayer;
            set
            {
                activePlayer = value;
                foreach (Player player in Players)
                {
                    if (player == activePlayer)
                        player.HandCards.Show();
                    else
                        player.HandCards.Hide();
                }
                MarkActivePlayer(activePlayer);
            }
        }

        public Action<string> ShowMessage;
        public Action<Player> MarkActivePlayer;
        public Action<CardSet> ShowCards;

        public Game(Action<string> showMessage,
            Action<Player> markActivePlayer,
            Action<CardSet> showCards,
            params Player[] players)
        {
            ShowMessage = showMessage;
            MarkActivePlayer = markActivePlayer;
            ShowCards = showCards;
            Table = GetCardSet();
            Players = new List<Player>(players);
            Deck = GetCardSet();
            Deck.Full();

        }

        public void Start()
        {
            Deal();
            ActivePlayer = Players[0];
            Refresh();
        }

        public virtual CardSet GetCardSet()
        {
            return new CardSet();
        }

        public void Move(Card card)
        {
            if (!ActivePlayer.HandCards.Cards.Contains(card)) return;

            Table.Add(ActivePlayer.HandCards.Pull(card));
            ActivePlayer = NextPlayer(ActivePlayer);
            Refresh();
        }

        public void Refresh()
        {
            foreach (var player in Players)
            {
                ShowCards(player.HandCards);
            }
            ShowCards(Table);
            
        }

        private Player NextPlayer(Player player)
        {
            if (player == Players[Players.Count - 1]) return Players[0];

            return Players[Players.IndexOf(player) + 1];
        }

        private Player PreviousPlayer(Player player)
        {
            if (player == Players[0]) return Players[Players.Count - 1];

            return Players[Players.IndexOf(player) - 1];
        }

        public void Deal()
        {
            Deck.Mix();
            foreach (var player in Players)
            {
                player.HandCards.Add(Deck.Deal(6));
            }
            Refresh();
        }
        public void GameOver()
        {
            foreach (var player in Players)
            {
                if (player.HandCards.Cards.Count != 0)
                    ShowMessage(player.Name + "loose");
            }
        }

        public void HangUp()
        {
            Table.Cards.Clear();
            Refresh();
        }
    }
}
