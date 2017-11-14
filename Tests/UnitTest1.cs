using System;
using NUnit.Framework;
using CaclClient;
using System.Threading.Tasks;
using HttpMock;

namespace Tests
{
    [TestFixture]
    public class UnitTest1
    {
        CalcClient client = null;
        IHttpServer mockServer;
        string uri = "http://localhost:2345";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            mockServer = HttpMockRepository.At(uri);
        }

        [SetUp]
        public void SetUp()
        {
            client = new CalcClient(uri);
        }

        [Test]
        [TestCase(1, 2, '+', "3")]
        [TestCase(12, 2, '-', "10")]
        [TestCase(7, 2, '*', "14")]
        [TestCase(6, 2, '/', "3")]
        public void TestCalculate(double x, double y, char op, string res)
        {
            mockServer.Stub(z => z.Get("/*"))
                    .Return(res)
                    .OK();
            Task.Run(() =>
            {
                double result = Task.Run(() => client.Calculate(x, y, op)).Result;
                return result;
            }).ContinueWith((e) => { Assert.AreEqual(res, e); });           
        }
    }
}
