using System;
using System.Collections.Generic;
using System.Text;

namespace Pronunciacion.SistemaExperto
{
    public class MemoriaTrabajo
    {
        List<Clausula> _hechos = new List<Clausula>();

        public MemoriaTrabajo()
        {
        }

        public void AddHecho(Clausula hecho)
        {
            _hechos.Add(hecho);
        }
        
        public List<Clausula>  GetHechos()
        {
            return _hechos;
        }

        public void ClearHechos()
        {
            _hechos.Clear();
        }

        public bool BuscarHecho(Clausula antecedente )
        {
            bool encontrado = false;
            foreach (var clausula in _hechos)
            {
                if (clausula.GetVariable() == antecedente.GetVariable() && clausula.GetValor() == antecedente.GetValor())
                {
                    encontrado = true;
                }
            }
            return encontrado;
        } 

    }
}
