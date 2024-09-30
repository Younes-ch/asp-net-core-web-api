using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class CompanyNotFoundException(Guid companyId)
        : NotFoundException($"The company with id: {companyId} doesn't exist in the database.");
}
