using System.Collections.Generic;

using NInsight.Core.Domain;

namespace NInsight.Core.Context
{
    internal class ReplayContext
    {
        //[ThreadStatic]
        internal static Run Run;

        //[ThreadStatic]
        internal static Point CurrentPoint;

        internal static Stack<Point> PointStack = new Stack<Point>();

        internal static void Init(Run run)
        {
            Run = run;
        }

        internal static void Init(Point point)
        {
            CurrentPoint = point;
        }
    }
}