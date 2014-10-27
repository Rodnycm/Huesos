using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesLayer;
using DAL;


namespace BLL
{
    public class Gestor
    {
        private UnitOfWork UoW = new UnitOfWork();

        public void agregarHueso(string nombre, string tipo, string ubicacion)
        {
            try
            {
                Hueso objHueso = new Hueso(nombre, tipo, ubicacion);
                if (objHueso.IsValid)
                {
                    UoW.HuesoRepository.Insert(objHueso);
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (RuleViolation rv in objHueso.GetRuleViolations())
                    {
                        sb.AppendLine(rv.ErrorMessage);
                    }
                    throw new ApplicationException(sb.ToString());
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        //public void modificarHueso(int idHueso, string nombre, string tipo, string ubicacion)
        //{

        //    Hueso objHueso = new Producto(idHueso, nombre, tipo,ubicacion);
        //    UoW.ProductoRepository.Update(objHueso);

        //}

        //public void eliminarProducto(int idHueso)
        //{

        //    Hueso objHueso = new Hueso { Id = idHueso };
        //    UoW.HuesoRepository.Delete(objHueso);
        //}

        public IEnumerable<Hueso> consultarHuesos()
        {
            return UoW.HuesoRepository.GetAll();
        }

        //public Hueso consultarHuesoXID(int id)
        //{
        //    return UoW.HuesoRepository.GetById(id);
        //}

        public void guardarCambios()
        {
            //try
            //{
                UoW.HuesoRepository.Save();
        //    }
        //    catch (DataAccessException ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        //logear a la bd
        //        throw new BusinessLogicException("Ha ocurrido un error al eliminar un usuario", ex);
        //    }
        }

    }
}
