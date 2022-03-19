using Knights_Tour.BaseModels;
using Knights_Tour.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knights_Tour.ViewModels
{
    public partial class MainWindowViewModel : BaseViewModel
    {
        private int m_chessBoardSize;

        public MainWindowViewModel()
        {
            ChessBoardSize = 8;
        }


        public int ChessBoardSize
        {
            get => m_chessBoardSize;
            set
            {
                m_chessBoardSize = value;
                OnPropertyChanged();
            }
        }
    }
}
