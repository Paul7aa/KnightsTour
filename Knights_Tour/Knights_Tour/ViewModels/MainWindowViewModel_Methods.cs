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
        public async Task StartAlgorithm()
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
                if (IsExecuting)
                {
                    m_warnsdorffAlgorithm = new WarnsdorffAlgorithmModel(ChessBoardSize, Knight);
                    startTime = DateTime.Now;
                    TimeElapsed.Start();
                    await Task.Run(() => m_warnsdorffAlgorithm.GetSolution(cancelTokenSource.Token));
                    TimeElapsed.Stop();
                    int[,] solutionTour = m_warnsdorffAlgorithm.SolutionTour;

                    //await Task.Run (() => ShowSolution(solutionTour, cancelTokenSource.Token));

                    int count = 1;
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
                                    KnightModel auxKnight = new KnightModel(Knight);
                                    Knight = auxKnight;
                                    count++;
                                    await Task.Delay(200);
                                }
                            }
                        }
                        if (cancelTokenSource.IsCancellationRequested)
                            throw new TaskCanceledException();
                    }
                    NeedsReset = true;
                    IsExecuting = false;
                    CommandManager.InvalidateRequerySuggested();
                }
            } 
            catch (TaskCanceledException)
            {
                NeedsReset = true;
                IsExecuting = false;
                TimeElapsed.Stop();
                cancelTokenSource = new CancellationTokenSource();
                CommandManager.InvalidateRequerySuggested();
            }
                
        }

        public async Task ShowSolution(int[,] solutionTour, CancellationToken cancellationToken)
        {
            int count = 1;
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
                            KnightModel auxKnight = new KnightModel(Knight);
                            Knight = auxKnight;
                            count++;
                            await Task.Delay(200);
                        }
                    }
                }
                if (cancellationToken.IsCancellationRequested)
                    throw new TaskCanceledException();
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
