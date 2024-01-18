using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniProject006.Models
{
    [Table("MiniProject006_Employees")]
    public class Employee
    {
        public int Id { get; set; }
        [DisplayName("First and last name")]
        public string Name { get; set; }
        [DisplayName("Date of employment")]
        public DateOnly DateOfEmployment { get; set; }
        [DisplayName("Year of birth")]
        public int YearOfBirth { get; set; }
    }
}
