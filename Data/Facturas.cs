using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data
{
    public class Facturas
    {
        public void AplicarOperacion(Entidades.Entidades.BarraProgreso miBarra)
        {
            int porcentaje = 0;

            for (int i = 1; i <= 10; i++)
            {
                porcentaje = (100 * i) / 10;
                miBarra(porcentaje, string.Format("Procesando la factura {0}", i));
                Thread.Sleep(500);

            }
        }
    }
}
