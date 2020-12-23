using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstable.RP5
{
    public class ObservationPoints : IEnumerable<ObservationPoint>
    {
        private SortedList<DateTime, ObservationPoint> list;

        public IList<ObservationPoint> List => list.Values;

        public ObservationPoint this[string stringDateTime]
        {
            get
            {
                return null;    
            }
        }

        public ObservationPoint this[DateTime dateTime]
        {
            get
            {
                return null;
            }
        }

        public ObservationPoints(IComparer<DateTime> comparer)
        {
            list = new SortedList<DateTime, ObservationPoint>(comparer);
        }

        public void Add(ObservationPoint point)
        {
            list[point.DateTime] = point;
        }

        public void AddRange(IEnumerable<ObservationPoint> points)
        {
            foreach (var point in points)
            {
                if (!list.ContainsKey(point.DateTime))
                {
                    list.Add(point.DateTime, point);
                }
            }
        }

        public void AddRange(ObservationPoint[] points)
        {
            for (int i = 0; i < points.Length; i++)
            {
                if (!list.ContainsKey(points[i].DateTime))
                {
                    list.Add(points[i].DateTime, points[i]);
                }
            }
        }

        public void AddCollection(ObservationPoints points)
        {
            foreach (var point in points)
            {
                if (!list.ContainsKey(point.DateTime))
                {
                    list.Add(point.DateTime, point);
                }
            }
        }

        public IEnumerator<ObservationPoint> GetEnumerator()
        {
            foreach (var point in list)
            {
                yield return point.Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
