using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IIG.Core.Interface.Repository.Dtos
{
    public class EfTransactionHolder
    {
        public DbContext DbContext { get; set; }
        public IDbContextTransaction Transaction { get; set; }
    }
}
