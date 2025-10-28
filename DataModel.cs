using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursApp
{
    internal class DataModel
    {
        public static string connection()
        {
            return 
                "server=localhost;user=root;" +
                "database=law_office;port=3306;password=12345;";
        }
    }
    internal class lawyers
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Experience { get; set; }
        public string License { get; set; }
        public string ContactData { get; set; }
        public string TypeOfCase { get; set; }
    }
    public class Client
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Rnokpp { get; set; }
        public string ContactData { get; set; }
    }
    public class Service
    {
        public int Id { get; set; }
        public string Clients { get; set; }
        public string Lawyers { get; set; }
        public int ServiceId { get; set; }
        public DateTime SaleDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public string TypeOfCase { get; set; }
    }
    public class TypeOfService
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
    }

    /*public class Service
    {
        public int Id { get; set; }
        public decimal Cost { get; set; }
        public string Deadline { get; set; }
        public string TypeOfService { get; set; }
    }*/

}
