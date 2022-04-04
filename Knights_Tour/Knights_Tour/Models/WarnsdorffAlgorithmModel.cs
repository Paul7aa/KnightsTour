using Knights_Tour.BaseModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Knights_Tour.Models
{
    public class WarnsdorffAlgorithmModel : BaseModel
    {
        private int chessBoardSize;
        // Move pattern on basis of the change of
        // x coordinates and y coordinates respectively
        private int[] cx = new int[8] { 1, 1, 2, 2, -1, -1, -2, -2 };
        private int[] cy = new int[8] { 2, -2, 1, -1, 2, -2, 1, -1 };
        private int cellNumber = 1;
        private long toursTested;
        private int[,] solutionTour;
        private bool solutionFound;
        private KnightModel knight;

        public WarnsdorffAlgorithmModel(int chessBoardSize, KnightModel knight)
        {
            this.chessBoardSize = chessBoardSize;
            this.toursTested = 0;
            this.solutionTour = new int[chessBoardSize, chessBoardSize];
            this.knight = knight;
        }

        public bool SolutionFound
        {
            get=> solutionFound;
            set=> solutionFound = value;
        }

        public long ToursTested
        {
            get => toursTested;
            set
            {
                toursTested = value;
                OnPropertyChanged();
            }
        }

        public int[,] SolutionTour
        {
            get => solutionTour;
            set
            {
                solutionTour = value;
                OnPropertyChanged();
            }
        }

        // function restricts the knight to remain within
        // the NxN chessboard
        private bool limits(int x, int y) 
        {
            return ((x >= 0 && y >= 0) && (x < chessBoardSize && y < chessBoardSize));
        }

        /* Checks whether a square is valid and empty or not */
        private bool isEmpty(int [,]a, int x, int y)
        {
            return (limits(x, y)) && (a[x,y] < 0);
        }
        /* Returns the number of empty squares adjacent
           to (x, y) */
        private int getDegree(int [,] a, int x, int y)
        {
            int count = 0;
            for (int i = 0; i < 8; i++)
                if (isEmpty(a, (x + cx[i]), (y + cy[i])))
                    count++;

            return count;
        }


        // Picks next point using Warnsdorff's heuristic.
        // Returns false if it is not possible to pick
        // next point.
        private bool nextMove(int[,] a, ref int x, ref int y)
        {
            int min_deg_idx = -1, c, min_deg = (chessBoardSize + 1), nx, ny;

            // Try all N adjacent of (x, y) starting
            // from a random adjacent. Find the adjacent
            // with minimum degree.
            Random random = new Random();
            int start = random.Next(0, 8);
            for (int count = 0; count < 8; count++)
            {
                int i = (start + count) % 8;
                nx = x + cx[i];
                ny = y + cy[i];
                if ((isEmpty(a, nx, ny)) &&
                   (c = getDegree(a, nx, ny)) < min_deg)
                {
                    min_deg_idx = i;
                    min_deg = c;
                }
            }

            // IF we could not find a next cell
            if (min_deg_idx == -1)
                return false;

            // Store coordinates of next point
            nx = x + cx[min_deg_idx];
            ny = y + cy[min_deg_idx];

            // Mark next move
            a[nx, ny] = a[nx, ny] + 1;
            SolutionTour[nx, ny] = cellNumber++;
            // Update next point
            x = nx;
            y = ny;

            return true;
        }


        /* checks its neighbouring squares */
        /* If the knight ends on a square that is one
           knight's move from the beginning square,
           then tour is closed */

        private bool neighbour(int x, int y, int xx, int yy)
        {
            for (int i = 0; i < 8; i++)
                if (((x + cx[i]) == xx) && ((y + cy[i]) == yy))
                    return true;

            return false;
        }

        /* Generates the legal moves using warnsdorff's
          heuristics. Returns false if not possible */
        private bool findClosedTour()
        {
            cellNumber = 0;
            // Filling up the chessboard matrix with -1's
            int[,] a = new int[chessBoardSize,chessBoardSize];
            for (int i = 0; i < chessBoardSize; i++)
                for(int j = 0; j < chessBoardSize; j++) 
                    a[i,j] = -1;

            // knight coordinates initial position
            int sx = knight.CurrentPosition.X;
            int sy = knight.CurrentPosition.Y;

            // Current points are same as initial points
            int x = sx, y = sy;
            a[x,y] = 1; // Mark first move.
            SolutionTour[x, y] = cellNumber++;
            // Keep picking next points using
            // Warnsdorff's heuristic
            for (int i = 0; i < chessBoardSize * chessBoardSize - 1; i++)
                if (nextMove(a, ref x, ref y) == false)
                    return false;

            // Check if tour is closed (Can end
            // at starting point)
            if (!neighbour(x, y, sx, sy))
                return false;

            return true;
        }

        public async Task GetSolution(CancellationToken cancellationToken)
        {
            TimeSpan timeLimit = new TimeSpan(0, 0, 0, chessBoardSize);
            Stopwatch sw = new Stopwatch();

            //reset solution
            for (int i=0; i<chessBoardSize; i++)
                for (int j = 0; j < chessBoardSize; j++)
                    SolutionTour[i, j] = 0;

            sw.Start(); //start timer

            while (!findClosedTour())
            {
                ToursTested++;
                if (sw.Elapsed > timeLimit || cancellationToken.IsCancellationRequested)
                {
                    sw.Stop();
                    solutionFound = false;
                    throw new TaskCanceledException();
                }
            }

            solutionFound = true;
            sw.Stop();
        }


    }
}
