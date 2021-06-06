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
    public partial class frmVista_Trabajador : Form
    {
        public frmVista_Trabajador()
        {
            InitializeComponent();
        }
        private void OcultarColumnas()
        {
            this.dataListado.Columns[0].Visible = false;
            this.dataListado.Columns[1].Visible = false;
            this.dataListado.Columns[4].Visible = false;
            this.dataListado.Columns[5].Visible = false;
            this.dataListado.Columns[6].Visible = false;
            this.dataListado.Columns[7].Visible = false;
            this.dataListado.Columns[8].Visible = false;
            this.dataListado.Columns[9].Visible = false;
            this.dataListado.Columns[11].Visible = false;
            this.dataListado.Columns[12].Visible = false;
            this.dataListado.Columns[13].Visible = false;
            
        }
        private void Mostrar()
        {
            this.dataListado.DataSource = NTrabajador.Mostrar();
            this.OcultarColumnas();
            lblTotal.Text = "Total Registros: " + Convert.ToString(dataListado.Rows.Count);
        }
        private void BuscarApellidos()
        {
            this.dataListado.DataSource = NTrabajador.BuscarApellidos(this.txtBuscar.Text);
            this.OcultarColumnas();
            lblTotal.Text = "Total Registros: " + Convert.ToString(dataListado.Rows.Count);
        }
        private void BuscarNum_Agente()
        {
            this.dataListado.DataSource = NTrabajador.BuscarNum_Agente(this.txtBuscar.Text);
            this.OcultarColumnas();
            lblTotal.Text = "Total Registros: " + Convert.ToString(dataListado.Rows.Count);
        }


        private void frmVista_Trabajador_Load(object sender, EventArgs e)
        {
            this.Mostrar();   
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (cbBuscar.Text.Equals("Apellidos"))
            {
                this.BuscarApellidos();
            }
            else if (cbBuscar.Text.Equals("Numero de Agente"))
            {
                this.BuscarNum_Agente();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (cbBuscar.Text.Equals("Apellidos"))
            {
                this.BuscarApellidos();
            }
            else if (cbBuscar.Text.Equals("Documento"))
            {
                this.BuscarNum_Agente();
            }
        }

        private void dataListado_DoubleClick(object sender, EventArgs e)
        {
            frmVenta form = frmVenta.GetInstancia();
            string par1, par2, par3, par4;
            par1 = Convert.ToString(this.dataListado.CurrentRow.Cells["idtrabajador"].Value);
            par2 = Convert.ToString(this.dataListado.CurrentRow.Cells["nombre"].Value);
            par3 = Convert.ToString(this.dataListado.CurrentRow.Cells["apellidos"].Value);
            par4 = Convert.ToString(this.dataListado.CurrentRow.Cells["num_agente"].Value);
            form.setTrabajador(par1, par2, par3, par4);
            this.Hide();
        }

        private void dataListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
