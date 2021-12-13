using System;
using System.Collections.Generic;
using System.Text;

namespace Pronunciacion.SistemaExperto
{
    public class MotorInferencia
    {
        List<Regla> _reglas = new List<Regla>();
        MemoriaTrabajo _memoria = new MemoriaTrabajo();
        List<string> _pensamiento = new List<string>();

        public MotorInferencia()
        {
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

        public Clausula ForwardChaining()
        {
            Clausula conclusion = null;
            Regla reglaSeleccionada = new Regla();
            List<Regla> candidatas = new List<Regla>();
            bool huboDisparo = true;
            while (GetReglasSinDisparar() > 0 && huboDisparo)
            {
                huboDisparo = false;
                foreach (var regla in _reglas)
                {
                    if (regla.IsDisparada())
                    {
                        continue;
                    }
                    regla.PrimerAntecedente();
                    while (regla.TieneMasClausulas())
                    {
                        if (_memoria.BuscarHecho(regla.SiguienteClausula()))
                        {
                            if (regla.TieneMasClausulas())
                            {
                                continue;
                            }
                            else
                            {
                                if (regla.GetPrioridad() == 1)
                                {
                                    _memoria.AddHecho(regla.GetConsecuente());
                                    regla.Disparar();
                                    _pensamiento.Add(regla.ToString());
                                    return regla.GetConsecuente();
                                }
                                candidatas.Add(regla);
                            }
                        }
                        else
                        {
                            break;
                        }
                    }

                }
                if (candidatas.Count > 0)
                {
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
