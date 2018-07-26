using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fachada;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Fachada.Fachada fachada = new Fachada.Fachada();
        private event Entidades.Entidades.BarraProgreso miEvento;
        private delegate void detallePorcentaje(int porcentaje, string mensaje);
        private Thread mihilo;

        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> Meses = new List<string>{
                "Enero",
                "Febrero",
                "Marzo",
                "Abril",
                "Mayo"
            };

            comboBox1.DataSource = Meses.Select((r, i) => new KeyValuePair<int, string>(i + 1, r)).ToList();
            comboBox1.DisplayMember = "value";
            comboBox1.ValueMember = "key";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(comboBox1.SelectedValue.ToString());
        }

        private void btnInicializacion_Click(object sender, EventArgs e)
        {
            Lazy<Employee> lstEmpleado = new Lazy<Employee>();

            //Initializing a value with a big computation, computed in parallel
            //Lazy<int> _data = new Lazy<int>(delegate
            //{
            //    return ParallelEnumerable.Range(0, 1000).
            //        Select(i => Compute(i)).Aggregate((x,y) => x + y);
            //}, LazyExecutionMode.EnsureSingleThreadSafeExecution);


        }

        private void MostrarDetalleProgreso(string valor)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string consulta = this.txtConsulta.Text;
            int indice = 0;
            int indiceJoin = 0;
            int indiceFrom = 0;
            int enviarIndice = 0;

            HashSet<string> lstTablas = new HashSet<string>();
            Func<string, HashSet<string>> CargarTablas = consul =>
            {
                lstTablas.Add(consul);
                return lstTablas;
            };


            consulta.Split(' ').Select(r =>
            {
                indice += (r.Length + 1);
                if (r.ToUpper().Equals("FROM") || r.ToUpper().Equals("JOIN"))
                {
                    indiceJoin = consulta.IndexOf("Join", indice);
                    indiceFrom = consulta.IndexOf("from", indice);
                    enviarIndice = indiceFrom < 0 ? indiceJoin - indice : indiceFrom < indiceJoin ? indiceFrom - indice : indiceJoin < 0 ? indiceFrom : indiceJoin < indiceFrom ? indiceJoin : -1;

                    if (enviarIndice > 0)
                        CargarTablas(consulta.Substring(indice, enviarIndice));
                    else
                        lstTablas.Add(consulta.Substring(indice, 4));
                }
                return r;
            }).ToList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ThreadStart delegadoPagos = new ThreadStart(iniciarPagos);
            mihilo = new Thread(delegadoPagos);
            mihilo.Start();
        }

        private void iniciarPagos()
        {
            miEvento += CargarBarra;
            fachada.AplicarOperacion(miEvento);
        }

        private void LlenarBarra(int porcentaje, string mensaje)
        {
            this.progressBar1.Value = porcentaje;
            this.txtPorcentaje.Text = porcentaje.ToString() + "% --" + mensaje;
        }

        private void CargarBarra(int porcentaje, string mensaje)
        {

            if (this.progressBar1.InvokeRequired)
            {
                detallePorcentaje delegado = new detallePorcentaje(LlenarBarra);
                this.progressBar1.BeginInvoke(delegado, new object[] { porcentaje, mensaje });
                this.txtPorcentaje.BeginInvoke(delegado, new object[] { porcentaje, mensaje });
            }
            else
            {
                this.progressBar1.Value = porcentaje;
                this.txtPorcentaje.Text = porcentaje.ToString() + "% --" + mensaje;
            }
        }
    }
}
