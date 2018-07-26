using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class frmCuentas : Form
    {
        List<Cuentas> lstListadoCuentas;
        public frmCuentas()
        {
            InitializeComponent();
            lstListadoCuentas = new List<Cuentas>();
        }

        private void frmCuentas_Load(object sender, EventArgs e)
        {
            //lstListadoCuentas.Add(new Cuentas { NumCuenta = "2020" });
            //lstListadoCuentas.Add(new Cuentas { NumCuenta = "12" });
            //lstListadoCuentas.Add(new Cuentas { NumCuenta = "30221" });
            lstListadoCuentas.Add(new Cuentas { NumCuenta = "11" });

            dataGridView1.DataSource = lstListadoCuentas;
        }

        private void btnAgregarCuenta_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtCuenta.Text))
            {
                if (verificarCuenta(this.txtCuenta.Text))
                {
                    dataGridView1.DataSource = null;
                    lstListadoCuentas.Add(new Cuentas { NumCuenta = this.txtCuenta.Text });
                    dataGridView1.DataSource = lstListadoCuentas;
                }
            }
        }

        private bool verificarCuenta(string numero)
        {
            int tamCuenta = 0;
            List<Cuentas> lstResultado = lstListadoCuentas.OrderBy(d => d.NumCuenta).Where(r =>
            {
                tamCuenta = r.NumCuenta.Length < numero.Length ? r.NumCuenta.Length : numero.Length;

                if (r.NumCuenta.Length < tamCuenta)
                    return false;
                else

                    return r.NumCuenta.Substring(0, tamCuenta).StartsWith(numero.Substring(0, tamCuenta));
            }).ToList();


            if (lstResultado.Count > 0)
            {
                MessageBox.Show(string.Format("La cuenta ingresa incumple la norma con las siguintes cuentas ingresadas: {0}", string.Join(",", lstResultado.Select(d => d.NumCuenta))));
                return false;
            }
            else
                return true;

        }
    }

    public class Cuentas
    {
        public string NumCuenta { get; set; }
    }
}
