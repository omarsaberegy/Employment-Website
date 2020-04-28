using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteZft.Models;

namespace WebsiteZft.Models
{
    public class JobsGrouping
    {
        public string JobName { get; set; }
        public IEnumerable<ApplyforJob> Items { get; set; }
    }
}