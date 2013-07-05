/*
 * Author Luke Campbell <LCampbell@ASAScience.com>
 * netcdf4.test.unittest
 */

using System;
using System.Collections.Generic;

namespace netcdf4.test {
    class UnitTest {
        public delegate bool TestCase();
        private List<TestCase> tests;

        public UnitTest() {
            tests = new List<TestCase>();
        }

        protected void addTest(TestCase a) {
            tests.Add(a);
        }

        public virtual void SetUp() {
        }

        public bool Run() {
            bool passing = true;
            int i=0;
            foreach(TestCase a in tests) {
                bool r = a();
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

    }
}





