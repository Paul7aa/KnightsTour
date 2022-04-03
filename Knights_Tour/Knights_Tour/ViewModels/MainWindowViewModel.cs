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
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Threading;

namespace Knights_Tour.ViewModels
{
    public partial class MainWindowViewModel : BaseViewModel
    {
        private int m_chessBoardSize = 8;
        private String m_rowHelperText = "";
        private String m_columnHelperText = "";
        private String m_executeButtonContent = "";
        private Boolean m_isExecuting = false;
        private SolidColorBrush m_executeButtonColor = Brushes.DarkGreen;
        private CellCollectionModel m_cellCollection;
        private WarnsdorffAlgorithmModel m_warnsdorffAlgorithm;
        public DateTime startTime = DateTime.Now;
        public DispatcherTimer m_timeElapsed;
        public string m_timeElapsedString = "0";
        public KnightModel m_knight;
        public String m_knightX = "8";
        public String m_knightY = "A";

        public KnightModel Knight
        {
            get
            {
                if (m_knight == null)
                    m_knight = new KnightModel(new Point(7,0));
                return m_knight;
            }
            set
            {
                m_knight = value;
                OnPropertyChanged();
            }
        }

        public String KnightX
        {
            get => m_knightX;
            set
            {
                m_knightX = value;
                if (validRow(m_knightX))
                {
                    Knight.SetPreviousPosition();
                    Knight.CurrentPosition.X = Int32.Parse(m_knightX) - 1;
                }
                else
                {
                    Knight.SetPreviousPosition();
                    Knight.CurrentPosition.X = -1;
                }
                OnPropertyChanged();
            }
        }

        public String KnightY
        {
            get => m_knightY;
            set
            {
                m_knightY = value;
                if (validColumn(m_knightY))
                {
                    Knight.SetPreviousPosition();
                    Knight.CurrentPosition.Y = Char.Parse(m_knightY.ToUpper()) - 65;
                }
                else
                {
                    Knight.SetPreviousPosition();
                    Knight.CurrentPosition.Y = -1;
                }
                OnPropertyChanged();
            }
        }

        public WarnsdorffAlgorithmModel WarnsdorffAlgorithm
        {
            get
            {
                if (m_warnsdorffAlgorithm == null)
                    m_warnsdorffAlgorithm = new WarnsdorffAlgorithmModel(8);
                return m_warnsdorffAlgorithm;
            }
            set
            {
                m_warnsdorffAlgorithm = value;
                OnPropertyChanged();
            }

        }

        public DispatcherTimer TimeElapsed
        {
            get
            {
                if (m_timeElapsed == null)
                {
                    m_timeElapsed = new DispatcherTimer();
                    m_timeElapsed.Interval = new TimeSpan(0, 0, 0, 0, 1);
                    m_timeElapsed.Tick += timeElapsedTick;
                }
                return m_timeElapsed;
            }
            set
            {
                m_timeElapsed = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TimeElapsedString));
            }
        }

        public string TimeElapsedString
        {
            get => m_timeElapsedString;
            set
            {
                m_timeElapsedString = value;
                OnPropertyChanged();
            }
        }
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
                if (m_cellCollection == null || m_cellCollection.Size != ChessBoardSize)
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
