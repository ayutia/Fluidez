using System;
using System.Collections.Generic;
using System.Text;

namespace Pronunciacion.SistemaExperto
{
    public class MemoriaTrabajo
    {
        List<Clausula> hechos = new List<Clausula>();

        public MemoriaTrabajo()
        {
            
        }

        public void AddHecho(Clausula hecho)
        {
            hechos.Add(hecho);
        }
        
        public List<Clausula>  GetHechos()
        {
            return hechos;
        }

        public void ClearHechos()
        {
            hechos.Clear();
        }

        public bool BuscarHecho(Clausula antecedente )
        {
            bool encontrado = false;
            foreach (var clausula in hechos)
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
