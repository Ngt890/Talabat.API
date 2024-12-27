using AutoMapper.Execution;
using Talabat.Core.Entities;
using Talabt.API.DTOS;
using AutoMapper;

namespace Talabt.API.Helpers
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductToReturnDto, string>

    {
        private readonly IConfiguration _config;

        public ProductPictureUrlResolver(IConfiguration config)
        {
            _config = config;
        }
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
                return $"{_config["BaseUrl"]}/{source.PictureUrl}";

            return string.Empty;




        } }
}
