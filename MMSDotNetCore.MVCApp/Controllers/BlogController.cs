﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Db;

namespace MMSDotNetCore.MVCApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _db;

        public BlogController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var lst = await _db.Blogs.AsNoTracking()
                .OrderByDescending(x => x.BlogId)
                .ToListAsync();
            return View(lst);
        }

        [ActionName("Create")]
        public IActionResult BlogCreate()
        {
            return View("BlogCreate");
        }

        [HttpPost]
        [ActionName("Save")]
        public async Task<IActionResult> SaveBlog(BlogModel blog)
        {
            await _db.Blogs.AddAsync(blog);
            var result = await _db.SaveChangesAsync();
            return Redirect("/Blog");
        }

        [ActionName("Edit")]
        public async Task<IActionResult> EditBlog(int id)
        {
            var item = await _db.Blogs.AsNoTracking()
                .FirstOrDefaultAsync(x => x.BlogId == id);
            if (item is null) return Redirect("/Blog");
            return View("EditBlog", item);
        }

        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> UpdateBlog(int id, BlogModel blog)
        {
            var item = await _db.Blogs.AsNoTracking()
                .FirstOrDefaultAsync(x => x.BlogId == id);
            if (item is null) return Redirect("/Blog");
            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            item.BlogContent = blog.BlogContent;
            _db.Entry(item).State = EntityState.Modified;
            var result = await _db.SaveChangesAsync();
            return Redirect("/Blog");
        }

        [ActionName("Delete")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            var item = await _db.Blogs.AsNoTracking()
                .FirstOrDefaultAsync(x => x.BlogId == id);
            if (item is null) return Redirect("/Blog");
            _db.Blogs.Remove(item);
            var result = await _db.SaveChangesAsync();
            return Redirect("/Blog");
        }
    }
}
