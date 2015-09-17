using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using InContextCaseStudy.Logic.Contracts;

namespace InContextCaseStudy.Logic
{
    public class ServiceClient
    {
        private readonly HttpClient _httpClient = new HttpClient()
        {
            BaseAddress = new Uri("http://placekitten.com/g/")
        };

        public async Task<ProductImage[]> GetProductImages(uint count)
        {
            // No actual API; simulate by retrieving n images
            var images = GenerateMockProductImages(count).ToArray();

            foreach (var image in images)
                image.Bytes = await GetMockProductImageBytes(image);

            return images;
        }

        private IEnumerable<ProductImage> GenerateMockProductImages(uint count)
        {
            var random = new Random();
            for (uint i = 0; i < count; i++)
            {
                var width = random.Next(200, 400);
                var height = random.Next(200, 400);

                yield return new ProductImage
                {
                    Id = Guid.NewGuid(),
                    DisplayName = "Kitten",
                    FileName = string.Format("{0}-{1}.jpg", width, height),
                    Height = (ushort)height,
                    Width = (ushort)width,
                    Created = DateTime.UtcNow,
                    Updated = DateTime.UtcNow,
                };
            }
        }

        private async Task<byte[]> GetMockProductImageBytes(ProductImage productImage)
        {
            var image = await _httpClient.GetAsync(string.Format("{0}/{1}", productImage.Width, productImage.Height));
            var bytes = await image.Content.ReadAsByteArrayAsync();
            return bytes;
        }
    }
}
