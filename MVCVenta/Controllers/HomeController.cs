using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCVenta.Models;
using MVCVenta.ViewModels;
namespace MVCVenta.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {

            public readonly VentaDataClassesDataContext _data;

        public HomeController(VentaDataClassesDataContext data)
        {
            _data = data;
        }

        public HomeController()
        {
            _data = new VentaDataClassesDataContext();
        }

        public ActionResult Index(int id)
        {
            //int dominio = 0;
            List<ProductoList> listaProductos = null;
            //if (Request.QueryString["id"] != null)
            if (id!= 0)
            {
               // dominio = Int16.Parse(Request.QueryString["id"].ToString());

                var productos = (from p in _data.TB_Productos
                                 join d in _data.TB_Dominios
                                 on p.Fk_eDominio equals d.Pk_eDominio
                                 where p.Fk_eDominio == id
                                 select new
                                 {
                                     p.Pk_eProducto,
                                     dominio = d.cDescripcion,
                                     producto = p.cDescripcion,
                                     p.dPrecio,
                                     p.cEspecificacion,
                                     p.bImagen
                                 }).ToList();

                listaProductos = productos.ConvertAll(o => new ProductoList(o.Pk_eProducto, o.dominio, o.producto, o.dPrecio, o.cEspecificacion, o.bImagen));
            }
            else {
                var productos = (from p in _data.TB_Productos
                                 join d in _data.TB_Dominios
                                 on p.Fk_eDominio equals d.Pk_eDominio
                                 select new
                                 {
                                     p.Pk_eProducto,
                                     dominio = d.cDescripcion,
                                     producto = p.cDescripcion,
                                     p.dPrecio,
                                     p.cEspecificacion,
                                     p.bImagen
                                 }).ToList();

                listaProductos = productos.ConvertAll(o => new ProductoList(o.Pk_eProducto, o.dominio, o.producto, o.dPrecio, o.cEspecificacion, o.bImagen));
            
            }
            
           

            

            //Listado de Categorias
            //var dominios = from c in _data.TB_Dominios select c;
            //ViewData["Dominios"] = new SelectList(dominios, "Pk_eDominio", "cDescripcion");
            List<Dominio> listaDominios = null;
            var dominios = (from c in _data.TB_Dominios select c).ToList();
            listaDominios = dominios.ConvertAll(o => new Dominio(o.Pk_eDominio, o.cDescripcion));
            ViewData["ListOfDominios"] = listaDominios;
            //ViewData["Dominios"] = new SelectList(dominios, "Pk_eDominio", "cDescripcion");
             
            return View(listaProductos);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
