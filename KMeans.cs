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
            ModelList = new List<char>();
//            CategoryDic = new Dictionary<int, List<Tuple<double, double>>>();
//            OldCategoryDic = new Dictionary<int, List<Tuple<double, double>>>();
        }

//        public List<Tuple<double, double>> KMeansAlgro()
//        {
//            var CategoryChanged = true;
//            var KMiddlePoint = new List<Tuple<double, double>>();
//            for (int i = 0; i < K; i++)
//            {
//                KMiddlePoint.Add(Data[i]);
//            }
//
//            while (CategoryChanged)
//            {
//                CategoryDic.Clear();
//                CategoryChanged = false;
//                foreach (var tuple in Data)
//                {
//                    var category = 0;
//                    var minDistance = Math.Pow(tuple.Item1 - KMiddlePoint[0].Item1, 2) +
//                                      Math.Pow(tuple.Item2 - KMiddlePoint[0].Item2, 2);
//                    for (int i = 1; i < K; i++)
//                    {
//                        var temp = Math.Pow(tuple.Item1 - KMiddlePoint[i].Item1, 2) +
//                                   Math.Pow(tuple.Item2 - KMiddlePoint[i].Item2, 2);
//                        if (temp < minDistance)
//                        {
//                            category = i;
//                            minDistance = temp;
//                        }
//                    }
//
//                    if (OldCategoryDic.ContainsKey(category) && !OldCategoryDic[category].Contains(tuple))
//                    {
//                        CategoryChanged = true;
//                    }
//
//                    if (!CategoryDic.ContainsKey(category))
//                    {
//                        CategoryDic.Add(category, new List<Tuple<double, double>>());
//                    }
//
//                    CategoryDic[category].Add(tuple);
//                }
//
//                OldCategoryDic = new Dictionary<int, List<Tuple<double, double>>>(CategoryDic);
//                foreach (var keyValue in CategoryDic)
//                {
//                    var item1 = 0.0;
//                    var item2 = 0.0;
//                    foreach (var tuple in keyValue.Value)
//                    {
//                        item1 += tuple.Item1;
//                        item2 += tuple.Item2;
//                    }
//
//                    KMiddlePoint[keyValue.Key] =
//                        new Tuple<double, double>(item1 / keyValue.Value.Count, item2 / keyValue.Value.Count);
//                }
//            }
//
//            return KMiddlePoint;
//        }
//
//        private Dictionary<int, List<Tuple<double, double>>> OldCategoryDic { get; set; }
//
//        private Dictionary<int, List<Tuple<double, double>>> CategoryDic { get; set; }

        /// <summary>
        /// this function category the data
        /// we choose the first K data to be the middle point
        /// the calculate the distance to the the middle point of
        /// all point in the data and then category
        /// then renew the middle point till the category not change
        /// </summary>
        /// <returns></returns>
        public List<char> KMeansAlg()
        {
            // init the categoty, the value of the array indicate which category the data in
            Category = new int[Data.Count];
            var categoryChanged = true;
            var kMiddlePoint = new List<Tuple<double, double>>();
            // choose the first K point to be the middle point
            // is alse means K categories
            for (int i = 0; i < K; i++)
            {
                kMiddlePoint.Add(Data[i]);
                Category[i] = i;
            }

            while (categoryChanged)
            {
                categoryChanged = false;
                // calculate the min distance each point to the K middle point 
                for (int i = 0; i < Data.Count; i++)
                {
                    var category = 0;
                    var minDistance = Math.Pow(Data[i].Item1 - kMiddlePoint[0].Item1, 2) +
                                      Math.Pow(Data[i].Item2 - kMiddlePoint[0].Item2, 2);
                    for (int j = 1; j < K; j++)
                    {
                        var temp = Math.Pow(Data[i].Item1 - kMiddlePoint[j].Item1, 2) +
                                   Math.Pow(Data[i].Item2 - kMiddlePoint[j].Item2, 2);
                        if (temp < minDistance)
                        {
                            category = j;
                            minDistance = temp;
                        }
                    }
                    //if this point changed its category then we change its value in the array
                    // and change the value of the variable categoryChanged
                    if (Category[i] != category)
                    {
                        categoryChanged = true;
                        Category[i] = category;
                    }
                }
                // renew the middle point value
                var KCount = new int[K];
                var KArray = new Double[K, 2];
                for (int j = 0; j < Data.Count; j++)
                {
                    KCount[Category[j]]++;
                    KArray[Category[j], 0] += Data[j].Item1;
                    KArray[Category[j], 1] += Data[j].Item2;
                }
                
                for (int j = 0; j < K; j++)
                {
                    kMiddlePoint[j] = new Tuple<double, double>(KArray[j, 0] / KCount[j], KArray[j, 1] / KCount[j]);
                }
            }

            for (int i = 0; i < Category.Length; i++)
            {
                var temp = '0';
                if (Category[i] == 1)
                {
                    temp = '1';
                }

                if (Category[i] == 2)
                {
                    temp = '2';
                }

                if (Category[i] == 3)
                {
                    temp = '3';
                }
                if (Category[i] == 4)
                {
                    temp = '4';
                }

                if (Category[i] == 5)
                {
                    temp = '5';
                }

                if (Category[i] == 6)
                {
                    temp = '6';
                }
                if (Category[i] == 7)
                {
                    temp = '7';
                }

                if (Category[i] == 8)
                {
                    temp = '8';
                }

                ModelList.Add(temp);
            }

            return ModelList;
        }

        private int K { get; set; }

        private int[] Category { get; set; }

        private List<char> ModelList { get; set; }

        private List<Tuple<double, double>> Data { get; set; }
    }
}