using POC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POC.ApiGetData
{
    public interface IApiGetData
    {
        string GetEmployee(int id, string name);
    }
}
