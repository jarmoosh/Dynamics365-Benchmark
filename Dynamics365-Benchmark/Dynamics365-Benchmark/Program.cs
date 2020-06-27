using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamics365_Benchmark
{
   public class Program
    {
        static void Main(string[] args) => BenchmarkRunner.Run<RestVsSoap>();
    }


    public class RestVsSoap
    {
        [Benchmark]
        public void CreateOrganizationService()
        {

        }

        [Benchmark]
        public void CreateRest()
        {

        }
    }
}
