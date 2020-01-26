using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class EnquiryNotificationModel
    {
        public EnquiryNotificationModel()
        {
            OverDueCount = 0;
            DueTodayCount = 0;
        }

        public int OverDueCount { get; set; }
        public int DueTodayCount { get; set; }
    }
}
