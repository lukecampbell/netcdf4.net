/*
 * Author Luke Campbell <LCampbell@ASAScience.com>
 * netcdf4.test.unittest
 */

using System;
using System.Collections.Generic;

namespace netcdf4.test {
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
                    Console.Write(names[i]+"...");
                    r = a();
                } catch (AssertFailedException e) {
                    Console.WriteLine("\tFail");
                    Console.WriteLine(e.ToString());
                    passing &= false;
                    i++;
                    continue;
                }
                if(r)
                    Console.WriteLine("\tOK");
                else
                    Console.WriteLine("\tFail");
				passing &= r;
                i++;
			}
            Console.WriteLine("\nRan " + i + " Tests: " + (passing ? "SUCESS" : "FAIL"));
			return passing;
        }

        public void CheckDelete(string filePath) {
            if(System.IO.File.Exists(filePath)) {
                System.IO.File.Delete(filePath);
            }
        }
    }
}





