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

        public KnightModel(Point currPosition)
        {
            this.currPosition = currPosition;
        }

        public Point CurrentPosition{
            get => currPosition;
            set 
            { 
                currPosition = value;
                OnPropertyChanged();
            }
        }

    }
}
