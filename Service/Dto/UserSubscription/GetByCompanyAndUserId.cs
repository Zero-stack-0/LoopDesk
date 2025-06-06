using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Dto.UserSubscription
{
    public class GetByCompanyAndUserId
    {
        public string CompanyId { get; set; }
        public string UserId { get; set; }
    }
}