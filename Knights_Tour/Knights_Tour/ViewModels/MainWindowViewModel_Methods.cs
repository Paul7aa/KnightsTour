using Knights_Tour.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knights_Tour.ViewModels
{
    public partial class MainWindowViewModel
    {
        public void StartAlgorithm()
        {
            IsExecuting = !IsExecuting;
            if (IsExecuting)
            {
                for (int i = 0; i < CellCollection.Size; i++)
                {
                    for(int j = 0; j < CellCollection.Size; j++)
                    {
                        CellCollection.Cells[i, j].CellState = Models.cellState.visited;
                    }
                    
                }
                CellCollectionModel auxCellCollection = new CellCollectionModel(CellCollection);
                CellCollection = auxCellCollection;
            }
        }
    }
}
