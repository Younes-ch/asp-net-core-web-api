﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class EmployeeNotFoundException(Guid employeeId) :
        NotFoundException($"Employee with id: {employeeId} doesn't exist in the database.");
}
