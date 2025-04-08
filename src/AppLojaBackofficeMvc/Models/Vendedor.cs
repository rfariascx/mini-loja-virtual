using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppLojaBackofficeMvc.Models
{
    public class Vendedor
    {
        [Key]
         [Display(Name = "Id do Vendedor")]
        public string VendedorId{get;set;}

        [Required]
        [StringLength(100)]
         [Display(Name = "Nome Completo")]
        public string VendedorNomeCompleto {get;set;}
        [Required]
        [Display(Name = "E-mail")]
  
        public string VendedorEmail {get;set;}

        
    }
}