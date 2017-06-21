using System;
using System.Collections.Generic;


namespace GomelRectorCouncil.Calculations
{
    public class Positions
    {

        private static void arraySort(Element[] array, int SortParam)
        {

            if (SortParam == 0)
            {
                Array.Sort(array, new ValueComparer());
            }
            else
            {
                Array.Sort(array, new ReverseValueComparer());
            }
        }
        /**
         * check array for duplicates and change results to places
         *
         * @param array
         *            double
         * @param param
         *            int
         */
        public static double[] Calculate(double[] array, int SortParam)
        {

            // an Objects array
            Element[] input = new Element[array.Length];

            Element[] output = new Element[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                input[i] = new Element()
                {
                    Index = i,
                    Value = array[i]
                };
                output[i] = new Element()
                {
                    Index = i,
                    Value = array[i]
                };

            }

            // sort
            Positions.arraySort(input, SortParam);

            // used elements
            HashSet<double> used = new HashSet<double>();
            // output array
            double[] places = new double[array.Length];
            // index in array
            int place = 0;
            // count how many duplicates
            int count = 0;
            // calculated index
            double index;
            // going through array first cycle
            for (int i = 0; i < input.Length; i++)
            {
                // if we used this elements - go to next element
                if (used.Contains(input[i].Value))
                {
                    continue;
                }
                else
                {
                    // else - add element to collection
                    used.Add(input[i].Value);
                }

                // temporal list for indexes
                List<int> positions = new List<int>();
                // add current position
                positions.Add(i);
                for (int j = i + 1; j < input.Length; j++)
                {
                    // check if there are duplicate elements
                    if (input[i].CompareTo(input[j]) == 0)
                    {
                        // if yes - add to the list
                        positions.Add(j);
                    }
                }
                // index in array
                place = 0;
                // count how many duplicates
                count = 0;
                // calculated index
                // for each positions count the sum of indexes and count duplicates
                foreach (int p in positions)
                {
                    place += p;
                    count++;

                }
                // count result
                index = (double)place / count;
                // write them to output array
                foreach (int p in positions)
                {
                    output[p].Value = index + 1;
                }

            }

            for (int i = 0; i < input.Length; i++)
            {
                output[i].Index = input[i].Index;
            }

  

            // sort objects by the index to the previous view
            Array.Sort(output, new IndexComparer());

            for (int i = 0; i < places.Length; i++)
            {
                places[i] = output[i].Value;
            }
            return places;



        }
    }
}















    




