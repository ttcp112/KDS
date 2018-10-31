using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSWebKDS.Shared.Models
{
    public class StoreModels
    {
        public string Id { get; set; }
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string IndustryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string GSTRegNo { get; set; }
        public string TimeZone { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedUser { get; set; }
        public DateTime LastDateModified { get; set; }
        public string LastUserModified { get; set; }
        public string StoreCode { get; set; }
        public bool IsIncludeTax { get; set; }
        public string HostUrlExtend { get; set; }
        public string NameExtend { get; set; }
        public string OrganizationName { get; set; }
        public DateTime ExpiredDate { get; set; }
        public List<StoreModels> ChildStore { get; set; }

        public StoreModels()
        {
            ChildStore = new List<StoreModels>();
        }
    }

    public class StoreModelsRespone
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string AppKey { get; set; }
        public string AppSerect { get; set; }
        public bool IsActive { get; set; }
    }
}
