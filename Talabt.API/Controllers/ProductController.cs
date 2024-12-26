using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;
using AutoMapper;
using Talabt.API.DTOS;
using Talabt.API.Errors;
using Talabt.API.Helpers;
using Microsoft.AspNetCore.Authorization;


namespace Talabt.API.Controllers
{

    public class ProductController : BaseApiController
    {
        private readonly IGenericRepository<Product> _repo;
        private readonly AutoMapper.IMapper _mapped;
        private readonly IGenericRepository<ProductBrand> _brandrepo;
        private readonly IGenericRepository<ProductCategory> _categoryrepo;

        public ProductController(IGenericRepository<Product> repo,IMapper mapped,
            IGenericRepository<ProductBrand> brandrepo,IGenericRepository<ProductCategory> categoryrepo)
        {
            _repo = repo;
     
            _mapped = mapped;
           _brandrepo = brandrepo;
            _categoryrepo = categoryrepo;
        }

        [CashedAttribute(600)]
        // /api/Product
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams Params)
        {
            var specs = new ProductWithBrandSpecification(Params);   
            var products = await _repo.GetAllAsyncWithSpec(specs);
           var mappedproduct = _mapped.Map<IReadOnlyList<ProductToReturnDto>>(products);
            var CountedSpec=new ProductWithFilterSpecification(Params);
           var Count = await _repo.GetCountWithSpec(CountedSpec);
         
            return Ok(new Pagination<ProductToReturnDto>(Params.PageSize, Params.PageIndex, mappedproduct,Count));

        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductToReturnDto), 200)]
        [ProducesResponseType(typeof(ApiResponses), 404)]
        public async Task <ActionResult<ProductToReturnDto>> Getproduct(int id)
        {
            var specs = new ProductWithBrandSpecification(id);    
            var product = await _repo.GetAsyncWithSpec(specs);
            if (product == null) { return NotFound(new ApiResponses(404)); }
            var mappedproduct = _mapped.Map<ProductToReturnDto>(product);
            return Ok(mappedproduct);
        }

        [HttpGet("Brands")]
        public async Task <ActionResult<IReadOnlyList<ProductBrand>>> GetBrandsAsync()
        {
            var Brands = await _brandrepo.GetAllAsync();
            if (Brands is null) { return NotFound(new ApiResponses(404)); }
            
            return Ok(Brands);
        }

        [HttpGet("Types")]
        public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetCategoriesAsync()
        {

            var Categories = await _categoryrepo.GetAllAsync(); 
            if (Categories is null) { return NotFound(new ApiResponses(404)); }
            return Ok(Categories);
        }

    }
}
