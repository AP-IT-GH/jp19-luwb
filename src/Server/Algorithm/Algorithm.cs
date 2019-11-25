using Server.Models;
using System;
using System.Collections.Generic;

namespace Server.Algorithm
{
    public class Algorithm
    {
        public static double[] Calculate(List<Data> dataList)
        {
            double[] pos = new double[2];
            double xPos;
            double yPos;
            if (dataList.Count > 3)
            {
                List<M> ms = new List<M>
                {
                    calculateThreeCircleIntersection(dataList[1].X_Pos, dataList[1].Y_Pos, dataList[1].Distance, dataList[2].X_Pos, dataList[2].Y_Pos, dataList[2].Distance, dataList[3].X_Pos, dataList[3].Y_Pos, dataList[3].Distance),
                    calculateThreeCircleIntersection(dataList[0].X_Pos, dataList[0].Y_Pos, dataList[0].Distance, dataList[1].X_Pos, dataList[1].Y_Pos, dataList[1].Distance, dataList[2].X_Pos, dataList[2].Y_Pos, dataList[2].Distance)
                };

                //xPos = (ms[0].XPos + ms[1].XPos) / 2;
                //yPos = (ms[0].YPos + ms[1].YPos) / 2;

                xPos = ms[0].XPos;
                yPos = ms[0].YPos;
            }
            else if (dataList.Count == 3)
            {
                M m = calculateThreeCircleIntersection(dataList[0].X_Pos, dataList[0].Y_Pos, dataList[0].Distance, dataList[1].X_Pos, dataList[1].Y_Pos, dataList[1].Distance, dataList[2].X_Pos, dataList[2].Y_Pos, dataList[2].Distance);
                xPos = m.XPos;
                yPos = m.YPos;
            }
            else
            {
                xPos = 0;
                yPos = 0;
            }

            pos[0] = xPos;
            pos[1] = yPos;

            return pos;
        }

        private static M calculateThreeCircleIntersection(double x0, double y0, double r0,
                                                      double x1, double y1, double r1,
                                                      double x2, double y2, double r2)
        {
            //define EPSILON to a small value that is acceptable for your application requirements (0.000001)
            double EPSILON = 400;

            double a, dx, dy, d, h, rx, ry;
            double point2_x, point2_y;

            /* dx and dy are the vertical and horizontal distances between
            * the circle centers.
            */
            dx = x1 - x0;
            dy = y1 - y0;

            /* Determine the straight-line distance between the centers. */
            d = Math.Sqrt(((dy * dy) + (dx * dx)));

            /* Check for solvability. */
            if (d > (r0 + r1))
            {
                /* no solution. circles do not intersect. */
                return new M { XPos = 1, YPos = 1 };
            }
            if (d < Math.Abs(r0 - r1))
            {
                /* no solution. one circle is contained in the other */
                return new M { XPos = 2, YPos = 2 };
            }

            /* 'point 2' is the point where the line through the circle
            * intersection points crosses the line between the circle
            * centers.
            */

            /* Determine the distance from point 0 to point 2. */
            a = ((r0 * r0) - (r1 * r1) + (d * d)) / (2.0 * d);

            /* Determine the coordinates of point 2. */
            point2_x = x0 + (dx * a / d);
            point2_y = y0 + (dy * a / d);

            /* Determine the distance from point 2 to either of the
            * intersection points.
            */
            h = Math.Sqrt((r0 * r0) - (a * a));

            /* Now determine the offsets of the intersection points from
            * point 2.
            */
            rx = -dy * (h / d);
            ry = dx * (h / d);

            /* Determine the absolute intersection points. */
            double intersectionPoint1_x = point2_x + rx;
            double intersectionPoint2_x = point2_x - rx;
            double intersectionPoint1_y = point2_y + ry;
            double intersectionPoint2_y = point2_y - ry;

            //MessageBox.Show("INTERSECTION Circle1 AND Circle2: (" + intersectionPoint1_x + ":" + intersectionPoint1_y + ")" + " AND (" + intersectionPoint2_x + ":" + intersectionPoint2_y + ")");

            /* Lets determine if circle 3 intersects at either of the above intersection points. */
            double dx2 = intersectionPoint1_x - x2;
            double dy2 = intersectionPoint1_y - y2;
            double d3a = Math.Sqrt((dy2 * dy2) + (dx2 * dx2));

            dx2 = intersectionPoint2_x - x2;
            dy2 = intersectionPoint2_y - y2;
            double d3b = Math.Sqrt((dy2 * dy2) + (dx2 * dx2));

            if (Math.Abs(d3a - r2) < EPSILON)
            {
                return new M { XPos = (int)Math.Round(intersectionPoint1_x), YPos = (int)Math.Round(intersectionPoint1_y) };
            }
            else if (Math.Abs(d3b - r2) < EPSILON)
            {
                return new M { XPos = (int)Math.Round(intersectionPoint2_x), YPos = (int)Math.Round(intersectionPoint2_y) };
            }
            /* no solution. the 3 circles doesn't intersect on 1 point */
            return new M { XPos = 3, YPos = 3 };
        }
    }

    public class M
    {
        public double XPos { get; set; }
        public double YPos { get; set; }
    }
}
