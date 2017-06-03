using System;
using System.Collections.Generic;
using System.IO;

namespace dpmatch
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            FileUtil futil = new FileUtil();
            List<string> fileList = new List<string>();
            try
            {
                fileList = futil.GetFileList("./trainingset");
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine(e);
                return;
            }

            List<double[]> file;
            while (true)
            {
                Console.WriteLine("1: パワー系列  2: デルタパワー  0: 終了");
                string inputFromKey = Console.ReadLine();
                if (inputFromKey.Equals("0"))
                {
                    Console.WriteLine("終了しました");
                    break;
                }
                foreach (var name in fileList)
				{
					file = futil.ReadEachLine(name);
					DPMatching dp = new DPMatching(file);
					if (inputFromKey.Equals("1"))
					{
						Console.WriteLine("パワー系列で比較");
                        dp.matchingByPower();
                    } else if (inputFromKey.Equals("2"))
                    {
                        Console.WriteLine("デルタパワーで比較");
                        dp.matchingByDcepstrum();
                    }
                }
            }

        }
    }
}
