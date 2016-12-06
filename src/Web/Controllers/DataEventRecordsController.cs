using System.Collections.Generic;
using DomainModel;
using DomainModel.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DotNetData.Controllers
{
    [Route("api/[controller]")]
    public class DataEventRecordsController : Controller
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        private readonly ILogger _logger;

        public DataEventRecordsController(IDataAccessProvider dataAccessProvider, ILogger<DataEventRecordsController> logger)
        {
            _logger = logger;
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public IEnumerable<DataEventRecord> Get()
        {
            _logger.LogDebug("Get request");
            return _dataAccessProvider.GetDataEventRecords();
        }

        [HttpGet]
        [Route("SourceInfos")]
        public IEnumerable<SourceInfo> GetSourceInfos(bool withChildren)
        {
            return _dataAccessProvider.GetSourceInfos(withChildren);
        }

        [HttpGet("{id}")]
        public DataEventRecord Get(long id)
        {
            return _dataAccessProvider.GetDataEventRecord(id);
        }

        [HttpPost]
        public void Post([FromBody]DataEventRecord value)
        {
            _dataAccessProvider.AddDataEventRecord(value);
        }

        [HttpPut("{id}")]
        public void Put(long id, [FromBody]DataEventRecord value)
        {
            _dataAccessProvider.UpdateDataEventRecord(id, value);
        }

        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            _dataAccessProvider.DeleteDataEventRecord(id);
        }
    }
}
