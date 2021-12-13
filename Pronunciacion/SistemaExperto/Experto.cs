using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Pronunciacion.SistemaExperto
{
    public class Experto
    {
        MotorInferencia _motor = new MotorInferencia();
        MemoriaTrabajo _memoria = new MemoriaTrabajo();

        public Experto()
        {
        }

        public void InicializarMemoria(List<string> modosCorregidos)
        {
            List<string> corregidoSinDuplicados = modosCorregidos.Distinct().ToList();
            foreach (var modo in corregidoSinDuplicados)
            {
                _memoria.AddHecho(new Clausula(modo, "corregir"));
            }
            _motor.AddMemoriaTrabajo(_memoria);
        }

        public string GetInferencia()
        {
            return _motor.ForwardChaining().ToString();
        }

        public List<string> GetPensamiento()
        {
            return _motor.GetRazonamiento();
        }
    }
}
