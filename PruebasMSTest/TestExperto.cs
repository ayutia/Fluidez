using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pronunciacion.SistemaExperto;

namespace PruebasMSTest
{
    [TestClass]
    public class TestExperto
    {
        [TestMethod]
        public void TestMemoria()
        {
            MemoriaTrabajo memoria = new MemoriaTrabajo();
            memoria.AddHecho(new Clausula("palabraCorregida", "vibranteDoble"));
            memoria.AddHecho(new Clausula("palabraCorregida", "vibranteSimple"));
            Assert.IsTrue(memoria.BuscarHecho(new Clausula("palabraCorregida", "vibranteSimple")));
        }

        [TestMethod]
        public void TestInferencia()
        {
            MemoriaTrabajo memoria = new MemoriaTrabajo();
            MotorInferencia moto = new MotorInferencia();

            memoria.AddHecho(new Clausula("palabraCorregida", "vibranteDoble"));
            memoria.AddHecho(new Clausula("palabraCorregida", "vibranteSimple"));
            memoria.AddHecho(new Clausula("palabraCorregida", "lateral"));

            moto.AddMemoriaTrabajo(memoria);
            string esperado = "liquida";
            string resultado = moto.ForwardChaining().GetVariable();
            Assert.AreEqual(esperado, resultado);
        }

    }
}
