using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesLayer;

namespace DAL
{

    public class UnitOfWork
    {

        private IRepository<Hueso> _huesoRepository;

        public IRepository<Hueso> HuesoRepository
        {
            get
            {
                if (this._huesoRepository == null)
                {
                    this._huesoRepository = new HuesoRepository();
                }
                return _huesoRepository;
            }
        }



    }
}