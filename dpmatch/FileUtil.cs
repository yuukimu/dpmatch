using System;
using System.Collections.Generic;
using System.IO;

namespace dpmatch
{
    public class FileUtil
    {

        public List<double[]> ReadEachLine(string path) {
            System.Text.RegularExpressions.Regex splitReg = new System.Text.RegularExpressions.Regex(@"\t+");
            List<double[]> lines = new List<double[]>();
            StreamReader file = new StreamReader(path);
            string line;
            string[] values;
            while ((line = file.ReadLine()) != null)
            {
                values = splitReg.Split(line);
                //Console.WriteLine(line);
                double[] tmp = { double.Parse(values[0]), double.Parse(values[1]) };
                lines.Add(tmp);
            }
            file.Close();
            return lines;
        }

        // パワー系列とデルタパワーのリストを取得
		public List<List<double>> ReadEachLine2(string path)
		{
			System.Text.RegularExpressions.Regex splitReg = new System.Text.RegularExpressions.Regex(@"\t+");
            List<List<double>> data = new List<List<double>>();
			StreamReader file = new StreamReader(path);
            List<double> power = new List<double>();
            List<double> dcepstrum = new List<double>();
			string line;
			string[] values;
			while ((line = file.ReadLine()) != null)
			{
				values = splitReg.Split(line);
                power.Add(double.Parse(values[0]));
                dcepstrum.Add(double.Parse(values[1]));
			}
			file.Close();
            data.Add(power);
            data.Add(dcepstrum);
            return data;
		}

		public List<string> GetTemplate()
		{
			List<string> template = new List<string>();
			try
			{
				template = GetFileList("./trainingset");
			}
			catch (DirectoryNotFoundException e)
			{
				Console.WriteLine(e);
			}
			return template;
		}

		public List<string> GetTestSet()
		{
			List<string> testset = new List<string>();
			try
			{
				testset = GetFileList("./testset");
			}
			catch (DirectoryNotFoundException e)
			{
				Console.WriteLine(e);
			}
			return testset;
		}

        List<string> GetFileList(string path)
        {
            List<string> fileList = new List<string>();
            string pwd = Directory.GetCurrentDirectory();
            Console.WriteLine(pwd);

            if (!Directory.Exists(path))
            {
                throw new DirectoryNotFoundException();
            }

            foreach (string fname in Directory.GetFiles(path, "*.pow"))
            {
                fileList.Add(fname);
            }

            return fileList;
        }
    }
}
