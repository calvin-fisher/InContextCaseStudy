using System;
using System.Linq;
using System.Net.Mime;
using InContextCaseStudy.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InContextCaseStudy.Test
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void GetImages()
        {
            const int testImagesToFetch = 5;

            var images = new ServiceClient().GetProductImages(testImagesToFetch).Result;
            
            Assert.IsNotNull(images);
            Assert.IsTrue(images.Length == testImagesToFetch, "Different number of images received than requested.");
            Assert.IsTrue(images.All(x => x.Height >= 200 && x.Height <= 400 && x.Width >= 200 && x.Width <= 400), "Not all images within expected sizes.");
            Assert.IsTrue(images.All(x => x.Bytes != null && x.Bytes.Any()), "Not all images have response payload.");
            Assert.IsTrue(images.GroupBy(x => x.Height).OrderBy(x => x.Count()).First().Count() <= 2, "Too many images have the same height; more randomness is expected.");
            Assert.IsTrue(images.GroupBy(x => x.Width).OrderBy(x => x.Count()).First().Count() <= 2, "Too many images have the same width; more randomness is expected.");
        }
    }
}
