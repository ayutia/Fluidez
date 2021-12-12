using System;
using System.Collections.Generic;
using System.Text;

namespace Pronunciacion.SistemaExperto
{
    public class MotorInferencia
    {
        // las iniciare aqui aver
        List<Regla> _reglas = new List<Regla>();
        MemoriaTrabajo _memoria = new MemoriaTrabajo();
        List<string> _pensamiento = new List<string>();

        public MotorInferencia()
        {
            Console.WriteLine("entre motor constri");
            Conocimiento conocimientoAdquirido = new Conocimiento();
            AddRegla(conocimientoAdquirido.GetConocimiento());
        }

        public void AddRegla(Regla regla)
        {
            _reglas.Add(regla);
        }

        public void AddRegla(List<Regla> reglas)
        {
            _reglas.AddRange(reglas);
        }

        public void AddMemoriaTrabajo(MemoriaTrabajo memoria)
        {
            _memoria = memoria;
        }

        // Inferencia que requiere una base de hechos completa antes de 
        // ser ejecutada.
        public Clausula ForwardChaining()
        {
            Clausula conclusion = null;
            Regla reglaSeleccionada = new Regla();
            List<Regla> candidatas = new List<Regla>();
            bool huboDisparo = true;
            // Termino si aplique todas 
            // Ver como terminar si no me quedan mas
            while (GetReglasSinDisparar() > 0 && huboDisparo)
            {
                Console.WriteLine("Entre primer While");
                huboDisparo = false;
                foreach (var regla in _reglas)
                {
                    Console.WriteLine("Entre primer Foreach");
                    if (regla.IsDisparada())
                    {
                        // Si la regla se despiro, entonces saltar
                        continue;
                    }
                    regla.PrimerAntecedente();
                    while (regla.TieneMasClausulas())
                    {
                        // Arreglando aca termino.
                        if (_memoria.BuscarHecho(regla.SiguienteClausula()))
                        {
                            // pregunto si tiene mas clausulas, si no, agregar
                            if (regla.TieneMasClausulas())
                            {
                                continue;
                                // si me da un no, agregar?
                                //seguir en el loop
                            }
                            else
                            {
                                if (regla.GetPrioridad() == 1)
                                {
                                    // si cogi la regla con prioridad 1
                                    // Agregar su consecuente a la memoria
                                    // disparar
                                    // retornar su clausula
                                    _memoria.AddHecho(regla.GetConsecuente());
                                    regla.Disparar();
                                    _pensamiento.Add(regla.ToString());
                                    return regla.GetConsecuente();
                                }
                                // Añadir la regla a las posibles
                                candidatas.Add(regla);
                            }
                        }
                        else
                        {
                            //no hace match salir del loop
                            break;
                        }

                    }// End While de las clausulas x regla

                }
                // Fin Foreach reglas, seleccionar, disparar y vaciar candidatas
                Console.WriteLine(candidatas.Count);
                if (candidatas.Count > 0)
                {
                    //Ver cual esta seleccionando
                    reglaSeleccionada = GetReglaPrioritaria(candidatas);
                    Console.WriteLine(reglaSeleccionada.GetConsecuente().ToString());
                    if (reglaSeleccionada.GetPrioridad() == 1)
                    {
                        return reglaSeleccionada.GetConsecuente();
                    }
                    reglaSeleccionada.Disparar();
                    _pensamiento.Add(reglaSeleccionada.ToString());
                    _memoria.AddHecho(reglaSeleccionada.GetConsecuente());
                    huboDisparo = true;
                    candidatas.Clear();
                }
            }
            conclusion = reglaSeleccionada.GetConsecuente();
            // poner como conclu la regla con mayor prioridad.
            Console.WriteLine("");
            return conclusion;
        }

        private int GetReglasSinDisparar()
        {
            int restantes = _reglas.Count;
            foreach(var regla in _reglas)
            {
                if (regla.IsDisparada())
                {
                    restantes--;
                }
            }
            return restantes;
        }

        public void ClearReglas()
        {
            _reglas.Clear();
        }

        public Regla GetReglaPrioritaria(List<Regla> reglas)
        {
            Regla auxRegla = null;
            int prioridad = 6;
            foreach(var regla in reglas)
            {
                // Para terminar
                if (regla.GetPrioridad() == 1)
                {
                    return regla;
                }
                if (prioridad > regla.GetPrioridad())
                {
                    prioridad = regla.GetPrioridad();
                    auxRegla = regla;
                }

            }
            return auxRegla;
        }

        public List<string> GetRazonamiento()
        {
            return _pensamiento;
        }

        public List<Regla> GetReglas()
        {
            return _reglas;
        }
    }
}
