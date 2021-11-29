using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using JogoDaVelha.Enum;
using JogoDaVelha.Interfaces.Players;
using JogoDaVelha.Methods;
using JogoDaVelha.Constantes;

namespace JogoDaVelha.MVVM.View
{
    /// <summary>
    /// Interação lógica para GameView.xam
    /// </summary>
    public partial class GameView : UserControl
    {
        PlayersData playerData;

        private const string P1 = "Player 1";
        private const string P2 = "Player 2";

        private int Player1Wins = 0;
        private int Player2Wins = 0;
        private int Draw = 0;

        private static int P1choosen = 0;
        private static int P2choosen = 0;
        public GameView()
        {
            InitializeComponent();
            ResetValues();
        }
        public void GameViewSetValues(int p1 = 0, int p2 = 0)
        {
            P1choosen = p1;
            P2choosen = p2;
        }

        private void BtnClick(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            if (PlayerTurnTxtB.Text.Contains(P1))
            {
                checkBtn(btn, P1);
            }
            else
            {
                checkBtn(btn, P2);
            }
        }

        private async void checkBtn(Button btn, string Player)
        {
            if (btn == null)
                throw new Exception("Seleção Inesperada");

            if (btn.Content == null)
            {
                btn.IsEnabled = false;
                switch (Player)
                {
                    case P1:
                        if (P1choosen == (int)ChooseEnum.O)
                        {
                            btn.Content = ChooseEnum.O.ToString();
                            btn.Foreground = Brushes.OrangeRed;
                        }
                        else
                        {
                            btn.Content = ChooseEnum.X.ToString();
                            btn.Foreground = Brushes.LightSeaGreen;
                        }
                        break;
                    case P2:
                        if (P2choosen == (int)ChooseEnum.O)
                        {
                            btn.Content = ChooseEnum.O.ToString();
                            btn.Foreground = Brushes.OrangeRed;
                        }
                        else
                        {
                            btn.Content = ChooseEnum.X.ToString();
                            btn.Foreground = Brushes.LightSeaGreen;
                        }
                        break;

                }
                await CheckStatus();
                PlayerTurnTxtB.Text = Player == P1 ? string.Format(TextDictionary.Turno, P2) : string.Format(TextDictionary.Turno, P1);
            }
        }
        private async Task CheckStatus()
        {
            var horizon = HorizontalCheck();
            var vertical = VerticalCheck();
            var diagonal = DiagonalCheck();
            var gameOver = false;

            if (!horizon && !vertical && !diagonal)
            {
                if (!A1btn.IsEnabled &&
                    !A2btn.IsEnabled &&
                    !A3btn.IsEnabled &&
                    !B1btn.IsEnabled &&
                    !B2btn.IsEnabled &&
                    !B3btn.IsEnabled &&
                    !C1btn.IsEnabled &&
                    !C2btn.IsEnabled &&
                    !C3btn.IsEnabled)
                {
                    gameOver = true;
                }
            }
            ValidateWinner(horizon, vertical, diagonal, gameOver);
        }

