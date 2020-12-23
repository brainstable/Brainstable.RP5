using System.Collections.Generic;

namespace Brainstable.RP5
{
    public class ObservationPointComparerUpInDown : IComparer<ObservationPoint>
    {
        public int Compare(ObservationPoint x, ObservationPoint y)
        {
            if (x.DateTime < y.DateTime)
            {
                return 1;
            }

            if (x.DateTime > y.DateTime)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}