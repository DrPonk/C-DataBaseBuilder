using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentOleg.DetailFolder
{
    public class PointTable
    {
        public int WinPoint { get; set; }
        public int LosePoint { get; set; }
        public int DrawPoint { get; set; }

        public PointTable()
        {

        }

        public PointTable(int winPoint, int losePoint, int drawPoint)
        {
        
            WinPoint = winPoint;
            LosePoint = losePoint;
            DrawPoint = drawPoint;
        }
    }

   

}
