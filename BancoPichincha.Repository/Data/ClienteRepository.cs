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
    public class ClienteRepository : BaseRepository<Cliente>, IClienteRepository
    {
        #region Propiedades
        private readonly TestBancoPichinchaContext _testBancoPichinchaContext;
        #endregion

        #region Constructor
        public ClienteRepository(TestBancoPichinchaContext testBancoPichinchaContext) : base(testBancoPichinchaContext)
        {
            _testBancoPichinchaContext = testBancoPichinchaContext;
        } 
        #endregion
    }
}
