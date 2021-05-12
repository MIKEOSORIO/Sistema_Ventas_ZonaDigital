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

    public partial class frmLogin : Form
    {

        public frmLogin()
        {

            InitializeComponent();
            lblHora.Text = DateTime.Now.ToString();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void uI_ButtonFlat1_Click(object sender, EventArgs e)
        {

        }

        private void uI_ButtonImage1_Click(object sender, EventArgs e)
        {
            frmPrincipal frm = new frmPrincipal();
            frm.Show();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToString();
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void BtnIngresar_Click(object sender, EventArgs e)
        {
            DataTable Datos = NTrabajador.Login(this.TxtUsuario.Text, this.TxtPassword.Text);
            //Evaluamos si no existen los Datos
            if (Datos.Rows.Count == 0)
            {
                MessageBox.Show("No Tiene Acceso al Sistema", "Sistema Ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                frmPrincipal frm = new frmPrincipal();
                frm.Idtrabajador = Datos.Rows[0][0].ToString();
                frm.Apellidos = Datos.Rows[0][1].ToString();
                frm.Nombre = Datos.Rows[0][2].ToString();
                frm.Acceso = Datos.Rows[0][3].ToString();

                frm.Show();
                this.Hide();
            }
        }

        private void lblHora_Click(object sender, EventArgs e)
        {

        }
    }
}

