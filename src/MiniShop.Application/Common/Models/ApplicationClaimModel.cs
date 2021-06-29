using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.Common.Models
{
   public class ApplicationClaimModel
    {

        public ApplicationClaimModel()
        {

        }

        public ApplicationClaimModel(string name, string displayName,
                                bool isImportantForSecurity = false)
        {
            Name = name;
            DisplayName = displayName;
            IsImportantForSecurity = isImportantForSecurity;
        }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public bool IsImportantForSecurity { get; set; }





    }
}
