using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Pronunciacion.SistemaExperto
{
    public class Experto
    {
        MotorInferencia motor = new MotorInferencia();
        MemoriaTrabajo memoriaTrabajo = new MemoriaTrabajo();

        public Experto()
        {

        }

        // añadir aca la memoria de trabajo y pasarsela al motor
        public void InicializarMemoria(List<string> modosCorregidos)
        {
            List<string> corregidoSinDuplicados = modosCorregidos.Distinct().ToList();
            foreach (var modo in corregidoSinDuplicados)
            {
                //Console.WriteLine("entre");
                memoriaTrabajo.AddHecho(new Clausula(modo, "corregir"));
            }
            motor.AddMemoriaTrabajo(memoriaTrabajo);
        }

        public string GetInferencia()
        {
            //Console.WriteLine(memoriaTrabajo.GetHechos().Count());
            //Console.WriteLine(motor.GetReglas().Count());
            //return motor.ForwardChaining().GetValor();
            return motor.ForwardChaining().ToString();
        }

        public List<string> GetPensamiento()
        {
            return motor.GetRazonamiento();
        }
    }
}