        #region Validação dos Quadrados
        private bool HorizontalCheck()
        {
            if (A1btn.Content?.ToString() == A2btn.Content?.ToString() && A1btn.Content?.ToString() == A3btn.Content?.ToString() && !A1btn.IsEnabled && !A2btn.IsEnabled && !A3btn.IsEnabled ||
                B1btn.Content?.ToString() == B2btn.Content?.ToString() && B1btn.Content?.ToString() == B3btn.Content?.ToString() && !B1btn.IsEnabled && !B2btn.IsEnabled && !B3btn.IsEnabled ||
                C1btn.Content?.ToString() == C2btn.Content?.ToString() && C1btn.Content?.ToString() == C3btn.Content?.ToString() && !C1btn.IsEnabled && !C2btn.IsEnabled && !C3btn.IsEnabled)
            {
                return true;
            }
            return false;
        }
        private bool VerticalCheck()
        {
            if (A1btn.Content?.ToString() == B1btn.Content?.ToString() && A1btn.Content?.ToString() == C1btn.Content?.ToString() && !A1btn.IsEnabled && !B1btn.IsEnabled && !C1btn.IsEnabled ||
                A2btn.Content?.ToString() == B2btn.Content?.ToString() && A2btn.Content?.ToString() == C2btn.Content?.ToString() && !A2btn.IsEnabled && !B2btn.IsEnabled && !C2btn.IsEnabled ||
                A3btn.Content?.ToString() == B3btn.Content?.ToString() && A3btn.Content?.ToString() == C3btn.Content?.ToString() && !A3btn.IsEnabled && !B3btn.IsEnabled && !C3btn.IsEnabled)
            {
                return true;
            }
            return false;
        }
        private bool DiagonalCheck()
        {
            if (A1btn.Content?.ToString() == B2btn.Content?.ToString() && A1btn.Content?.ToString() == C3btn.Content?.ToString() && !A1btn.IsEnabled && !B2btn.IsEnabled && !C3btn.IsEnabled ||
                A3btn.Content?.ToString() == B2btn.Content?.ToString() && A3btn.Content?.ToString() == C1btn.Content?.ToString() && !A3btn.IsEnabled && !B2btn.IsEnabled && !C1btn.IsEnabled)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Validação se houve vencedor no jogo
        private void ValidateWinner(bool horizon, bool vertical, bool diagonal, bool gameOver)
        {
            if (gameOver)
            {
                GamePanel.IsEnabled = false;
                EndGameBrd.Visibility = Visibility.Visible;
                RestartDisplayLbl.Text = "Deu Velha!";
                Draw++;
                DrawLbl.Content = string.Format(TextDictionary.Empate, Draw);
            }
            if (horizon || vertical || diagonal)
            {
                GamePanel.IsEnabled = false;
                if (PlayerTurnTxtB.Text.Contains(P1))
                {
                    RestartDisplayLbl.Text = string.Format(TextDictionary.Vitoria, P1);
                    Player1Wins++;
                    Player1Score.Text = String.Format(TextDictionary.P1Score, Player1Wins, Player2Wins);
                    Player2Score.Text = String.Format(TextDictionary.P2Score, Player2Wins, Player1Wins);
                    P1WinsLbl.Content = string.Format(TextDictionary.ContadorVitoria, Player1Wins);
                    P2LosesLbl.Content = string.Format(TextDictionary.ContadorDerrotas, Player1Wins);
                }
                else
                {
                    RestartDisplayLbl.Text = string.Format(TextDictionary.Vitoria, P2);
                    Player2Wins++;
                    Player1Score.Text = String.Format(TextDictionary.P1Score, Player1Wins, Player2Wins);
                    Player2Score.Text = String.Format(TextDictionary.P2Score, Player2Wins, Player1Wins);
                    P1LosesLbl.Content = string.Format(TextDictionary.ContadorDerrotas, Player2Wins);
                    P2WinsLbl.Content = string.Format(TextDictionary.ContadorVitoria, Player2Wins);
                }
                EndGameBrd.Visibility = Visibility.Visible;
            }
        }
        #endregion


        #region Area PopUp Fim de Jogo
        private void EndGameExitBtn(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();

            mw.Show();

            Window.GetWindow(this).Close();
        }

        private void EndGameContinueBtn(object sender, RoutedEventArgs e)
        {
            ResetBtn();
            EndGameBrd.Visibility = Visibility.Hidden;

            Xbtn.IsEnabled = true;
            Obtn.IsEnabled = true;
            ChoosePopUpBrd.Visibility = Visibility.Visible;
        }

        private void EndGameRestartBtn(object sender, RoutedEventArgs e)
        {
            EndGameBrd.Visibility = Visibility.Hidden;

            Xbtn.IsEnabled = true;
            Obtn.IsEnabled = true;
            ChoosePopUpBrd.Visibility = Visibility.Visible;
            ResetBtn();
            ResetValues();
        }
        #endregion

        #region PopUp escolher simbolo
        private void ChooseBtn(object sender, RoutedEventArgs e)
        {
            Xbtn.IsEnabled = true;
            Obtn.IsEnabled = true;
            if (sender != null)
            {
                Button bt = (Button)sender;
                bt.IsEnabled = false;
                playerData = new PlayersData();
                playerData.PlayersDatas(bt.Content.ToString());

                (P1choosen, P2choosen) = (playerData.Player1, playerData.Player2);

                ChoosePopUpBrd.Visibility = Visibility.Hidden;
                GamePanel.IsEnabled = true;
            }
            else
            {
                ChoosP1Lbl.Content = "Player 1, Escolha seu símbolo.";
            }
        }
        #endregion

        #region Reiniciar Valores
        private void ResetBtn()
        {
            A1btn.Content = null;
            A1btn.IsEnabled = true;
            A2btn.Content = null;
            A2btn.IsEnabled = true;
            A3btn.Content = null;
            A3btn.IsEnabled = true;
            B1btn.Content = null;
            B1btn.IsEnabled = true;
            B2btn.Content = null;
            B2btn.IsEnabled = true;
            B3btn.Content = null;
            B3btn.IsEnabled = true;
            C1btn.Content = null;
            C1btn.IsEnabled = true;
            C2btn.Content = null;
            C2btn.IsEnabled = true;
            C3btn.Content = null;
            C3btn.IsEnabled = true;

            PlayerTurnTxtB.Text = string.Format(TextDictionary.Turno, P1);
        }
        private void ResetValues()
        {
            Player1Wins = 0;
            Player2Wins = 0;
            Draw = 0;
            DrawLbl.Content = string.Format(TextDictionary.Empate, Draw);
            P1WinsLbl.Content = string.Format(TextDictionary.ContadorVitoria, Player1Wins);
            P2WinsLbl.Content = string.Format(TextDictionary.ContadorVitoria, Player2Wins);
            P1LosesLbl.Content = string.Format(TextDictionary.ContadorDerrotas, Player2Wins);
            P2LosesLbl.Content = string.Format(TextDictionary.ContadorDerrotas, Player1Wins);
            PlayerTurnTxtB.Text = string.Format(TextDictionary.Turno, P1);
        }
        #endregion
    }
}
