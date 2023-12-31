﻿// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using ProductivityTools.MasterConfiguration;
using ProductivityTools.PhotoGallery.Resizer.Logic;

Console.WriteLine("Hello, World!");

IConfigurationRoot configuration = new ConfigurationBuilder()
.AddMasterConfiguration(configName: "ProductivityTools.PhotoGallery.json")
.Build();
var photoPath = configuration["PhotoPath"];
var thumbNailPath = configuration["ThumbnailPath"];
List<int> targetWidthSizes = configuration["ThumbNailWidthSizes"].Split(',').Select(x => int.Parse(x)).ToList();

ProcessingService ps = new ProcessingService((x) => Console.WriteLine(x));
ps.ValidateThumNailsForAllDirectories(photoPath, thumbNailPath, targetWidthSizes);
