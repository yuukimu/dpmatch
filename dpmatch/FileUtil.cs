using System;
using System.Collections.Generic;
using System.IO;

namespace dpmatch
{
    public class FileUtil
    {

        public List<string> GetFileList(string path) {
            List<string> fileList = new List<string>();
            string pwd = Directory.GetCurrentDirectory();
            Console.WriteLine(pwd);

            if(!Directory.Exists(path)) {
                throw new DirectoryNotFoundException();
            }

            foreach (string fname in Directory.GetFiles(path, "*.pow")){
                fileList.Add(fname);
            }

            return fileList;
        }


        public List<double[]> ReadEachLine(string path) {
            System.Text.RegularExpressions.Regex splitReg = new System.Text.RegularExpressions.Regex(@"\t+");
            List<double[]> lines = new List<double[]>();
            StreamReader file = new StreamReader(path);
            string line;
            string[] values;
            while ((line = file.ReadLine()) != null)
            {
                values = splitReg.Split(line);
                Console.WriteLine(line);
                double[] tmp = { double.Parse(values[0]), double.Parse(values[1]) };
                lines.Add(tmp);
            }
            file.Close();
            return lines;
        }

    }
}
