using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HumanResourcesProject.Domain.Entities
{
    public class Department:IBaseEntity
    {

        public Guid Id { get; set; }
        //[Display(Name = "Department Name")]
        //[Required(ErrorMessage = "{0} field is required.")]
        //[MinLength(5, ErrorMessage = "{0} cannot be least 5 characters."), MaxLength(100, ErrorMessage = "{0} cannot be more than 100 characters.")]
        public string Name { get; set; }
        //[Display(Name = "Department Description")]
        //[Required(ErrorMessage = "{0} field is required.")]
        //[MinLength(5, ErrorMessage = "{0} cannot be least 5 characters."), MaxLength(100, ErrorMessage = "{0} cannot be more than 100 characters.")]
        public string Description { get; set; }
        
        //Navigation Property
        public  virtual List<Person> Persons { get; set; }
        public DateTime? CreatedDate { get ; set ; }
        public DateTime? UpdatedDate { get ; set ; }
        public DateTime? DeletedDate { get ; set ; }
        public virtual List<Employee> Employees { get; set; }
    }
}
