using System;
using System.Linq;
using System.Collections.Generic;
using GomelRectorCouncil.Models;

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
        public static float[] Calculate(float[] array, int SortParam)
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
            HashSet<float> used = new HashSet<float>();
            // output array
            float[] places = new float[array.Length];
            // index in array
            int place = 0;
            // count how many duplicates
            int count = 0;
            // calculated index
            float index;
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
                index = (float)place / count;
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
        public static List<Achievement> Get(List<Achievement> inputAchievementsYear)
        {
            List<Achievement> outputAchievementsYear=new List<Achievement>();
            int i3=0;
            var groupIndicatorId1=inputAchievementsYear.GroupBy(j1=>j1.Indicator.IndicatorId1);
            foreach (var item1 in groupIndicatorId1)
            {
                var groupIndicatorId2=item1.GroupBy(j2=>j2.Indicator.IndicatorId3);
                foreach (var item2 in groupIndicatorId2)
                {
                    var groupIndicatorId3=item2.GroupBy(j3=>j3.Indicator.IndicatorId3);
                    foreach (var item3 in groupIndicatorId3)
                    {
                        float[] inputarray3=item3.Select(t=>t.IndicatorValue).ToArray();
                        int indicatorType3=(int)item3.Select(o=>o.Indicator.IndicatorType).FirstOrDefault();
                        float[] outputarray3=Calculate(inputarray3,indicatorType3);
                        i3=0;
                        foreach (var t in item3)
                        {
                            t.Position=outputarray3[i3];
                        }
                    }
                    // var inputarray2=item2.Select(t=> new {t.IndicatorValue,t.Position}).ToArray();
                    // int indicatorType2=(int)item2.Select(o=>o.Indicator.IndicatorType).FirstOrDefault();
                    // float[] outputarray2=Calculate(inputarray2,indicatorType2);
                    // var i2=0;
                    // foreach (var t in item2)
                    // {
                    //     t.Position=outputarray2[i2];
                    // }
                    


                }


                

            }
            return outputAchievementsYear;

        }
    }
}















    




