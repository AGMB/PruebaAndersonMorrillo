using BancoPichincha.Core.Interfaces.Repository;
using BancoPichincha.Core.Models.Entities;
using BancoPichincha.Repository.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoPichincha.Repository.Data
{
    public class MovimientoRepository: BaseRepository<Movimiento>, IMovimientoRepository
    {
        #region Propiedades
        private readonly TestBancoPichinchaContext _dbContext;
        #endregion

        #region Constructor
        public MovimientoRepository(TestBancoPichinchaContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        } 
        #endregion
    }
}
