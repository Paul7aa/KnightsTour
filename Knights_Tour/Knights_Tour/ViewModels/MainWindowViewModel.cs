using Knights_Tour.BaseModels;
using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Knights_Tour.Commands;
using Knights_Tour.Models;

namespace Knights_Tour.ViewModels
{
    public partial class MainWindowViewModel : BaseViewModel
    {
        private int m_chessBoardSize = 8;
        private String m_rowHelperText = "1 - 8";
        private String m_columnHelperText = "A - H";
        private String m_executeButtonContent = "START";
        private SolidColorBrush m_executeButtonColor = Brushes.DarkGreen;
        private Boolean isExecuting = false;

        private CellModel[][] m_cells = new CellModel[8][8]; 

        public int ChessBoardSize
        {
            get => m_chessBoardSize;
            set
            {
                m_chessBoardSize = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(RowHelperText));
                OnPropertyChanged(nameof(ColumnHelperText));
            }
        }

        public String RowHelperText
        {
            get => "1 - " + m_chessBoardSize.ToString();
            set
            {
                m_rowHelperText = value;
                OnPropertyChanged();
            }
        }

        public String ColumnHelperText
        {
            get => "A - " + ((char)(m_chessBoardSize + 64)).ToString();
            set
            {
                m_columnHelperText = value;
                OnPropertyChanged();
            }
        }

        public String ExecuteButtonContent
        {
            get => (!isExecuting) ? "START" : "STOP";
            set
            {
                m_executeButtonContent = value;
                OnPropertyChanged();
            }
        }

        public SolidColorBrush ExecuteButtonColour
        {
            get => (!isExecuting) ? Brushes.LimeGreen : Brushes.Red;
            set
            {
                ExecuteButtonColour = value;
                OnPropertyChanged();
            }
        }

        public Boolean IsExecuting
        {
            get => isExecuting;
            set
            {
                isExecuting = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ExecuteButtonContent));
                OnPropertyChanged(nameof(ExecuteButtonColour));
            }
        }
    }
}
