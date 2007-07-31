using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Tyng.Text.Phonetics;

namespace Tyng.Text.Tests
{
    [TestFixture]
    public class PhoneticsTests
    {
        [Test]
        public void MetaphoneTest()
        {
            DoMetaphoneTest("ANASTHA", "ANS0");
            DoMetaphoneTest("DAVIS-CARTER","TFSKRTR");
            DoMetaphoneTest("ESCARMANT","ESKRMNT");
            DoMetaphoneTest("MCCALL","MKKL");
            DoMetaphoneTest("MCCROREY","MKKRR");
            DoMetaphoneTest("MERSEAL","MRSL");
            DoMetaphoneTest("PIEURISSAINT","PRSNT");
            DoMetaphoneTest("ROTMAN","RTMN");
            DoMetaphoneTest("SCHEVEL","SXFL");
            DoMetaphoneTest("SCHROM","SXRM");
            DoMetaphoneTest("SEAL","SL");
            DoMetaphoneTest("SPARR","SPR");
            DoMetaphoneTest("STARLEPER","STRLPR");
            DoMetaphoneTest("THRASH", "0RX");
        }

        private void DoMetaphoneTest(string name, string expected)
        {
            Assert.AreEqual(expected, Metaphone.Get(name), name);
        }
    }
}
