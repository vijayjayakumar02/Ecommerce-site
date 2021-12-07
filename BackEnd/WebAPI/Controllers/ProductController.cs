using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomIdentity.Data.Context;
using WebAPI.Models.BindingModel;
using WebAPI.Data.Context;
using WebAPI.Data.ScaffoldedEntity;
using Microsoft.AspNetCore.Identity;
using CustomIdentity.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;

namespace WebAPI.Controllers
{
    public class ProductController : Controller
    {
        private readonly ScubrDBContext _db;
        private readonly IConfiguration config;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly string coverImageFolderPath = string.Empty;
        public ProductController(ScubrDBContext db, IConfiguration config, UserManager<AppUser> userManager, IWebHostEnvironment hostingEnvironment)
        {
            this._db = db;
            this.config = config;
            this._userManager = userManager;
            this._hostingEnvironment = hostingEnvironment;
            coverImageFolderPath = Path.Combine(_hostingEnvironment.WebRootPath, "Upload");
            if (!Directory.Exists(coverImageFolderPath))
            {
                Directory.CreateDirectory(coverImageFolderPath);
            }
        }

        [HttpPost("AddBrand")]
        public async void AddProduct([FromBody] AddBrandtBindingModel model)
        {
            var brand = new Brand
            {
                Name = model.Name
            };
            var res = await _db.AddAsync(brand);
            _db.SaveChanges();
        }

        [HttpPost("AddCategory")]
        public async void AddCategory([FromBody] AddCategoryBindingModel model)
        {
            var category = new Category
            {
                Name = model.Name
            };
            var res = await _db.AddAsync(category);
            _db.SaveChanges();
        }

        [HttpPost("AddProduct")]
        public async Task<Object> AddProduct([FromBody] AddProductBindingModel model, string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            try
            {
                var product = new Product
                {
                    ProductName = model.ProductName,
                    Price = model.price,
                    BrandId = model.BrandId,
                    CategoryId = model.categoryId
                };
                var res = await _db.AddAsync(product);
                int result = _db.SaveChanges();
                return result > 0 ? String.Format("product added successfully.", result) : "No updates have been written to the database.";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        [HttpGet("GetAllProduct")]
        public async Task<List<Product>> GetAllProducts([FromBody] AddProductBindingModel model)
        {
            return await Task.FromResult(_db.Products.ToList()).ConfigureAwait(true);
        }

        [HttpGet("GetCategoryList")]
        public async Task<IEnumerable<Category>> Categories()
        {
            return await Task.FromResult(_db.Categories.ToList()).ConfigureAwait(true);
        }
    }
}
