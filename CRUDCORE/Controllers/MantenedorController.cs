using Microsoft.AspNetCore.Mvc;
using CRUDCORE.Datos;
using CRUDCORE.Models;
using System.Security.Cryptography;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace CRUDCORE.Controllers
{
    public class MantenedorController : Controller
    {

        ContactoDatos _ContactoDatos= new ContactoDatos();
        public IActionResult Listar(String filter)
        {

            if (filter != null)
            {
                var idUsuario = HttpContext.Session.GetInt32("idUsuario");
                if(idUsuario != null)
                {
                    var oLista = _ContactoDatos.Listar(filter);
                    return View(oLista);
                }
                else
                {
                    return RedirectToAction("Login","Mantenedor");
                }
                
            }

            return View();
        }

        public IActionResult Editar(int IdContacto)
        {
            // metodo solo devuelve la vista
            var idUsuario = HttpContext.Session.GetInt32("idUsuario");
            if (idUsuario != null)
            {
                var ocontacto = _ContactoDatos.Obtener(IdContacto);
                return View(ocontacto);
            }
            else
            {
                return RedirectToAction("Login", "Mantenedor"); 
            }    
        }
        [HttpPost]
        public IActionResult Editar(ContactoModel oContacto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var respuesta = _ContactoDatos.Editar(oContacto);
            if (respuesta)
            {
                return RedirectToAction("Listar");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        //--------------------------------------------------------------------------------------------------------------------//

        public IActionResult Login()
        {
            return View();
        }

        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();

            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();

        }

        [HttpPost]
        public IActionResult Login(ContactoModel oUsuario)
        {
            int idUsuario = 0;
            string clave;
            if (!string.IsNullOrEmpty(oUsuario.clave)&&!string.IsNullOrEmpty(oUsuario.correo))
            {
                clave = MD5Hash(oUsuario.clave);
                oUsuario.clave = clave;
                var cn = new Conexion();
                string correo = oUsuario.correo;
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    SqlCommand cmd = new SqlCommand("sp_ValidarUsuario", conexion);
                    cmd.Parameters.AddWithValue("@Logueo", correo);
                    cmd.Parameters.AddWithValue("@Pass", clave);
                    cmd.CommandType = CommandType.StoredProcedure;

                    conexion.Open();

                    idUsuario = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                    oUsuario.Id_Usuario = idUsuario;
                }
                if (idUsuario != 0)
                {
                    HttpContext.Session.SetInt32("idUsuario", idUsuario);
                    return RedirectToAction("Listar", "Mantenedor");
                }
                else
                {
                    ViewData["Mensaje"] = "Usuario no encontrado";
                    return View();
                }
            }
                
            else
            {
                ViewData["Mensaje"] = "Ingrese Datos";
                return RedirectToAction("Login", "Mantenedor");
            }            
        }
    }
}



