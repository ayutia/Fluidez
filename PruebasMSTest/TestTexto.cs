using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pronunciacion;

namespace PruebasMSTest
{
    [TestClass]
    public class TestTexto
    {
        [TestMethod]
        public void TestTitulo  ()
        {
            Texto texto = new Texto("1.txt");
            string _esperado = "L�grimas de luz";
            string _obtenido = texto.GetTitulo();
            Assert.AreEqual(_esperado, _obtenido);
        }
    }
}
