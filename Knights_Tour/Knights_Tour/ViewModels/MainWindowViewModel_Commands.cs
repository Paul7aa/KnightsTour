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

        public ICommand StartAlgorithmCommand
        {
            get
            {
                if (m_startAlgorithmCommand == null)
                    return new RelayCommand((_) => StartAlgorithm(), (_) => true);
                return m_startAlgorithmCommand;
                
            }
        }
    }
}
