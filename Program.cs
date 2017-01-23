using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2._36
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Gyulai Márton
             * 2017.01.21.
             * Area of a triangle from its vertices' coordinates
             * 
             * Of course there is a simpler way of doing it: the Heron's formula
             * But it's too easy for us
             *
             * The algorithm:
             *  - calculating one of the altitudes of the triangle
             *     - select a not horizontal or vertical side of the triangle
             *     - calculate perpendicular line from the opposite vertex to that side
             *     - find the intersection
             *     - calculate the distance between the intersection and the opposite vertex, this distance is the altitude
             *  - returning the area based on the side and the altitude (side * altitude / 2)
             */

            Console.WriteLine("Area of a triangle from its vertices' coordinates");

            System.Threading.Thread.Sleep(2000);

            Console.WriteLine("\nThe first vertex of the triangle (e.g: -2,3335 3)");
            double[] A = (from d in Console.ReadLine().Split(' ') select Convert.ToDouble(d)).ToArray<double>();

            Console.WriteLine("The second vertex of the triangle");
            double[] B = (from d in Console.ReadLine().Split(' ') select Convert.ToDouble(d)).ToArray<double>();

            Console.WriteLine("The third vertex of the triangle");
            double[] C = (from d in Console.ReadLine().Split(' ') select Convert.ToDouble(d)).ToArray<double>();




            Line side = new Line(B, C); //Create a Line from its two points
            double[] vertex = A; //The opposite vertex

            if (double.IsInfinity(side.a) || side.a == 0) //If it is horizontal or vertical
            {
                side = new Line(A, C); //Select the next side
                vertex = B;
            }

            if (double.IsInfinity(side.a) || side.a == 0) //If it is horizontal or vertical
            {
                side = new Line(A, B); //Select the next side
                vertex = C;
            }


            Line perp = side.Perpendicular(vertex); //Perpendicular to that side of the triangle

            double[] intrsction = perp.Intersection(side); //Find the intersection

            double height = dist(vertex, intrsction); //The altitude is the distance between the intersection and the vertex

            /*
             * Calculate the area of the triangle
             * Then print it to the console
             */
            Console.WriteLine("\nThe area of the triangle: " + (dist(new double[] { side.points[0, 0], side.points[0, 1] }, new double[] { side.points[1, 0], side.points[1, 1] }) * height / 2));

        }

        static double dist(double[] a, double[] b)
        {
            return Math.Sqrt( Math.Pow((a[0] - b[0]), 2) + Math.Pow((a[1] - b[1]), 2));
        }

        class Line
        {
            public double a;
            double b;
            public double[,] points = new double[2, 2];

            public Line(double a, double b)
            {
                this.a = a;
                this.b = b;
            }

            public Line(double[] A, double[] B)
            {
                a = (A[1] - B[1]) / (A[0] - B[0]);
                b = A[1] - a * A[0];

                points[0, 0] = A[0];
                points[0, 1] = A[1];
                points[1, 0] = B[0];
                points[1, 1] = B[1];
            }

            public bool Contains(double[] toCheck)
            {
                return toCheck[1] == toCheck[0] * a + b;
            }

            public Line Perpendicular(double[] point)
            {
                return new Line(-1 / a, point[1] - (-1 / a) * point[0]);
            }

            public double[] Intersection(Line other)
            {
                return new double[2] { (other.b - b) / (a - other.a), (other.b - b) / (a - other.a) * a + b };
            }
        }
    }
}
