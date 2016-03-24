using IdeasRepository.Models;
using IdeasRepository.Services;
using IdeasRepository.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdeasRepository.Controllers
{
    [Authorize]
    [RecordErrorHandler]
    public class RecordController : Controller
    {
        private IRecordService recordService;

        public RecordController(IRecordService recordService) 
        {
            this.recordService = recordService;
        }

        [HttpPost]
        public ActionResult Save(Record record) 
        {
            recordService.Save(record, User.Identity.Name);
            return Json(new { success = true });
        }

        [HttpPut]
        public ActionResult Update(Record record)
        {
            recordService.Update(record);
            return Json(new { success = true });
        }

        [HttpDelete]
        public ActionResult Delete(int recordId) 
        {
            recordService.Delete(recordId, User.Identity.Name);
            return Json(new { success = true });
        }

        [HttpDelete]
        public ActionResult ConfirmDeletion(int recordId)
        {
            recordService.Delete(recordId, User.Identity.Name);
            return Json(new { success = true });
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public ActionResult Restore(int recordId)
        {
            recordService.RestoreRecord(recordId);
            return Json(new { success = true });
        }

        [HandleError]
        public ActionResult GetUserRecords()
        {
            IEnumerable<Record> records = recordService.GetUserRecords(User.Identity.Name);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_RecordsPartial", records);
            }
            return View("Records", records);
        }

        [HandleError]
        [Authorize(Roles = "Admin")]
        public ActionResult GetRecords()
        {
            
            IEnumerable<Record> records = recordService.GetRecords();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_RecordsPartial", records);
            }
            return View("Records", records);
        }

    }
}
