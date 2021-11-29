using JogoDaVelha.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoDaVelha.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public RelayCommand GameViewCommand { get; set; }
        public GameViewModel GameVm { get; set; }

        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set 
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            GameVm = new GameViewModel();
            CurrentView = GameVm;

           GameViewCommand = new RelayCommand(o =>
           {
               CurrentView = GameVm;
           });
        }
    }
}
