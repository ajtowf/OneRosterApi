using System;

namespace OneRosterProviderDemo.Models
{
    public class IMSClassAcademicSession : BaseModel
    {
        internal override string ModelType()
        {
            throw new NotImplementedException();
        }

        internal override string UrlType()
        {
            throw new NotImplementedException();
        }

        public string IMSClassId { get; set; }
        public IMSClass IMSClass { get; set; }

        public string AcademicSessionId { get; set; }
        public AcademicSession AcademicSession { get; set; }
    }
}
