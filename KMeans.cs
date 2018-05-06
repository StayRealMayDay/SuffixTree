using System;
using System.Collections.Generic;

namespace SuffixTree
{
    public class KMeans
    {
        public KMeans(List<Tuple<double, double>> data, int k)
        {
            Data = data;
            K = k;
        }

        private int K { get; set; }

        private List<Tuple<double, double>> Data { get; set; }
    }
}