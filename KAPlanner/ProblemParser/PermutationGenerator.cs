using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProblemParser
{
    class PermutationGenerator
    {
        private int mNumObjects;
        public int NumObjects { get { return mNumObjects; } }
        public PermutationGenerator(int numObjects)
        {
            mNumObjects = numObjects;

            //generate for size of one.
            List<int[]> firstList = new List<int[]>();
            for (int i = 0; i < numObjects; i++)
            {
                int[] set = new int[1];
                set[0] = i;
                firstList.Add(set);
            }

            mCache.Add(1, firstList);
        }
        private Dictionary<int, List<int[]>> mCache = new Dictionary<int, List<int[]>>();

        public List<int[]> Generate(int size)
        {
            List<int[]> ret;
            if (mCache.TryGetValue(size, out ret))
                return ret;

            return GenerateInternal(size);
        }

        private List<int[]> GenerateInternal(int size)
        {
            // first find the minimum key that exists:
            int max = mCache.Keys.Max();

            // generate one level at a time
            for (int i = max + 1; i <= size; i++)
            {
                GenerateNextPermSet();
            }

            return mCache[size];
        }

        private void GenerateNextPermSet()
        {
            int size = mCache.Keys.Max() + 1;
            List<int[]> newList = new List<int[]>();
            List<int[]> prevData = mCache[size - 1];
            foreach (int[] data in prevData)
            {
                // for each data 
                for (int i = 0; i < NumObjects; i++)
                {
                    int[] newData = new int[size];
                    data.CopyTo(newData, 0);
                    newData[size - 1] = i;
                    newList.Add(newData);
                }
            }

            mCache.Add(size, newList);

        }

    }
}
