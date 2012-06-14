﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using Machine.Specifications;
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
                //var photo = "Images//canon-ixus.jpg";
                //var photo = "Images//nikon-e950.jpg";
                var photo = "Images//IMG_1831.TIFF";
                _image = Image.FromFile(Path.Combine(localPath, photo));
                _propertyItems = _image.PropertyItems;

                var propertyItemIdTranslator = new DefaultPropertyItemIdTranslator();
                var propertyItemValueConverter = new DefaultPropertyItemValueTranslator();

                _exifReader = new ExifReader(propertyItemIdTranslator, propertyItemValueConverter);
            };

        Because of = () => _properties = _exifReader.Read(_propertyItems);

        It should_be = () => _image.ShouldNotBeNull();

        static Image _image;
        static IEnumerable<PropertyItem> _propertyItems;
        static IEnumerable<Property> _properties;
        static ExifReader _exifReader;
    }
}
