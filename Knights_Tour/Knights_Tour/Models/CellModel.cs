﻿using Knights_Tour.BaseModels;
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

        public CellModel(int i, int j, cellState state)
        {
            m_state = state;
            if (i % 2 != 0 && j % 2 != 0 || i % 2 == 0 && j % 2 == 0)
            {
                m_colour = cellColour.white;
            }
            else
            {
                m_colour = cellColour.black;
            }
        }

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
