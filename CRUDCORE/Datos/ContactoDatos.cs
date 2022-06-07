using CRUDCORE.Models;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace CRUDCORE.Datos
{
    public class ContactoDatos
    {

        public List<ContactoModel> Listar(String filter)
        {

            var oLista = new List<ContactoModel>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                //sp_listar nombre de la tabla
                SqlCommand cmd = new SqlCommand("sp_Filter", conexion);
                //new procedure filter
                cmd.Parameters.AddWithValue("@filter", filter);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oLista.Add(new ContactoModel()
                        {

                            Id_IE = Convert.ToInt32(dr["Id_IE"]), 
                            N_Identificacion = dr["N_Identificacion"].ToString(),
                            Codigo_Sae = dr["Codigo_Sae"].ToString(),
                            Nombres = dr["Nombres"].ToString(),
                            Apellido1 = dr["Apellido1"].ToString(),
                            Apellido2 = dr["Apellido2"].ToString(),
                            Fec_ExpDoc = dr["Fec_ExpDoc"].ToString(),
                            Email_Personal = dr["Email_Personal"].ToString(),
                            Email_Empresa = dr["Email_Empresa"].ToString(),
                            Descripcion = dr["Descripcion"].ToString(),
                        });
                    }
                }
            }
            return oLista;
        }


        public ContactoModel Obtener(int Id_IE)
        {
            var oContacto = new ContactoModel();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {  
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_obtener", conexion);
                cmd.Parameters.AddWithValue("@id_Info_Empleado", Id_IE);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oContacto.Id_IE = Convert.ToInt32(dr["Id_IE"]);
                        oContacto.N_Identificacion = dr["N_Identificacion"].ToString();
                        oContacto.Codigo_Sae = dr["Codigo_Sae"].ToString();
                        oContacto.Nombres = dr["Nombres"].ToString();
                        oContacto.Apellido1 = dr["Apellido1"].ToString();
                        oContacto.Apellido2 = dr["Apellido2"].ToString();
                        if (!string.IsNullOrEmpty(dr["Fec_ExpDoc"].ToString()))
                        {
                            oContacto.Fec_ExpDoc = Convert.ToDateTime(dr["Fec_ExpDoc"].ToString()).ToString("yyyy-MM-dd");
                        }
                        oContacto.Email_Personal = dr["Email_Personal"].ToString();
                        oContacto.Email_Empresa = dr["Email_Empresa"].ToString();
                        oContacto.Descripcion = dr["Descripcion"].ToString();
                    }
                }
            }
            return oContacto;
        }


        public bool Editar(ContactoModel oContacto)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_editar", conexion);
                    cmd.Parameters.AddWithValue("Id_IE", oContacto.Id_IE);
                    cmd.Parameters.AddWithValue("Codigo_Sae", oContacto.Codigo_Sae);
                    cmd.Parameters.AddWithValue("Fec_ExpDoc", oContacto.Fec_ExpDoc);
                    cmd.Parameters.AddWithValue("Email_Personal", oContacto.Email_Personal);
                    cmd.Parameters.AddWithValue("Email_Empresa", oContacto.Email_Empresa);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                rpta = true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                rpta = false;
            }
            return rpta;
        }
    }
}
