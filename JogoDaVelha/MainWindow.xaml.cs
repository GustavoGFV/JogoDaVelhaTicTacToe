using JogoDaVelha.Methods;
using JogoDaVelha.MVVM.View;
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

namespace JogoDaVelha
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        PlayersData playerData;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void PlayBtn(object sender, RoutedEventArgs e)
        {
            MenuBrd.IsEnabled = false;
            ChoosePopUpBrd.Visibility = Visibility.Visible;
        }

        private void ExitBtn(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ChooseBtn(object sender, RoutedEventArgs e)
        {
            if(sender != null)
            {
                Button bt = (Button)sender;
                bt.IsEnabled = false;
                playerData = new PlayersData();
                playerData.PlayersDatas(bt.Content.ToString());

                GameView gm = new GameView();
                gm.GameViewSetValues(playerData.Player1, playerData.Player2);
                ChoosePopUpBrd.Visibility = Visibility.Hidden;
                MenuBrd.Visibility = Visibility.Hidden;
                TitleTxtBlock.Visibility = Visibility.Hidden;
                GameContentControl.Visibility = Visibility.Visible;
            }
            else
            {
                ChoosP1Lbl.Content = "Player 1, Escolha seu símbolo.";
            }
        }
    }
}
