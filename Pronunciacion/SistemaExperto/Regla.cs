using System;
using System.Collections.Generic;
using System.Text;

namespace Pronunciacion.SistemaExperto
{
    public class Regla
    {
        // Antecedentes: List<hecho>
        // Consecuencias: hecho
        List<Clausula> _antecedentes = new List<Clausula>();
        Clausula _consecuente = null;
        bool _disparada = false;
        string _nombre;
        int _indiceAntecedente = 0;
        // para el forward.
        int _prioridad= 0;
        
        //Vacio porsia
        public Regla()
        {

        }

        public Regla(string nombre)
        {
            _nombre = nombre;
        }

        public string GetNombre()
        {
            return _nombre;
        }

        public void Disparar()
        {
            _disparada = true;
        }

        public bool IsDisparada()
        {
            return _disparada;
        }

        public void AddAntecedentes(Clausula antecedente)
        {
            _antecedentes.Add(antecedente);
        }

        public void SetConsecuente(Clausula consecuente)
        {
            _consecuente = consecuente;
        }

        public Clausula GetConsecuente()
        {
            return _consecuente;
        }

        public void SetPrioridad(int prioridad)
        {
            _prioridad = prioridad;
        }

        public int GetPrioridad()
        {
            return _prioridad;
        }

        public void PrimerAntecedente()
        {
            _indiceAntecedente = 0;
        }

        public bool TieneMasClausulas()
        {
            return _indiceAntecedente < _antecedentes.Count;
        }

        public Clausula SiguienteClausula()
        {
            Clausula clausula = _antecedentes[_indiceAntecedente];
            _indiceAntecedente++;
            return clausula;
        }

        public override string ToString()
        {
            string concatenacionAntecedentes = "[ ";
            foreach(var antecedentes in _antecedentes)
            {
                concatenacionAntecedentes += "(" +antecedentes.ToString() + ") ";
            }
            concatenacionAntecedentes += " ]";
            return _nombre + ":  IF " + concatenacionAntecedentes + "THEN " + _consecuente.ToString();
        }
    }
}
