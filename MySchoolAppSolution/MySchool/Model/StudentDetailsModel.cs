using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySchool
{

    public static class ConstStudentDetails
    {
        public const string TableName = "StudentDetails";
        public const string StudentID = "studentid";
        public const string Name = "name";
        public const string Gender = "gender";
        public const string StudentClass = "studentclass";
        public const string Birthdate = "birthdate";
        public const string Address = "address";
        public const string Street = "street";
        public const string City = "city";
        public const string State = "state";
        public const string Country = "country";
        public const string PostalCode = "postalCode";
        public const string EmailID = "emailid";
        public const string ContactNo = "contactno";
        public const string IsAudited = "isaudited";
        public const string FileName = "filename";
        public const string Type = "type";
    }

    [Table(ConstStudentDetails.TableName)]
    public  class StudentDetailsModel
    {
        [Column(ConstStudentDetails.StudentID), PrimaryKey]
        public int StudentID { get; set; }

        [Column(ConstStudentDetails.Name)]
        public string Name { get; set; }

        [Column(ConstStudentDetails.Gender)]
        public string Gender { get; set; }

        [Column(ConstStudentDetails.StudentClass)]
        public string StudentClass { get; set; }

        [Column(ConstStudentDetails.Birthdate)]
        public string Birthdate { get; set; }

        [Column(ConstStudentDetails.Address)]
        public string Address { get; set; }

        [Column(ConstStudentDetails.Street)]
        public string Street { get; set; }

        [Column(ConstStudentDetails.City)]
        public string City { get; set; }

        [Column(ConstStudentDetails.State)]
        public string State { get; set; }

        [Column(ConstStudentDetails.Country)]
        public string Country { get; set; }

        [Column(ConstStudentDetails.PostalCode)]
        public string PostalCode { get; set; }

        [Column(ConstStudentDetails.EmailID)]
        public string EmailID { get; set; }

        [Column(ConstStudentDetails.ContactNo)]
        public string ContactNo { get; set; }

        [Column(ConstStudentDetails.IsAudited)]
        public bool IsAudited { get; set; }

        [Column(ConstStudentDetails.FileName)]
        public string FileName { get; set; }

        [Column(ConstStudentDetails.Type)]
        public string Type { get; set; }

        public byte[] bytesToUpload { get; set; }
        [Ignore]
        public UserOAuthenticationModel userOAuthenticationModel { get; set; }
        
    }

   
}
