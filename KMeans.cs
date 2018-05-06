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
            CategoryDic = new Dictionary<int, List<Tuple<double, double>>>();
        }

        public List<Tuple<double, double>> KMeansAlgro()
        {
            var CategoryChanged = true;
            var KMiddlePoint = new List<Tuple<double, double>>();
            for (int i = 0; i < K; i++)
            {
                KMiddlePoint.Add(Data[i]);
            }
            
            while (CategoryChanged)
            {
                OldCategoryDic = new Dictionary<int, List<Tuple<double, double>>>(CategoryDic);
                CategoryDic.Clear();
                CategoryChanged = false;
                foreach (var tuple in Data)
                {
                    var category = 0;
                    var minDistance = Math.Pow(tuple.Item1 - KMiddlePoint[0].Item1, 2) +
                                      Math.Pow(tuple.Item2 - KMiddlePoint[0].Item2, 2);
                    for (int i = 1; i < K; i++)
                    {
                        var temp = Math.Pow(tuple.Item1 - KMiddlePoint[i].Item1, 2) +
                                   Math.Pow(tuple.Item2 - KMiddlePoint[i].Item2, 2);
                        if (temp < minDistance)
                        {
                            category = i;
                            minDistance = temp;
                        }
                    }

                    if (!CategoryDic.ContainsKey(category))
                    {
                        CategoryDic.Add(category, new List<Tuple<double, double>>());
                    }
                    CategoryDic[category].Add(tuple);
                }

                foreach (var keyValue in CategoryDic)
                {
                    var item1 = 0.0;
                    var item2 = 0.0;
                    foreach (var tuple in keyValue.Value)
                    {
                        item1 += tuple.Item1;
                        item2 += tuple.Item2;
                    }
                    KMiddlePoint[keyValue.Key] = new Tuple<double, double>(item1 / keyValue.Value.Count, item2 / keyValue.Value.Count);
                }
            }
            return KMiddlePoint;
        }

        private Dictionary<int, List<Tuple<double, double>>> OldCategoryDic { get; set; }
        
        private Dictionary<int, List<Tuple<double, double>>> CategoryDic { get; set; }

        private int K { get; set; }

        private List<Tuple<double, double>> Data { get; set; }
    }
}