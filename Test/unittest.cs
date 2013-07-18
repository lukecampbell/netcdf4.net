/*
 * Author Luke Campbell <LCampbell@ASAScience.com>
 * netcdf4.test.unittest
 */

using System;
using System.Collections.Generic;

namespace ASA.NetCDF4.Test {
    public class UnitTest {
        public delegate bool TestCase();
        private List<TestCase> tests;
        private List<string> names;

        public UnitTest() {
            tests = new List<TestCase>();
            names = new List<string>();

        }

        protected void AddTest(TestCase a, string name="") {
            tests.Add(a);
            if(String.IsNullOrWhiteSpace(name)) {
                names.Add("empty");
            } else {
                names.Add(name);
            }
        }

        public virtual void SetUp() {
        }

        public bool Run() {
            bool passing = true;
            int i=0;
            foreach(TestCase a in tests) {
                bool r;
                try {
                    r = a();
                } catch (AssertFailedException e) {
                    Console.WriteLine(String.Format("{0,-30} {1:-30}", names[i], "FAIL"));
                    Console.WriteLine(e.ToString());
                    passing &= false;
                    i++;
                    continue;
                }
                if(r)
                    Console.WriteLine(String.Format("{0,-30} {1:-30}", names[i], "OK"));
                else
                    Console.WriteLine(String.Format("{0,-30} {1:-30}", names[i], "FAIL"));
				passing &= r;
                i++;
			}
            Console.WriteLine(String.Format(" - {0,-27} {1:-30}", GetType().Name, passing ? "SUCCESS" : "FAIL"));
            Console.WriteLine("");
			return passing;
        }

        public void CheckDelete(string filePath) {
            TestHelper.CheckDelete(filePath);
        }
    }
}





