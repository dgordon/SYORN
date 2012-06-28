using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using SYORN.Services;

namespace ConsoleApp
{
    class Program
    {
        static void Main()
        {
            var uriPath =
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            var localPath = new Uri(uriPath.Remove(uriPath.IndexOf("bin"))).LocalPath;
            var photo = "Images//iphone.JPG";

            var image = Image.FromFile(Path.Combine(localPath, photo));

            var propertyItemValueConverter = new DefaultPropertyItemValueConverter();
            var propertyItemIdTranslator = new DefaultPropertyItemTranslator(propertyItemValueConverter);

            var _exifReader = new ExifReader(propertyItemIdTranslator);

            Console.ForegroundColor = ConsoleColor.Gray;

            foreach (var prop in _exifReader.Read(image.PropertyItems))
            {
                Console.Write(prop.Name + ": ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(prop.Value + "\n");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }
    }
}
