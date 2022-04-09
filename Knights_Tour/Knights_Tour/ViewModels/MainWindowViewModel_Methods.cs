using Knights_Tour.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Knights_Tour.ViewModels
{
    public partial class MainWindowViewModel
    {
        private async Task StartAlgorithmOnClick()
        {
            try
            {
                if (IsExecuting)
                {
                    IsExecuting = !IsExecuting;
                    cancelTokenSource.Cancel();
                    return;
                }
                IsExecuting = !IsExecuting;
                TextBoxesEnabled = false;
                if (IsExecuting)
                {
                    m_warnsdorffAlgorithm = new WarnsdorffAlgorithmModel(ChessBoardSize, Knight);
                    startTime = DateTime.Now;
                    TimeElapsed.Start();
                    await Task.Run(() => m_warnsdorffAlgorithm.GetSolution(cancelTokenSource.Token));
                    TimeElapsed.Stop();
                    int[,] solutionTour = m_warnsdorffAlgorithm.SolutionTour;

                    //await Task.Run (() => ShowSolution(solutionTour, cancelTokenSource.Token));

                    Int32 count = 1;
                    Knight.StartPosition = new Point(Knight.CurrentPosition);
                    Knight.IsMoving = true;
                    while (count != ChessBoardSize * ChessBoardSize)
                    {
                        for (int i = 0; i < solutionTour.GetLength(0); i++)
                        {
                            for (int j = 0; j < solutionTour.GetLength(1); j++)
                            {
                                if (solutionTour[i, j] == count)
                                {
                                    Knight.SetPreviousPosition();
                                    Knight.CurrentPosition.X = i;
                                    Knight.CurrentPosition.Y = j;
                                    Knight.CellsCrossed = count;
                                    KnightModel auxKnight = new KnightModel(Knight);
                                    Knight = auxKnight;
                                    await Task.Delay(Speed);

                                    if (cancelTokenSource.IsCancellationRequested)
                                        throw new TaskCanceledException();

                                    count++;
                                    continue;
                                }
                            }
                        }
                    }


                    Knight.IsMoving = false;
                    NeedsReset = true;
                    IsExecuting = false;
                    CommandManager.InvalidateRequerySuggested();
                }
            } 
            catch (TaskCanceledException)
            {
                Knight.IsMoving = false;
                NeedsReset = true;
                IsExecuting = false;
                TimeElapsed.Stop();
                cancelTokenSource = new CancellationTokenSource();
                CommandManager.InvalidateRequerySuggested();
            }
                
        }

        private void RestartTourOnClick() 
        {
            if (m_warnsdorffAlgorithm.SolutionTour == null)
                return;

            RestartTourRequested = true;
            CleanChessBoardOnClick();

        }

        private void CleanChessBoardOnClick()
        {
            if (RestartTourRequested)
            {
                RestartTourRequested = false;
                Knight.SetToStartPosition();
                //trigger knight change (not good practice)
                KnightModel auxKnight = new KnightModel(Knight);
                Knight = auxKnight;
                this.StartAlgorithmCommand.Execute(null);
            }
            else
            {
                KnightX = "1";
                KnightY = "A";
                Knight.Reset();
                KnightModel auxKnight = new KnightModel(Knight);
                Knight = auxKnight;
                NeedsReset = false;
                TextBoxesEnabled = true;
            }
        }


        private async Task ChangeSpeedOnClick()
        {
            switch (Speed)
            {
                case 1:
                    Speed = 1000;
                    break;
                case 500:
                    Speed = 1;
                    break;
                case 1000:
                    Speed = 500;
                    break;
            }
        }

        private void timeElapsedTick(object sender, EventArgs e)
        {
            TimeElapsedString = Convert.ToString((DateTime.Now - startTime).TotalMilliseconds);
            CommandManager.InvalidateRequerySuggested();
        }

        private bool CanStartAlgorithm()
        {
            bool ChessBoardSizeValid = (ChessBoardSize > 4 && ChessBoardSize < 17);

            bool knightXValid = (Knight.CurrentPosition.X >= 0 && Knight.CurrentPosition.X < ChessBoardSize);

            bool KnightYValid = (Knight.CurrentPosition.Y >= 0 && Knight.CurrentPosition.Y < ChessBoardSize);

            return ChessBoardSizeValid && knightXValid && KnightYValid; 
        }

        private bool validRow(string row)
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

        private bool validColumn(string column)
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
