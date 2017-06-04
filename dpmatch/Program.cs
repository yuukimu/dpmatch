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

            MainLoop(template, testset, futil);

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


    }
}
