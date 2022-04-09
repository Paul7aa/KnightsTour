using Knights_Tour.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knights_Tour.Models
{
    public class Point : BaseModel, IDisposable
    {
        private int x;
        private int y;
        
        public Point()
        {
            this.x = 0;
            this.y = 0; 
        }
        
        public Point(Point p)
        {
            this.x = p.x;
            this.y = p.y;
        }

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int X
        {
            get => x;
            set
            {
                x = value;
                OnPropertyChanged();
            }

        }

        public int Y 
        {
            get => y;
            set
            {
                y = value;
                OnPropertyChanged();
            }
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            x = 0;
            y = 0;
        }

    }
}
