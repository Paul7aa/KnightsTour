using Knights_Tour.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knights_Tour.Models
{
     public enum cellState
    {
        notVisited,
        visited
    }

    public enum cellColour
    {
        black,
        white
    }

    public class CellModel : BaseModel
    {
        private cellState m_state = cellState.notVisited;
        private cellColour m_colour;

        public cellState CellState
        {
            get => m_state;
            set
            {
                m_state = value;
                OnPropertyChanged();
            }
        }

        public cellColour CellColour
        {
            get => m_colour;
            set
            {
                m_colour = value;
                OnPropertyChanged();
            }
        }

        public void ResetState()
        {
            CellState = cellState.notVisited;
        }

        public void SetAsVisited()
        {
            CellState = cellState.visited;
        }
    }
}
