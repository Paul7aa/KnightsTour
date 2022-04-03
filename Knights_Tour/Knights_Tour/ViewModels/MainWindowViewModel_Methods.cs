using Knights_Tour.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Knights_Tour.ViewModels
{
    public partial class MainWindowViewModel
    {
        public async Task StartAlgorithm()
        {
            IsExecuting = !IsExecuting;
            if (IsExecuting)
            {
                //for (int i = 0; i < CellCollection.Size; i++)
                //{
                //    for(int j = 0; j < CellCollection.Size; j++)
                //    {
                //        CellCollection.Cells[i, j].CellState = Models.cellState.visited;
                //        CellCollectionModel auxCellCollection = new CellCollectionModel(CellCollection);
                //        CellCollection = auxCellCollection;
                //        await Task.Delay(500);
                //    }

                //}


                
                m_warnsdorffAlgorithm = new WarnsdorffAlgorithmModel(ChessBoardSize);
                startTime = DateTime.Now;
                TimeElapsed.Start();
                await Task.Run(() => m_warnsdorffAlgorithm.GetSolution());
                TimeElapsed.Stop();
                int[,] solutionTour = m_warnsdorffAlgorithm.SolutionTour;
                IsExecuting = false;
            }
        }

        public void timeElapsedTick(object sender, EventArgs e)
        {
            TimeElapsedString = Convert.ToString((DateTime.Now - startTime).TotalMilliseconds);
            CommandManager.InvalidateRequerySuggested();
        }

        public bool CanStartAlgorithm()
        {
            bool ChessBoardSizeValid = (ChessBoardSize > 4 && ChessBoardSize < 17);

            bool knightXValid = (Knight.CurrentPosition.X >= 0 && Knight.CurrentPosition.X < ChessBoardSize);

            bool KnightYValid = (Knight.CurrentPosition.Y >= 0 && Knight.CurrentPosition.Y < ChessBoardSize);

            return ChessBoardSizeValid && knightXValid && KnightYValid; 
        }

        public bool validRow(string row)
        {
            int intRow = 0;
            bool parseSuccesful = Int32.TryParse(row, out intRow);
            if (parseSuccesful)
            {
                if (intRow > 0 && intRow <= ChessBoardSize)
                    return true;
            }
            return false;


        }

        public bool validColumn(string column)
        {
            char charColumn;
            bool parseSuccesful = Char.TryParse(column.ToUpper(), out charColumn);
            if (parseSuccesful)
            {
                if (charColumn >= 'A' && charColumn <= 65 + ChessBoardSize)
                    return true;
            }
            return false;
        }
    }
}
