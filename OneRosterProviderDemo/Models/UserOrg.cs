﻿using System;

namespace OneRosterProviderDemo.Models
{
    public class UserOrg : BaseModel
    {
        internal override string ModelType()
        {
            throw new NotImplementedException();
        }

        internal override string UrlType()
        {
            throw new NotImplementedException();
        }

        public string UserId { get; set; }
        public User User { get; set; }

        public string OrgId { get; set; }
        public Org Org { get; set; }
    }
}
