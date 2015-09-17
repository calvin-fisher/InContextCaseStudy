using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InContextCaseStudy.Logic.Contracts
{
    public class ProductImage
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string FileName { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public ushort Width { get; set; }
        public ushort Height { get; set; }
        public byte[] Bytes { get; set; }
    }
}
