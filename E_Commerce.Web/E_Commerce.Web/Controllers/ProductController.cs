using E_Commerce.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController] // =>handle formate response ,check model state is valid or not, if model is not valid return bad request 400
    public class ProductController : ControllerBase
    {
        // Swagger donot care of name of function or parameter jest care of http method
        //[HttpGet("{id}")] //Get: baseurl/api/Product/{id}
        //public ActionResult<Product> GetProduct(int id)
        //{
        //    return new Product() { Id = id };
        //}

        //    [HttpGet] //Get: baseurl/api/Product
        //    public ActionResult<Product> GetProduct()
        //    {
        //        return new Product() { Id = 10 };
        //    }

        //    [HttpPost]
        //    public ActionResult<Product> AddProduct()
        //    {
        //        return new Product() { Id = 10 };
        //    }

        //    [HttpPut]
        //    public ActionResult<Product> UpdateProduct()
        //    {
        //        return new Product() { Id = 10 };
        //    }

        //    [HttpDelete]
        //    public ActionResult<Product> DeleteProduct()
        //    {
        //        return new Product() { Id = 10 };
        //    }

        //}
    }
}
