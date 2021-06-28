using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using negocio;
using dominio;

namespace COMERCIO
{
    public partial class frmAgregar : Form
    {
        private Producto producto = null;
        private bool detalle = false;
        
        //sobrecarga agregar
        public frmAgregar()
        {
            InitializeComponent();
            Text = "agregar Producto";
        }
        
        //sobrecarga modificar
        public frmAgregar(Producto producto)         
        {
                InitializeComponent();
                this.producto = producto;
                Text = "modificar producto";
        }

        public frmAgregar (Producto producto, bool detalle)//sobre carga y bandera bool
        {
            InitializeComponent();
            this.producto = producto;
            this.detalle = detalle;
            Text = "Detalle Producto "; 
                
        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Estas seguro de salir?Perderas los datos ingresados","Salir",MessageBoxButtons.YesNo,MessageBoxIcon.Warning) == DialogResult.No)
                return;
            Dispose();
        }

        private void frmAgregar_Load(object sender, EventArgs e)
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            //combo box Categoria
            cboCategoria.DataSource = categoriaNegocio.Listar();
            cboCategoria.ValueMember = "Id";
            cboCategoria.DisplayMember = "Descripcion";
            //combo boc Marca
            cboMarca.DataSource = marcaNegocio.Listar();
            cboMarca.ValueMember = "Id";
            cboMarca.DisplayMember = "Descripcion";

            try
            {
                if(producto !=null)//llego de modificar
                {
                    txtCodigo.Text = producto.Codigo;//me tira error 
                    txtNombre.Text = producto.Nombre;
                    txtDescripcion.Text = producto.Descripcion;
                    txtImagen.Text = producto.UrlImagen;
                    txtPrecio.Text = producto.Precio.ToString();
                    cboMarca.SelectedValue = producto.marca.Descripcion;
                    cboCategoria.SelectedValue = producto.categoria.Descripcion;
                    RecargarImagen(producto.UrlImagen);
                }
               
            } 
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            if (detalle == true)
            {
                gbxProducto.Enabled = false;
                btnAceptar.Visible = false;
                btnCancelar.Visible = false;

            }

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
           
            
            try
            { 
                ProductoNegocio negocio = new ProductoNegocio();
                
                if(producto == null)
                {
                    
                    producto  = new Producto();
                }
                producto.Codigo = txtCodigo.Text;
                producto.Nombre = txtNombre.Text;
                producto.Descripcion = txtDescripcion.Text.Trim();
                producto.marca = (Marca)cboMarca.SelectedItem;
                producto.Categoria = cboCategoria.Text;//me lo toma sin el selectedItem,nose xke?
                producto.UrlImagen = txtImagen.Text;
                producto.Precio = Convert.ToDecimal(txtPrecio.Text);//converti el decimal no me lo agarraba
                
                //Agregar o Modificar
                if(producto.Id ==0)
                {
                    negocio.Agregar(producto);
                MessageBox.Show("Perfectamente Agregado");
                Dispose();

                }
                else
                {
                    negocio.modificar(producto);
                    MessageBox.Show("Perfectamente Modificado");
                    Dispose();
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
          

                
            

        }
         private void RecargarImagen(string img)
         {
            try
            {
                if(img !="")
                {
                  
                   pbxProducto.Load(img);

                }
                
            }
             catch(Exception)
            {
            MessageBox.Show("no se pudo cargar la imagen ingresar url valida por favor");
            }

         }

        
    }
}
