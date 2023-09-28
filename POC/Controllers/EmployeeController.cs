using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using POC.ApiGetData;
using POC.Model;

namespace EmployeeMiddleware.Controllers
{
    [EnableCors("AllowOrigin")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IApiGetData _ApiGetData;
        private readonly IMapper _mapper;

        public EmployeeController(IApiGetData apiGetData, IMapper mapper)
        {
            _ApiGetData = apiGetData;
            _mapper = mapper;
        }

        [Route("api/[controller]/{id}/{name}")]
        [HttpGet]
        public ActionResult GetEmp(int id, string name)
        {
            
            var response = _ApiGetData.GetEmployee(id, name);

            var output = JsonConvert.DeserializeObject<BaseEmployees>(response);
            var result = _mapper.Map<Employee>(output);

            return Ok(result);
        }
    }
}
