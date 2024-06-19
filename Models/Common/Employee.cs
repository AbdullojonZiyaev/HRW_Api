using HRM_Project.Models.Abstraction;

namespace HRM_Project.Models.Common
{
    public class Employee:DbRecord
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SurName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfHire { get; set; } // Дата найма
        public double Salary { get; set; } // Зарплата
        public Gender Gender { get; set; } // Пол
        public string Nationality { get; set; } // Национальность
        public string Education { get; set; } // Образование
        public string PartyAffiliation { get; set; } // Партийность
        public string PreviousJob { get; set; } // Место прошлой работы
        public string MaritalStatus { get; set; } // Семейный статус
        public int NumberOfChildren { get; set; } // Количество детей
        public string Specialization { get; set; } // Специальность
        public int? DivisionId { get; set; }
        public virtual Division Division { get; set; }
    }
    public enum Gender
    {
        Male,
        Female,
        Other
    }

}
