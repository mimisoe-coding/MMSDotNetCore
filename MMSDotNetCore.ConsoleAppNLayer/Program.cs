﻿// See https://aka.ms/new-console-template for more information
using MMSDotNetCore.NLayer.DataAccess;
using Newtonsoft.Json;

Console.WriteLine("Hello, World!");
BL_Blog blog = new BL_Blog();
var lst = blog.GetBlogs();
Console.WriteLine(lst);

List<BlogEntity> blogEntities = lst.Select(x =>
        new BlogEntity(x.BlogId, x.BlogTitle!, x.BlogAuthor!, x.BlogContent!))
    .ToList();

var jsonStr = JsonConvert.SerializeObject(lst, Formatting.Indented);
Console.WriteLine(jsonStr);
Console.WriteLine(blogEntities.ToString());
foreach (var blogEntity in blogEntities)
{
    Console.WriteLine(blogEntity);
}
foreach (var item in lst)
{
    Console.WriteLine(item);
}
Console.ReadLine();

Console.ReadKey();