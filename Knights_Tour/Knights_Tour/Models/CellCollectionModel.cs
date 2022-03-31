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
                    m_cells[i, j] = new CellModel(i, j, cellState.notVisited); 
                }
            }
        }

        public CellCollectionModel(CellCollectionModel cellCollection)
        {
            this.m_cells = cellCollection.m_cells;
            this.m_size = cellCollection.m_size;
        }
        
        public CellModel[,] Cells
        {
            get
            {
                return m_cells;
            }
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
