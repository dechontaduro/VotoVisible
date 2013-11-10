using System;

namespace com.VotoVisible.Entitity
{
    /// <summary>
    /// Descripción breve de VotacionTotales
    /// </summary>
    public class VotacionTotales
    {
        public string decision;
        public int votosPublicos;
        public int votosPrivados;

        public VotacionTotales(string decision, int votosPublicos, int votosPrivados)
        {
            this.decision = decision;
            this.votosPublicos = votosPublicos;
            this.votosPrivados = votosPrivados;
        }
    }
}