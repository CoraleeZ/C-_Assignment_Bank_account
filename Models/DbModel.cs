using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bank_Accounts.Models
{
    public class ForLandR
    {
        [Key]
        [Required]
        public int ForLandRId { get; set; }
        [Required]
        [MinLength(3)]
        public string Firstname { get; set; }
        [Required]
        [MinLength(3)]
        public string Lastname { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [MinLength(8, ErrorMessage="Password must be 8 characters or longer!")]
        public string Password {get;set;}
        public List<Transaction> Trans {get;set;}
        [DisplayFormat(ApplyFormatInEditMode=true,DataFormatString="{0:MM/dd/yyyy}")]
        public DateTime CreatedAt {get;set;}
        [DisplayFormat(ApplyFormatInEditMode=true,DataFormatString="{0:MM/dd/yyyy}")]
        public DateTime UpdatedAt {get;set;}

        [NotMapped]
        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string C_Password {get;set;}
    }

    public class Transaction
    {
        [Key]
        public int TansactionId { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public int ForLandRId{ get; set; }
        public ForLandR Customer {get;set;}
        [DisplayFormat(ApplyFormatInEditMode=true,DataFormatString="{MM/dd/yyyy}")]
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        [DisplayFormat(ApplyFormatInEditMode=true,DataFormatString="{MM/dd/yyyy}")]
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }

}