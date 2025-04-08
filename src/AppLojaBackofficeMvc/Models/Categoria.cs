using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppLojaBackofficeMvc.Models
{
    public class Categoria
    {
        [Key]
        [Display(Name = "Id da Categoria")]
        public int CategoriaId{get;set;}

        [Required]
        [StringLength(40)]  
        [Display(Name = "Descrição da Categoria")]    
        public string? CategoriaDescricao{get;set;}


        
    }
}