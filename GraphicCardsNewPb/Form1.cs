﻿using CardGame;
using Cards;
using GraphicCards;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicCardsNewPb
{
    public partial class Form1 : Form
    {
        GraphicGame game;
        Card activeCard;
        public Form1()
        {
            InitializeComponent();
            game = new GraphicGame(
                pnlDeck,
                pnlTable,
                ShonInfo,
                MarkActive,
                ShowCards,
                GetPlayers());

            foreach (var card in game.Deck.Cards)
            {
                PictureBox pb = ((GraphicCard)card).Pb;
                pb.MouseClick += Card_Select;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            game.Move(activeCard);
            bMove.Visible = false;
        }

        private void Card_Select(object sender, MouseEventArgs e)
        {
            CardPictureBox pb = (CardPictureBox)sender;
            Card card = pb.Card;
            if (e.Button == MouseButtons.Left)
            {
                activeCard = card;
                pb.Top += 10;
                Panel playerPnl = ((GraphicCardSet)game.ActivePlayer.HandCards).Pnl;
                bMove.Location = new Point(playerPnl.Left + pb.Left+pb.Width,playerPnl.Top);
                bMove.Visible = true;
            }
            else if (e.Button == MouseButtons.Right)
            {
                pb.Top -= 10;
                activeCard = null;
                bMove.Visible = false;
            }
        }

        private Player[] GetPlayers()
        {
            return new Player[] 
            {
                new GraphicPlayer() { lblName = lP1, Name = "P1", HandCards = new GraphicCardSet(pnlP1) },
                new GraphicPlayer() { lblName = lP2, Name = "P2", HandCards = new GraphicCardSet(pnlP2) },
                new GraphicPlayer() { lblName = lP3, Name = "P3", HandCards = new GraphicCardSet(pnlP3) },
            };
        }

        private void ShowCards(CardSet cardSet)
        {
            var cards = cardSet as GraphicCardSet;
            cards.Draw();
        }

        private void MarkActive(Player player)
        {
            foreach (GraphicPlayer p in game.Players)
            {
                p.lblName.ForeColor = p == player ? Color.Red : Color.Black;
            }
        }

        private void ShonInfo(string message)
        {
            lInfo.Text = message;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            game.Start();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
