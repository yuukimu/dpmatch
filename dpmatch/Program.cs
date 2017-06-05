using System;
using System.Collections.Generic;

namespace dpmatch
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            FileUtil futil = new FileUtil();
            List<string> template = futil.GetTemplate();
            List<string> testset = futil.GetTestSet();

            if (template.Count.Equals(0) && testset.Count.Equals(0)) {
                Console.WriteLine("テンプレート及び評価データがありません");
                return;
            }
            if (template.Count.Equals(0)) {
				Console.WriteLine("テンプレートがありません");
				return;
			}
            if (testset.Count.Equals(0)) {
				Console.WriteLine("評価データがありません");
				return;
			}

            MainLoop2(template, testset, futil);

        }

        static void MainLoop(List<string> template, List<string> testset, FileUtil futil)
        {
            List<double[]> testData;
            List<double[]> tempData;
            while (true)
            {
                Console.WriteLine("1: パワー系列  2: デルタパワー  0: 終了");
                string inputFromKey = Console.ReadLine();
                if (inputFromKey.Equals("0"))
                {
                    Console.WriteLine("終了しました");
                    break;
                }
                foreach (var testName in testset)
                {
                    testData = futil.ReadEachLine(testName);
                    double minDistance = -1;
                    string minTemp = "";
                    foreach (var tempName in template)
                    {
                        tempData = futil.ReadEachLine(tempName);
                        DPMatching dp = new DPMatching();
						if (inputFromKey.Equals("1"))
						{
						    double distance = dp.MatchingByPower(testData, tempData);
                            if (minDistance > distance || minDistance.Equals(-1))
                            {
                                minDistance = distance;
                                minTemp = tempName;
                            }
                        }
						else if (inputFromKey.Equals("2"))
						{
						    double distance = dp.MatchingByDcepstrum(testData, tempData);
							if (minDistance > distance || minDistance.Equals(-1))
							{
								minDistance = distance;
								minTemp = tempName;
							}
						}
					}
                    Console.WriteLine(testName + "," + minTemp);
                }
			}
        }

		static void MainLoop2(List<string> template, List<string> testset, FileUtil futil)
		{
            Dictionary<string, List<double>> testPowerData = new Dictionary<string, List<double>>();
            Dictionary<string, List<double>> testDeltaData = new Dictionary<string, List<double>>();
            Dictionary<string, List<double>> tempPowerData = new Dictionary<string, List<double>>();
            Dictionary<string, List<double>> tempDeltaData = new Dictionary<string, List<double>>();

            foreach (var testName in testset)
            {
                List<List<double>> data = futil.ReadEachLine2(testName);
                testPowerData.Add(testName, data[0]);
                testDeltaData.Add(testName, data[1]);
            }

            foreach (var tempName in template)
			{
                List<List<double>> data = futil.ReadEachLine2(tempName);
                tempPowerData.Add(tempName, data[0]);
                tempDeltaData.Add(tempName, data[1]);
			}

            DPMatch dp = new DPMatch();
            MatchByDelta dpDelta = new MatchByDelta();
            while (true)
            {
				Console.WriteLine("1: パワー系列  2: デルタパワー  0: 終了");
				string inputFromKey = Console.ReadLine();
				if (inputFromKey.Equals("0"))
				{
					Console.WriteLine("終了しました");
					break;
				}
                int correctNum = 0;
                foreach (var testName in testset)
                {
					double minDistance = -1;
					string minTemp = "";
                    foreach (var tempName in template)
                    {
						if (inputFromKey.Equals("1"))
						{
                            double distance = dp.MatchByPower(testPowerData[testName], tempPowerData[tempName]);
							if (minDistance > distance || minDistance.Equals(-1))
							{
								minDistance = distance;
								minTemp = tempName;
							}
						}
						else if (inputFromKey.Equals("2"))
						{
                            double distance = dpDelta.Match(testDeltaData[testName], tempDeltaData[tempName]);
							if (minDistance > distance || minDistance.Equals(-1))
							{
								minDistance = distance;
								minTemp = tempName;
							}
						}
                    }
					if (testName.Split('_')[1].Equals(minTemp.Split('_')[1]))
					{
						correctNum += 1;
                        Console.WriteLine(testName + " : 正解");
                    } else {
                        Console.WriteLine(testName + " : 不正解");
                    }
                }
                Console.WriteLine($"正解率 : {(1.0 * correctNum / testset.Count) * 100}");
            }
		}

    }
}
