using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class frmAutorizacion : Form
    {
        public frmAutorizacion()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void frmAutorizacion_Load(object sender, EventArgs e)
        {
            
        }

       
        private void button1_Click(object sender, EventArgs e)
        {
            DataTable Datos = NTrabajador.Autorizacion(this.txtAgente.Text, this.txtPassword.Text);
            //Evaluamos si no existen los Datos
            if (Datos.Rows.Count == 0)
            {
                MessageBox.Show("No Tiene Acceso al Sistema", "Sistema Ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                frmVenta formAutorizacion = frmVenta.GetInstancia();
                frmPrincipal frm = new frmPrincipal();
                frm.Idtrabajador = Datos.Rows[0][0].ToString();
                frm.Apellidos = Datos.Rows[0][1].ToString();
                frm.Nombre = Datos.Rows[0][2].ToString();
                frm.Acceso = Datos.Rows[0][3].ToString();

                string par1 = Convert.ToString(txtAgente.Text);
                formAutorizacion.setAutorizacion(par1);
                frm.Show();
                this.Hide();
            }
        }
    }
}
