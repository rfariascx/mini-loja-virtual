using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppLojaBackofficeMvc.Models
{
    public class Produto
    {
        [Key]
        [Display(Name = "Código do Produto")]
        public int ProdutoId{get;set;}
        [Required]
        [StringLength(40)]
        [Display(Name = "Descrição do Produto")]
        public string ProdutoDescricao{get;set;}
        
        
        public string? ProdutoImagem{get;set;}

        [Required]
        [Range(0.01, 999999.99)]
        [Display(Name = "Preço (BRL)")]
        public decimal ProdutoPreco{get;set;}
        [Required]
        [Range(0, int.MaxValue)]
        [Display(Name = "Saldo no Estoque")]
        public int ProdutoEstoque{get;set;}
        [Required]
        [Display(Name = "Categoria")]
        public int CategoriaId {get;set;}
        public Categoria Categoria {get;set;}   
        
        public string VendedorId {get;set;}
    }
}