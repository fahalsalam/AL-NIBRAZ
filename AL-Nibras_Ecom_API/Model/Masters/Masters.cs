#nullable disable
using AL_Nibras_Ecom_API.Classes;
using Newtonsoft.Json;

namespace AL_Nibras_Ecom_API.Model.Masters
{
    public class Masters
    {
        public class CategoryMaster
        {
            public int? CategoryId { get; set; }
            public string CategoryName { get; set; }
            public string CategoryDescription { get; set; }
            public int? ParentCategoryId { get; set; }
            public string ImageUrl { get; set; }
            public int? SortOrder { get; set; }

        }

        public class BrandMaster
        {
            public string BrandName { get; set; }
            public string ImageUrl { get; set; }
            public bool isActive { get; set; }

        }

        public class DesktopImageConfig
        {
            public string Imgurl_1 { get; set; }
            public string Imgurl_2 { get; set; }
            public string Imgurl_3 { get; set; }
            public string Imgurl_4 { get; set; }
            public string Imgurl_5 { get; set; }
            public string Imgurl_6 { get; set; }
        }

        public class MobileImageConfig
        {
            public string Imgurl_1 { get; set; }
            public string Imgurl_2 { get; set; }
            public string Imgurl_3 { get; set; }
            public string Imgurl_4 { get; set; }
            public string Imgurl_5 { get; set; }
            public string Imgurl_6 { get; set; }
        }

        public class TabImageConfig
        {
            public string Imgurl_1 { get; set; }
            public string Imgurl_2 { get; set; }
            public string Imgurl_3 { get; set; }
            public string Imgurl_4 { get; set; }
            public string Imgurl_5 { get; set; }
            public string Imgurl_6 { get; set; }
        }

        public class EcommerceConfiy
        {
            public List<DesktopImageConfig> Desktop { get; set; }
            public List<MobileImageConfig> Mobile { get; set; }
            public List<TabImageConfig> Tab { get; set; } 
            public string settings { get; set; } 
        }


        public class Colors
        {
            public string ColorName { get; set; }
            public string HexCode { get; set; }
        }

        public class ColorsMaster
        {
            public List<Colors> Colors { get; set; }
        }

        public class Size
        {
            public string SizeLabel { get; set; }
            public string SizeOrder { get; set; }
        }

        public class SizeMaster
        {
            public List<Size> Size { get; set; }
        }

        public class ProductCreateDto
        {
            public string ProductName { get; set; }
            public string? Description { get; set; }
            public int? CategoryId { get; set; }
            public string? TaxCode { get; set; }
            public string? ImageUrl { get; set; }
            public string? ImageUr2 { get; set; }
            public string? ImageUr3 { get; set; }
            public string? ImageUrl4 { get; set; } 
            public int? BrandID { get; set; }  
            public decimal? Price { get; set; }
            public decimal? StockQty { get; set; }
            public decimal? Discount { get; set; } 
            public List<ProductVariantDto> Variants { get; set; }
        }

        public class ProductVariantDto
        {
            public int? BrandId { get; set; }
            public int? ColorId { get; set; }
            public int? SizeId { get; set; }
            public string Sku { get; set; }
            public ProductPriceDto Price { get; set; }
            public ProductStockDto Stock { get; set; }
            public List<ProductImageDto> Images { get; set; }

         }

        public class ProductPriceDto
        {
            public int? PriceId { get; set; }
            public decimal? Price { get; set; }
            public decimal? DiscountPercentage { get; set; }
            public decimal? DiscountPrice { get; set; }
            public string? Currency { get; set; }
        }

        public class ProductStockDto
        {
            public int? StockId { get; set; }
            public decimal? Onhand { get; set; }
        }

        public class ProductImageDto
        {
            public int? ImageId { get; set; } 
            public string? ImageUrl { get; set; }
            public bool? IsPrimary { get; set; }
        }

        public class ProductDto
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public string Description { get; set; }
            public int CategoryId { get; set; }
            public string? CategoryName { get; set; } 
            public string TaxCode { get; set; }
            public string? ImageUrl { get; set; }
            public string? ImageUr2 { get; set; }
            public string? ImageUr3 { get; set; }
            public string? ImageUrl4 { get; set; }
            public int? BrandID { get; set; }
            public string? BrandName { get; set; }
            public decimal? Price { get; set; }
            public decimal? StockQty { get; set; }
            public decimal? Discount { get; set; }

            public List<ProductVariantGetDto> Variants { get; set; }
        }

        public class ProductVariantGetDto
        {
            public int VariantId { get; set; }
            public int BrandId { get; set; }
            public string BrandName { get; set; }
            public int ColorId { get; set; }
            public string ColorName { get; set; }
            public string HexCode { get; set; } 
            public int SizeId { get; set; }
            public string SizeLabel { get; set; } 
            public string Sku { get; set; }

            [JsonConverter(typeof(NestedJsonConverter<ProductPriceDto>))]
            public ProductPriceDto Price { get; set; }

            [JsonConverter(typeof(NestedJsonConverter<ProductStockDto>))]
            public ProductStockDto Stock { get; set; }

            public List<ProductImageDto> Images { get; set; }
        } 
       
    }
}
