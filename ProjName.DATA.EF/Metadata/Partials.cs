using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProjName.DATA.EF.Models//Metadata
{
    [ModelMetadataType(typeof(BakeryProductsMetadata))]
    public partial class BakeryProducts
    {

        [NotMapped]
        public IFormFile? Image { get; set; }

    }

    [ModelMetadataType(typeof(CategoryMetadata))]
    public partial class Category { }

    [ModelMetadataType(typeof(SeasonsMetadata))]
    public partial class Seasons{ }

    [ModelMetadataType(typeof(UserDetailsMetadata))]
    public partial class UserDetail
    {
            public string FullName { get { return $"{FirstName} {LastName}"; } }
    }
    
}
