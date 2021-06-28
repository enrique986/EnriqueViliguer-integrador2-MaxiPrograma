using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using negocio;

namespace COMERCIO
{
    public partial class FrmComercio : Form
    {
        private List<Producto> listaProductos;
        public FrmComercio()
        {
            InitializeComponent();
        }

        private void FrmComercio_Load(object sender, EventArgs e)
        {
           
            try 
            { 
                
                ProductoNegocio productoNegocio = new ProductoNegocio();
               
                listaProductos = productoNegocio.Listar();
               
                 dgvProducto.DataSource = listaProductos;
            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
     
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAgregar agregar = new frmAgregar();
            agregar.ShowDialog();
            FrmComercio_Load(sender, e);
        }

        private void dgvProducto_MouseClick(object sender, MouseEventArgs e)
        {
            Producto seleccion = (Producto)dgvProducto.CurrentRow.DataBoundItem;
            RecargarImagen(seleccion.UrlImagen);
            lblDescripcion.Text = seleccion.Descripcion;
        }
        private void RecargarImagen(string img)
        {
            try
            {
                if(img !="")
                {
                    pbxProductos.Load(img);
                }
            }
            catch(Exception)
            {
                MessageBox.Show("no se pudo cargar la imagen ingresar url valida por favor");
            }

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Producto unproducto = (Producto)dgvProducto.CurrentRow.DataBoundItem;
            frmAgregar modificar = new frmAgregar(unproducto);
            modificar.ShowDialog();
            FrmComercio_Load(sender,e);


        }

        private void btnDetalle_Click(object sender, EventArgs e)
        {
            Producto unproducto = (Producto)dgvProducto.CurrentRow.DataBoundItem;
            frmAgregar Detalle = new frmAgregar(unproducto,true);
            Detalle.ShowDialog();
            FrmComercio_Load(sender, e);
            
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Producto unproducto = (Producto)dgvProducto.CurrentRow.DataBoundItem;

            ProductoNegocio Negocio = new ProductoNegocio();
             try
             {
                if (MessageBox.Show("el elemento seleccionado sera eliminado...", "El elemento:" + unproducto.Nombre, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) 

                {
                    Negocio.Eliminar(unproducto.Id);
                    MessageBox.Show("Eliminado Correctamente");
                    FrmComercio_Load(sender,e );
                }
             }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString()); 
            } 
            
            


        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtBuscar.Text!="")
                {
                    //filtro
                    List<Producto> listaFiltrada = listaProductos.FindAll(x => x.Nombre.ToUpper().Contains(txtBuscar.Text.ToUpper()) || x.marca.Descripcion.ToUpper().Contains(txtBuscar.Text.ToUpper()));
                    dgvProducto.DataSource = listaFiltrada;
                    //quitar campos de la grilla
                    quitarCampos();
                }
                else 
                {
                    dgvProducto.DataSource = null;
                    dgvProducto.DataSource = listaProductos;
                    quitarCampos();
                }
            }
            catch(Exception)
            {
                MessageBox.Show("No hay resultados para la busqueda deseada");
                dgvProducto.DataSource = null;
                dgvProducto.DataSource = listaProductos;
                txtBuscar.Focus();

            }

        }
        private void quitarCampos()
        //quitar campos de mi grilla
        {
            dgvProducto.Columns["IdArticulo"].Visible = false;
            dgvProducto.Columns["Descripcion"].Visible = false;
            dgvProducto.Columns["Imagen"].Visible = false;

            dgvProducto.CurrentCell = dgvProducto.Rows[0].Cells[1];
            Producto seleccion = (Producto)dgvProducto.CurrentRow.DataBoundItem;
            RecargarImagen(seleccion.UrlImagen);
            lblDescripcion.Text = seleccion.Descripcion;
        }
    }
}
