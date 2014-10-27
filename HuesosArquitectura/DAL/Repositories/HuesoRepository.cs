using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesLayer;
using System.Collections;
using System.Transactions;
using System.Data.SqlClient;
using System.Data;
using TIL.CustomExceptions;


namespace DAL
{
    public class HuesoRepository : IRepository<Hueso>
    {

        private List<IEntity> _insertItems;
        private List<IEntity> _deleteItems;
        private List<IEntity> _updateItems;

        public HuesoRepository()
        {
            _insertItems = new List<IEntity>();
            _deleteItems = new List<IEntity>();
            _updateItems = new List<IEntity>();
        }

        public void Insert(Hueso entity)
        {
            _insertItems.Add(entity);
        }

        public void Delete(Hueso entity)
        {
            _deleteItems.Add(entity);
        }

        public void Update(Hueso entity)
        {
            _updateItems.Add(entity);
        }

        public IEnumerable<Hueso> GetAll()
        {

            
            List<Hueso> phueso = null;
           
            SqlCommand cmd = new SqlCommand();
            DataSet ds = DBAccess.ExecuteSPWithDS(ref cmd, "pa_listar_huesos");

            

            if (ds.Tables[0].Rows.Count > 0)
            {
                phueso = new List<Hueso>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    phueso.Add(new Hueso
                    {
                        Id = Convert.ToInt32(dr["idHueso"]),
                        Nombre = dr["nombre"].ToString(),
                        Tipo = dr["tipo"].ToString(),
                        Ubicacion = dr["ubicacion"].ToString()
                    });
                }
            }

            return phueso;
        }

        public Hueso GetById(int id)
        {
            Hueso objHueso = null;
            var sqlQuery = "SELECT Id, Nombre, Precio FROM Producto WHERE id = @idProducto";
            SqlCommand cmd = new SqlCommand(sqlQuery);
            cmd.Parameters.AddWithValue("@idProducto", id);

            var ds = DBAccess.ExecuteQuery(cmd);

            if (ds.Tables[0].Rows.Count > 0)
            {
                var dr = ds.Tables[0].Rows[0];

                objHueso = new Hueso
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    Nombre = dr["Nombre"].ToString(),
                    Tipo = dr["Tipo"].ToString(),
                    Ubicacion = dr["Ubicacion"].ToString()
                };
            }



            return objHueso;
        }

        public void Save()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    if (_insertItems.Count > 0)
                    {
                        foreach (Hueso objHueso in _insertItems)
                        {
                            InsertHueso(objHueso);
                        }
                    }

                    if (_updateItems.Count > 0)
                    {
                        foreach (Hueso p in _updateItems)
                        {
                            UpdateHueso(p);
                        }
                    }

                    if (_deleteItems.Count > 0)
                    {
                        foreach (Hueso p in _deleteItems)
                        {
                            DeleteHueso(p);
                        }
                    }

                    scope.Complete();
                }
                catch (TransactionAbortedException ex)
                {

                }
                catch (ApplicationException ex)
                {

                }
                finally
                {
                    Clear();
                }

            }
        }

        public void Clear()
        {
            _insertItems.Clear();
            _deleteItems.Clear();
            _updateItems.Clear();
        }

        private void InsertHueso(Hueso objHueso)
        {

            try
            {
                SqlCommand cmd = new SqlCommand();

                cmd.Parameters.Add(new SqlParameter("@nomb", objHueso.Nombre));
                cmd.Parameters.Add(new SqlParameter("@tipo", objHueso.Tipo));
                cmd.Parameters.Add(new SqlParameter("@ubicacion", objHueso.Ubicacion));

                DataSet ds = DBAccess.ExecuteSPWithDS(ref cmd, "pa_agregar_hueso");

            }
            catch (Exception ex)
            {

            }

        }

        private void UpdateHueso(Hueso objHueso)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();

                cmd.Parameters.Add(new SqlParameter("@nomb", objHueso.Nombre));
                cmd.Parameters.Add(new SqlParameter("@tipo", objHueso.Tipo));
                cmd.Parameters.Add(new SqlParameter("@ubicacion", objHueso.Ubicacion));


                DataSet ds = DBAccess.ExecuteSPWithDS(ref cmd, "pa_modificar_hueso");

            }
            catch (Exception ex)
            {
                throw new DataAccessException("No se pudó modificar el hueso", ex);
            }
        }

        private void DeleteHueso(Hueso objHueso)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@", objHueso.Id));
                DataSet ds = DBAccess.ExecuteSPWithDS(ref cmd, "pa_borrar_hueso");

            }
            catch (SqlException ex)
            {
                //logear la excepcion a la bd con un Exception
                throw new DataAccessException("Ha ocurrido un error al eliminar un usuario", ex);

            }
            catch (Exception ex)
            {
                //logear la excepcion a la bd con un Exception
                throw new DataAccessException("Ha ocurrido un error al eliminar un usuario", ex);
            }
        }

    }
}
