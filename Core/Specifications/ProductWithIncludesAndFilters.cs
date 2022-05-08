using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductWithIncludesAndFilters : BaseSpecification<Product>
    {
        public ProductWithIncludesAndFilters(ProductSpecParams productParams) :
            base(x =>
                    (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
                    (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
                    (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
                )
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
            AddInclude(p => p.Pictures);


            //Default Sorting
            AddOrderBy(p => p.Name);

            //Add Paging
            var skip = productParams.PageSize * (productParams.PageIndex - 1);
            var take = productParams.PageSize;
            ApplyPaging(skip, take);


            //sorting data
            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    case "nameDesc":
                        AddOrderByDescending(p => p.Name);
                        break;
                    case "nameAsc":
                        AddOrderBy(p => p.Name);
                        break;
                    default:
                        AddOrderBy(n => n.Name);
                        break;
                }
            }
        }

        public ProductWithIncludesAndFilters()
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
            AddInclude(p => p.Pictures);
        }
    }
}
