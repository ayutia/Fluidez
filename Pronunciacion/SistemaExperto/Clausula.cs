using System;
using System.Collections.Generic;
using System.Text;

namespace Pronunciacion.SistemaExperto
{
    // Ya Sebastian, vamooos
    public class Clausula
    {
        private string _variable;
        private string _valor;

        public Clausula()
        {

        }

        public Clausula(string variable, string valor)
        {
            _variable = variable;
            _valor = valor;
        }

        public string GetVariable()
        {
            return _variable;
        }

        public string GetValor()
        {
            return _valor;
        }

        public override string ToString()
        {
            return _variable + " " +_valor;
        }
    }
}
