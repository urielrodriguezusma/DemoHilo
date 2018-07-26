using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Data;


namespace Fachada
{
    public class Fachada
    {

        public void AplicarOperacion(Entidades.Entidades.BarraProgreso miBarra)
        {
            Facturas factura = new Facturas();
            factura.AplicarOperacion(miBarra);
        }

    }
}
