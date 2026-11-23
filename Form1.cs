using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaDePrestamos
{
    public partial class Form1 : Form
    {

        double prestamos, tasaINT, numerosP, mensualP, totalP;

        private void txtCodigoF_TextChanged(object sender, EventArgs e)
        {
            string s = txtCodigoF.Text.Replace("-", "");

            if (s.Length > 6)
            {
                s = s.Substring(0, 6);
            }

            switch (s.Length)
            {
                case 0:
                case 1:
                case 2:
                    txtCodigoF.Text = s;
                    break;

                case 3:
                case 4:
                    txtCodigoF.Text = s;
                    break;

                case 5:
                case 6:
                    txtCodigoF.Text = s.Substring(0, 2) + "-" +
                                      s.Substring(2, 2) + "-" +
                                      s.Substring(4);

                    break;

            }

            txtCodigoF.SelectionStart = txtCodigoF.Text.Length;
        }

        private void btnPRESTAMOS_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Por favor introduzca el nombre", "Sistema de Gestion",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNombre.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDireccion.Text))
            {
                MessageBox.Show("Por favor introduzca la direccion", "Sistema de Gestion",
                   MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDireccion.Focus();
                return;
            }

            if (!long.TryParse(txtCuentaN.Text, out _))
            {

                MessageBox.Show("Introduzca un numero de cuenta valido", "Sistema de Gestion",
                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCuentaN.Clear();
                txtCuentaN.Focus();
               

                return;
            }

            if (!double.TryParse(txtMontoP.Text, out double loan))
            {

                MessageBox.Show("Introduzca un monto valido", "Sistema de Gestion",
                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMontoP.Clear();
                txtMontoP.Focus();

                return;
            }

            if (!double.TryParse(txtTasaINT.Text, out double TasaINT))
            {

                MessageBox.Show("Introduzca una tasa de interes valida", "Sistema de Gestion",
                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTasaINT.Clear();
                txtTasaINT.Focus();

                return;
            }

            if (!double.TryParse(txtNumerosP.Text, out double NumerosP))
            {

                MessageBox.Show("Numero de pago debe ser mayor que cero", "Sistema de Gestion",
                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNumerosP.Clear();
                txtNumerosP.Focus();

                return;
            }



            loan = Convert.ToDouble(txtMontoP.Text);
            TasaINT = Convert.ToDouble(txtTasaINT.Text);
            NumerosP = Convert.ToDouble(txtNumerosP.Text);

            if (NumerosP <= 0)
            {
                MessageBox.Show("Introduzca un numero de pago valido", "Sistema de Gestion",
                 MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNumerosP.Clear();
                txtNumerosP.Focus();

                return;
            }

            totalP = loan + ((loan * TasaINT) / 100);
            mensualP = totalP / numerosP;

            lblPagosM.Text = mensualP.ToString("C2");
            lblPagoTotal.Text = totalP.ToString("C2");

            txtMontoP.Text = loan.ToString("C2");

            txtRecibos.Text =
                "\tSistema De Gestion " + Environment.NewLine + Environment.NewLine +
                $"\tNombre: {txtNombre.Text}" + Environment.NewLine +
                $"\tDireccion: {txtDireccion.Text}" + Environment.NewLine +
                $"\tTipo de cuenta: {cboCuenta.Text}" + Environment.NewLine +
                $"\tNumero de cuenta: {txtCuentaN.Text}" + Environment.NewLine +
                $"\tCodigo fuente: {txtCodigoF.Text}" + Environment.NewLine +
                $"\tMonto del prestamo: {txtMontoP.Text}" + Environment.NewLine +
                $"\tTasa de Interes: {txtTasaINT.Text}%" + Environment.NewLine +
                $"\t:Numeros de pagos: {txtNumerosP.Text}" + Environment.NewLine +
                $"\tPagos mensuales: {lblPagosM.Text}" + Environment.NewLine +
                Environment.NewLine +
                $"\t---------------------------------" + Environment.NewLine +
                $"\tTOTAL DE PAGO" + Environment.NewLine +
                $"\t---------------------------------" + Environment.NewLine +
                $"\tTotal pagos: {lblPagosT.Text}" + Environment.NewLine +
                Environment.NewLine +
                $"\tDate: {DateTime.Now:dd/mm/yyyy}";

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult iExit;

            iExit = MessageBox.Show("Confirma si desea salir?", "Sistema de Gestion",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if(iExit == DialogResult.Yes)
            {
                Close();
            }
        }

        private void btnRestablecer_Click(object sender, EventArgs e)
        {
            cboCuenta.Text = "Seleccione el tipo de cuenta";

            txtNombre.Text = "";
            txtDireccion.Text = "";
            txtCuentaN.Text = "";
            txtMontoP.Text = "";
            txtTasaINT.Text = "";
            txtNumerosP.Text = "";
            txtRecibos.Text = "";
            txtCodigoF.Text = "";
            lblPagosM.Text = "";
            lblPagosT.Text = "";

            txtNombre.Focus(); 
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cboCuenta.Text = "Seleccione el tipo de cuenta";

            cboCuenta.Items.Add("Cuenta corriente");
            cboCuenta.Items.Add("Cuenta ahorro");
            cboCuenta.Items.Add("Cuenta deposito");
            cboCuenta.Items.Add("Cuenta conjunta");
        }
    }
}
