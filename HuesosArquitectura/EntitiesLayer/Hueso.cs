using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer
{
    public class Hueso : IEntity
    {
        private int _idProducto;
        public int Id { 
            get { return _idProducto; } 
            set { _idProducto = value; }
        }
        public String Nombre { get; set; }
        public String Tipo { get; set; }
        public String Ubicacion { get; set; }

        public Hueso(){
            Id = 0;
            Nombre = "";
            Tipo = "";
            Ubicacion = "";
        }

        public Hueso(String pnombre,String ptipo, String pubicacion)
        {
            Id = 0;
            Nombre = pnombre;
            Tipo = ptipo;
            Ubicacion = pubicacion;
        }
        public bool IsValid
        {
            get { return (GetRuleViolations().Count() == 0); }
        }

        public IEnumerable<RuleViolation> GetRuleViolations()
        {
            if (String.IsNullOrEmpty(Nombre))
            {
                yield return new RuleViolation("Nombre Requerido", "Nobre");
            }
            //if (Precio <= 0)
            //{
            //    yield return new RuleViolation("El precio debe tener un valor", "Precio");
            //}

            yield break;
        }

    
    }
}

 


