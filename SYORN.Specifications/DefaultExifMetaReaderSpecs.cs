using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Machine.Specifications;
using FakeItEasy;
using SYORN.Services;

namespace SYORN.Specifications
{
    public class When_retrieving_EXIF_data
    {
        Establish context = () =>
            {
                var uriPath =
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
                var localPath = new Uri(uriPath.Remove(uriPath.IndexOf("bin"))).LocalPath;
                _image = Image.FromFile(Path.Combine(localPath, "Images//nikon-e950.jpg"));
                _exifReader = new ExifReader(_image.PropertyItems);
            };

        Because of = () => _exifReader.Read();
        It should_be = () => _image.ShouldNotBeNull();

        static Image _image;
        static ExifReader _exifReader;
    }
}
