namespace Talabat.Dtos
{
    public class ProductToreturnDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? PicUrl { get; set; }
        public decimal Price { get; set; }

        public int CategoryId { get; set; }
        public string? Category { get; set; }

        public int BrandId { get; set; }
        public string? Brand { get; set; }
    }
}
