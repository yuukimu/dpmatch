using System;
using System.Collections.Generic;

namespace dpmatch
{
    public class DPMatching
    {

        public double MatchingByPower(List<double[]> testData, List<double[]> tempData) {
            double[][] ary = new double[testData.Count][];
            int[][] back = new int[testData.Count][];
            // バックポインタ初期化
            for (int i = 0; i < testData.Count; i++)
            {
                back[i] = new int[tempData.Count];
            }

            // 左端の列の計算
            ary[0] = new double[tempData.Count];
            for (int j = 0; j < tempData.Count; j++)
            {
                if (j == 0)
                {
                    ary[0][j] = Math.Pow(testData[0][0] - tempData[j][0], 2);
                } else {
                    ary[0][j] = Math.Pow(testData[0][0] - tempData[j][0], 2) + ary[0][j-1];
                }
                back[0][j] = 1;
            }

            // 下端の計算
            for (int i = 1; i < testData.Count; i++)
			{
                ary[i] = new double[tempData.Count];
				ary[i][0] = Math.Pow(testData[i][0] - tempData[0][0], 2) + ary[i-1][0];
                back[i][0] = 2;
			}

            for (int i = 1; i < testData.Count; i++)
            {
                for (int j = 1; j < tempData.Count; j++)
                {
					// 斜め
					double naname = 2 * Math.Pow(testData[i][0] - tempData[j][0], 2) + ary[i - 1][j - 1];
                    double min = naname;
					min = naname;
					back[i][j] = 3;
                    // 縦の計算
                    double tate = Math.Pow(testData[i][0] - tempData[j][0], 2) + ary[i][j-1];
                    if (min > tate && j < i + 5)
                    {
                        min = tate;
                        back[i][j] = 1;
                    }
                    // 横の計算
                    double yoko = Math.Pow(testData[i][0] - tempData[j][0], 2) + ary[i-1][j];
                    if (min > yoko && i < j + 5){
                        min = yoko;
                        back[i][j] = 2;
                    }
                    ary[i][j] = min;
                }
            }

            int bi = testData.Count - 1;
            int bj = tempData.Count - 1;
            double distance = 0.0;
            while (bi > 0 || bj > 0)
            {
                distance += ary[bi][bj];
                switch (back[bi][bj])
                {
                    case 1:
                        bj -= 1;
                        break;
                    case 2:
                        bi -=1;
                        break;
					case 3:
						bi -= 1;
                        bj -= 1;
						break;
                    default:
                        break;
                }
            }
            //double distance = CalcDPDistance(ary);
            return Math.Sqrt(distance);
        }

        public double MatchingByDcepstrum(List<double[]> testData, List<double[]> tempData)
		{
			double[][] ary = new double[testData.Count][];
			int[][] back = new int[testData.Count][];
			// バックポインタ初期化
			for (int i = 0; i < testData.Count; i++)
			{
				back[i] = new int[tempData.Count];
			}

			// 左端の列の計算
			ary[0] = new double[tempData.Count];
			for (int j = 0; j < tempData.Count; j++)
			{
				if (j == 0)
				{
					ary[0][j] = Math.Pow(testData[0][1] - tempData[j][1], 2);
				}
				else
				{
					ary[0][j] = Math.Pow(testData[0][1] - tempData[j][1], 2) + ary[0][j - 1];
				}
				back[0][j] = 1;
			}

			// 下端の計算
			for (int i = 1; i < testData.Count; i++)
			{
				ary[i] = new double[tempData.Count];
				ary[i][0] = Math.Pow(testData[i][1] - tempData[0][1], 2) + ary[i - 1][0];
				back[i][0] = 2;
			}

			for (int i = 1; i < testData.Count; i++)
			{
				for (int j = 1; j < tempData.Count; j++)
				{
					// 縦の計算
					double tate = Math.Pow(testData[i][1] - tempData[j][1], 2) + ary[i][j - 1];
					double min = tate;
					back[i][j] = 1;
					// 横の計算
					double yoko = Math.Pow(testData[i][1] - tempData[j][1], 2) + ary[i - 1][j];
					if (min > yoko)
					{
						min = yoko;
						back[i][j] = 2;
					}
					// 斜め
					double naname = 2 * Math.Pow(testData[i][1] - tempData[j][1], 2) + ary[i - 1][j - 1];
					if (min > naname)
					{
						min = naname;
						back[i][j] = 3;
					}
					ary[i][j] = min;
				}
			}

			int bi = testData.Count - 1;
			int bj = tempData.Count - 1;
			double distance = 0.0;
			while (bi > 0 || bj > 0)
			{
				distance += ary[bi][bj];
				switch (back[bi][bj])
				{
					case 1:
						bj -= 1;
						break;
					case 2:
						bi -= 1;
						break;
					case 3:
						bi -= 1;
						bj -= 1;
						break;
					default:
						break;
				}
                Console.WriteLine(bi + ", " + bj);
            }
			//double distance = CalcDPDistance(ary);
			return 2 * distance;
		}

        double CalcDPDistance(double[][] ary)
        {
            double distance = Math.Pow(ary[0][0], 2);

            for (int i = 0; i < ary[0].Length; i++)
            {

            }

            return distance;
        }
    }
}
