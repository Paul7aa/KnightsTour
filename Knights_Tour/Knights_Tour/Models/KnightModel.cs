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

        public KnightModel(Point currPosition)
        {
            this.currPosition = currPosition;
            previousPosition = new Point();
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

        public void SetPreviousPosition()
        {
            PreviousPosition.X = CurrentPosition.X;
            PreviousPosition.Y = CurrentPosition.Y;
        }
    }
}
