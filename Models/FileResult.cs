using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class FileResult
    {
        //  public string FileName { get; set; }
        public string NewFileName { get; set; }
        public string OrgReqId { get; set; }
        public string DocumentName { get; set; }
        public string DocumentType { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public string FilePath { get; set; }
        public string FileSize { get; set; }
        public char IsUploaded { get; set; }
        public string tokenId { get; set; }

    }

    public class FileUploaded
    {
        public string FileName, DocumentType, FileSizeInKB;

    }
    public class DocumentType
    {
        public string Name { set; get; }
        public string Value { set; get; }

    }
}


//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace WebApplication1.Models
//{
//    public class FileResult
//    {
//        //  public string FileName { get; set; }
//        public string NewFileName { get; set; }
//        public string OrgReqId { get; set; }
//        public string DocumentName { get; set; }
//        public string DocumentType { get; set; }
//        public string Name { get; set; }
//        public string ContentType { get; set; }
//        public string FilePath { get; set; }
//        public string FileSize { get; set; }
//        public char IsUploaded { get; set; }
//        public string tokenId { get; set; }

//    }

//    public class FileUploaded
//    {
//        public string FileName, DocumentType, FileSizeInKB;

//    }
//    public class DocumentType
//    {
//        public string Name { set; get; }
//        public string Value { set; get; }

//    }
//}