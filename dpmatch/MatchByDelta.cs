using System;
using System.Collections.Generic;

namespace dpmatch
{
    public class MatchByDelta
    {

		List<double> testData;
		List<double> tempData;

		public double Match(List<double> testData, List<double> tempData)
		{
			this.testData = testData;
			this.tempData = tempData;
			double[,] ary = new double[testData.Count, tempData.Count];
			int[,][] back = new int[testData.Count, tempData.Count][];
			CalcLeftVertical(ref ary, ref back);
			CalcBtmHorizontal(ref ary, ref back);
			CalcOther(ref ary, ref back);
            double distance = ary[testData.Count - 1, tempData.Count - 1];
			return distance;
		}

		void CalcLeftVertical(ref double[,] ary, ref int[,][] back)
		{
			for (int j = 0; j < tempData.Count; j++)
			{
				if (j.Equals(0))
				{
					ary[0, j] = 2 * Math.Pow(testData[0] - tempData[j], 2);
					back[0, j] = new int[] { 0, 0 };
				}
				else
				{
					ary[0, j] = 2 * Math.Pow(testData[0] - tempData[j], 2) + ary[0, j - 1];
					back[0, j] = new int[] { 0, j - 1 };
				}
			}
		}

		void CalcBtmHorizontal(ref double[,] ary, ref int[,][] back)
		{
			for (int i = 1; i < testData.Count; i++)
			{
				ary[i, 0] = 2 * Math.Pow(testData[i] - tempData[0], 2) + ary[i - 1, 0];
				back[i, 0] = new int[] { i - 1, 0 };
			}
		}

		void CalcOther(ref double[,] ary, ref int[,][] back)
		{
            int diff = Math.Abs(testData.Count - tempData.Count);
			for (int i = 1; i < testData.Count; i++)
			{
				for (int j = 1; j < tempData.Count; j++)
				{
					// 斜め
					double slant = 4 * Math.Pow(testData[i] - tempData[j], 2) + ary[i - 1, j - 1];
					double minDist = slant;
					back[i, j] = new int[] { i - 1, j - 1 };

					// 縦
					double vertical = 2 * Math.Pow(testData[i] - tempData[j], 2) + ary[i, j - 1];
                    if (minDist > vertical)
					{
						minDist = vertical;
						back[i, j] = new int[] { i, j - 1 };
					}

					// 横
					double horizontal = 2 * Math.Pow(testData[i] - tempData[j], 2) + ary[i - 1, j];
                    if (minDist > horizontal)
					{
						minDist = horizontal;
						back[i, j] = new int[] { i - 1, j };
					}
					ary[i, j] = minDist;
				}
			}
		}

    }
}
