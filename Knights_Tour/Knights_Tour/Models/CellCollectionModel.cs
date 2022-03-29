using Knights_Tour.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knights_Tour.Models
{
    public class CellCollectionModel : BaseModel
    {
        private CellModel[,] m_cells;
        private int m_size;

        public CellCollectionModel(int size)
        {
            m_size = size;
            m_cells = new CellModel[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    m_cells[i, j] = new CellModel();  
                }
            }
        }
        
        public CellModel[,] Cells
        {
            get => m_cells;
            set
            {
                m_cells = value;
                OnPropertyChanged();
            }
        }

        public int Size
        {
            get => m_size;
            set
            {
                m_size = value;
                OnPropertyChanged();
            }
        }

    }
}
