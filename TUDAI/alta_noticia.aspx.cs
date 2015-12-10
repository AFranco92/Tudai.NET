﻿using System;
using DAL;
using System.Collections.Generic;
using System.Linq;

namespace TUDAI
{
    public partial class AltaNoticia : System.Web.UI.Page
    {
        public const string id_noticia = "id";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDdls();
               
            }

            if (Request.QueryString[id_noticia] != null)
            {
                accionNoticia.Text = "Editar Noticia";
                var oNoticia = new Noticia()
                {
                    Id = int.Parse(Request.QueryString[id_noticia]),

                };
                NoticiaBusiness noticiaByID = new NoticiaBusiness();
                var noticiaM = noticiaByID.GetNoticiaById(oNoticia);

                txt_titulo.Text = noticiaM.Tables[0].Rows[0]["titulo"].ToString();
                txt_cuerpo.Text = noticiaM.Tables[0].Rows[0]["cuerpo"].ToString();
                date_fecha.SelectedDate = DateTime.Parse(noticiaM.Tables[0].Rows[0]["fecha"].ToString());
                ddl_categorias.Text = noticiaM.Tables[0].Rows[0]["id_categoria"].ToString();


            }
            
                
            


        }

        private void CargarDdls()
        {
            ddl_categorias.DataSource = new CategoriaBusiness().GetCategorias();
            ddl_categorias.DataBind();   
        }

        protected void Publicar_Noticia(object sender, EventArgs e)
        {
            var oNoticia = new Noticia()
            {
                Titulo = txt_titulo.Text,
                Cuerpo = txt_cuerpo.Text,
                Fecha = date_fecha.SelectedDate,
                IdCategoria = int.Parse(ddl_categorias.SelectedValue)
            };
            using (NoticiaBusiness n = new NoticiaBusiness())
            {
                n.InsertNoticia(oNoticia);
            }
            lbl_resultado.Text = "Noticia publicada correctamente";            
            
        }

    }
}