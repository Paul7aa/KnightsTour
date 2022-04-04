using Knights_Tour.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knights_Tour.Models
{
    public class KnightModel : BaseModel
    {
        private Point currPosition;
        private Point previousPosition;
        private bool isMoving;

        public KnightModel(Point currPosition)
        {
            this.currPosition = currPosition;
            previousPosition = new Point();
            
        }

        public KnightModel(KnightModel knight)
        {
            this.currPosition = knight.currPosition;
            previousPosition = new Point();
            this.isMoving = knight.isMoving;
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
    }
}
