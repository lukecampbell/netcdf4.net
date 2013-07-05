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
            foreach(TestCase a in tests) {
                bool r = a();
                if(r)
                    Console.WriteLine("ok");
                else
                    Console.WriteLine("Fail");
				passing &= r;
			}
			return passing;
        }

    }
}





