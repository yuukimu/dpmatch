using System;
using System.Collections.Generic;

namespace dpmatch
{
    public class DPMatching
    {
        private List<double[]> file;
        public DPMatching(List<double[]> file)
        {
            this.file = file;
        }

        public void matchingByPower() {
			foreach (var item in file)
			{
				Console.WriteLine(item[0]);
			}
        }

		public void matchingByDcepstrum()
		{
			foreach (var item in file)
			{
				Console.WriteLine(item[1]);
			}
		}
    }
}
