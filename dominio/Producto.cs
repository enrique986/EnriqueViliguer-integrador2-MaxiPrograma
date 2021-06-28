﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
   public class Producto
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
       public string Descripcion { get; set; }
       // public string Categoria { get; set; }
        public string UrlImagen { get; set; }
        public decimal Precio { get; set; }
        public Marca marca { get; set; }
        public Categoria categoria { get; set; }

    }
}
