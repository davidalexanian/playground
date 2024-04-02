namespace MassTransitProject.Tests
{
    [TestClass]
    public class UnitTest1
    {
        public readonly PrimeService _primeService = new PrimeService();

        [TestMethod]
        public void TestMethod1()
        {
        }

        [TestMethod]
        [DataRow(-1), DataRow(0), DataRow(1)]
        public void IsPrime_ValuesLessThan2_ReturnFalse(int value)
        {
           Assert.IsFalse(_primeService.IsPrime(value), $"{value} should not be prime");
        }

        [TestMethod]
        [DataRow(2), DataRow(3), DataRow(5), DataRow(7)]
        public void IsPrime_PrimesLessThan10_ReturnTrue(int value) =>
            Assert.IsTrue(_primeService.IsPrime(value), $"{value} should be prime");

        [TestMethod]
        [DataRow(4), DataRow(6), DataRow(8), DataRow(9)]
        public void IsPrime_NonPrimesLessThan10_ReturnFalse(int value) =>
            Assert.IsFalse(_primeService.IsPrime(value), $"{value} should not be prime");
    }
}