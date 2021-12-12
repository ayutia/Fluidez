using System;
using System.Collections.Generic;
using System.Text;

namespace Pronunciacion.SistemaExperto
{
    public class Conocimiento
    {
        List<Regla> adquiridas = new List<Regla>();

        public Conocimiento()
        {
            Regla regla1 = new Regla("Liquida");
            regla1.SetPrioridad(1);
            regla1.SetConsecuente(new Clausula("liquida", "corregir"));
            regla1.AddAntecedentes(new Clausula("vibrante", "corregir"));
            regla1.AddAntecedentes(new Clausula("lateral", "corregir"));

            Regla regla2 = new Regla("Vibrante");
            regla2.SetPrioridad(2);
            regla2.SetConsecuente(new Clausula("vibrante", "corregir"));
            regla2.AddAntecedentes(new Clausula("vibranteSimple", "corregir"));
            regla2.AddAntecedentes(new Clausula("vibranteDoble", "corregir"));

            Regla regla3 = new Regla("Lateral");
            regla3.SetPrioridad(3);
            regla3.SetConsecuente(new Clausula("lateral", "corregir"));
            regla3.AddAntecedentes(new Clausula("palabraCorregida", "lateral"));

            Regla regla4 = new Regla("VibranteDoble");
            regla4.SetPrioridad(4);
            regla4.SetConsecuente(new Clausula("vibranteDoble", "corregir"));
            regla4.AddAntecedentes(new Clausula("palabraCorregida", "vibranteDoble"));

            Regla regla5 = new Regla("VibranteSimple");
            regla5.SetPrioridad(5);
            regla5.SetConsecuente(new Clausula("vibranteSimple", "corregir"));
            regla5.AddAntecedentes(new Clausula("palabraCorregida", "vibranteSimple"));

            adquiridas.Add(regla1);
            adquiridas.Add(regla2);
            adquiridas.Add(regla3);
            adquiridas.Add(regla4);
            adquiridas.Add(regla5);
        }

        public List<Regla> GetConocimiento()
        {
            return adquiridas;
        }
    }
}
