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
        private String m_rowHelperText = "";
        private String m_columnHelperText = "";
        private String m_executeButtonContent = "";
        private SolidColorBrush m_executeButtonColor = Brushes.DarkGreen;
        private Boolean m_isExecuting = false;
        private CellCollectionModel m_cellCollection = null;

        public int ChessBoardSize
        {
            get => m_chessBoardSize;
            set
            {
                m_chessBoardSize = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(RowHelperText));
                OnPropertyChanged(nameof(ColumnHelperText));
                OnPropertyChanged(nameof(CellCollection));
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
            get => (!m_isExecuting) ? "START" : "STOP";
            set
            {
                m_executeButtonContent = value;
                OnPropertyChanged();
            }
        }

        public SolidColorBrush ExecuteButtonColour
        {
            get => (!m_isExecuting) ? Brushes.LimeGreen : Brushes.Red;
            set
            {
                ExecuteButtonColour = value;
                OnPropertyChanged();
            }
        }

        public Boolean IsExecuting
        {
            get => m_isExecuting;
            set
            {
                m_isExecuting = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ExecuteButtonContent));
                OnPropertyChanged(nameof(ExecuteButtonColour));
            }
        }

        public CellCollectionModel CellCollection{
            get
            {
                if (m_cellCollection == null)
                    m_cellCollection = new CellCollectionModel(ChessBoardSize);
                return m_cellCollection;
            }
            set
            {
                m_cellCollection = value;
                OnPropertyChanged();
            }
        }
    }
}
