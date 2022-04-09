using Knights_Tour.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Knights_Tour.ViewModels
{
    public partial class MainWindowViewModel
    {
        private ICommand m_startAlgorithmCommand;
        private ICommand m_restartTourCommand;
        private ICommand m_cleanChessBoard;
        private ICommand m_changeSpeedCommand;
        public ICommand StartAlgorithmCommand
        {
            get
            {
                if (m_startAlgorithmCommand == null)
                    return new AsyncRelayCommand(async (_) => await StartAlgorithmOnClick(),
                        (_) => CanStartAlgorithm() && !NeedsReset);
                return m_startAlgorithmCommand;
                
            }
        }

        public ICommand RestartTourCommand
        {
            get
            {
                if (m_restartTourCommand == null)
                    return new RelayCommand((_) => RestartTourOnClick(), (_) => NeedsReset); ;
                return m_startAlgorithmCommand;

            }
        }

        public ICommand CleanChessBoardCommand
        {
            get
            {
                if (m_cleanChessBoard == null)
                    return new RelayCommand((_) => CleanChessBoardOnClick(), (_) => NeedsReset); 
                return m_cleanChessBoard;
            }
        }

        public ICommand ChangeSpeedCommand
        {
            get
            {
                if (m_changeSpeedCommand == null)
                    return new AsyncRelayCommand((_) => ChangeSpeedOnClick(), (_) => true);
                return m_changeSpeedCommand;
            }
        }
    }
}
