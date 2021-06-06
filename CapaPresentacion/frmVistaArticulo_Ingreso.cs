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
    public partial class frmVistaArticulo_Ingreso : Form
    {
        public frmVistaArticulo_Ingreso()
        {
            InitializeComponent();
        }

        private void frmVistaArticulo_Ingreso_Load(object sender, EventArgs e)
        {
            this.Mostrar();
        }
        //Método para ocultar columnas


        //Método para ocultar columnas
        private void OcultarColumnas()
        {
            this.dataListado.Columns[0].Visible = false;
            this.dataListado.Columns[1].Visible = false;
            this.dataListado.Columns[6].Visible = false;
            this.dataListado.Columns[8].Visible = false;
        }

        //Método Mostrar
        private void Mostrar()
        {
            this.dataListado.DataSource = NArticulo.Mostrar();
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dataListado.Rows.Count);
        }

        //Método BuscarNombre
        private void BuscarNombre()
        {
            this.dataListado.DataSource = NArticulo.BuscarNombre(this.txtBuscar.Text);
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dataListado.Rows.Count);
        }

        private void BuscarCodigo()
        {
            this.dataListado.DataSource = NArticulo.BuscarCodigo(this.txtBuscar.Text);
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dataListado.Rows.Count);
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (cbBuscar.Text.Equals("Codigo"))
            {
                this.BuscarCodigo();
            }
            else if (cbBuscar.Text.Equals("Nombre"))
            {
                this.BuscarNombre();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtBuscar.Text.Equals("Codigo"))
            {
                this.BuscarCodigo();
            }
            else if (txtBuscar.Text.Equals("Nombre"))
            {
                this.BuscarNombre();
            }
        }


        private void txtBuscar_DoubleClick(object sender, EventArgs e)
        {

        }

        private void dataListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataListado_DoubleClick(object sender, EventArgs e)
        {
            frmIngreso form = frmIngreso.GetInstancia();
            string par1, par2, par3;
            par1 = Convert.ToString(this.dataListado.CurrentRow.Cells["idarticulo"].Value);
            par2 = Convert.ToString(this.dataListado.CurrentRow.Cells["nombre"].Value);
            par3 = Convert.ToString(this.dataListado.CurrentRow.Cells["codigo"].Value);
          
            form.setArticulo(par1, par2, par3);
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmArticulo frm = frmArticulo.GetInstancia();
            frm.Show();
        }
    }
}
