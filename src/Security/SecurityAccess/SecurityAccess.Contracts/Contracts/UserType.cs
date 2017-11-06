using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityAccess.Contracts
{
    public enum UserTypeEnum
    {
        None = 0,
        Company = 1,
        Individual = 2
    }

    public enum CompanyUserTypeEnum
    {
        Admin = 1,
        Normal = 2
    }
}
