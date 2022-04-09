using Knights_Tour.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knights_Tour.Models
{
    public class KnightModel : BaseModel, IDisposable
    {
        private Point startPosition;
        private Point currPosition;
        private Point previousPosition;
        private bool isMoving;

        public KnightModel(Point currPosition)
        {
            this.currPosition = currPosition;
            previousPosition = new Point(0,0);
        }

        public KnightModel(KnightModel knight)
        {
            this.startPosition = knight.startPosition;
            this.currPosition = knight.currPosition;
            this.previousPosition = knight.previousPosition;
            this.isMoving = knight.isMoving;
        }



        public Point StartPosition
        {
            get => startPosition;
            set
            {
                startPosition = value;
                OnPropertyChanged();
            }
        }

        public Point CurrentPosition{
            get => currPosition;
            set 
            {
                currPosition = value;
                OnPropertyChanged();
            }
        }

        public Point PreviousPosition
        {
            get => previousPosition;
            set
            {
                previousPosition = value;
                OnPropertyChanged();
            }
        }

        public bool IsMoving
        {
            get => isMoving;
            set
            {
                isMoving = value;
                OnPropertyChanged();
            }
        }

        public void SetPreviousPosition()
        {
            PreviousPosition.X = CurrentPosition.X;
            PreviousPosition.Y = CurrentPosition.Y;
        }

        public void SetToStartPosition()
        {
            if (StartPosition == null)
                return;
            CurrentPosition.X = StartPosition.X;
            CurrentPosition.Y = StartPosition.Y;
            SetPreviousPosition();
        }

        public void Reset()
        {
            CurrentPosition.X = 0;
            CurrentPosition.Y = 0;
            PreviousPosition = new Point(0,0);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                startPosition.Dispose();
                previousPosition.Dispose();
                CurrentPosition.Dispose();

            }

        }
    }
}
