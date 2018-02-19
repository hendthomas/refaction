namespace refactor_me.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web.Http;
    using refactor_me.Models;

    [RoutePrefix("products")]
    public class ProductsController : ApiController
    {
        /**
         * Returns all products
         **/
        [Route]
        [HttpGet]
        public IEnumerable<Product> GetAll()
        {
            using (ProductContext db = new ProductContext())
            {
                return db.Products.ToList();
            }
        }

        /**
         * Returns all products with the given name
         **/
        [Route]
        [HttpGet]
        public IEnumerable<Product> SearchByName(string name)
        {
            using (ProductContext db = new ProductContext())
            {
                return db.Products.Where(p => p.Name.ToLower() == name.ToLower()).ToList();
            }
        }

        /**
         * Returns a product with the given id
         **/
        [Route("{id}")]
        [HttpGet]
        public Product GetProduct(Guid id)
        {
            using (ProductContext db = new ProductContext())
            {
                List<Product> products = db.Products.Where(p => p.Id == id).ToList();
                // Throw exception if no products with id found
                if((products.Count) == 0)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                else
                {
                    return products.First();
                }
            }
        }

        /**
         * Creates the given product
         **/
        [Route]
        [HttpPost]
        public void Create(Product product)
        {
            using (ProductContext db = new ProductContext())
            {
                db.Products.Add(product);
                db.SaveChanges();
            }
        }
        
        /**
         * Updates the product with the given id using the given product
         **/
        [Route("{id}")]
        [HttpPut]
        public void Update(Guid id, Product product)
        {
            Product productToUpdate;
            using (ProductContext db = new ProductContext())
            {
                productToUpdate = db.Products.Where(p => p.Id == id).FirstOrDefault();                  
            }
            // Check the product to update exists
            if (productToUpdate != null)
            {
                productToUpdate = product;
            }
            using (ProductContext db = new ProductContext())
            {
                db.Entry(productToUpdate).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        /**
         * Deletes the product with the given id
         **/
        [Route("{id}")]
        [HttpDelete]
        public void Delete(Guid id)
        {
            using (ProductContext db = new ProductContext())
            {
                Product productToRemove = db.Products.Where(p => p.Id == id).FirstOrDefault();
                db.Products.Remove(productToRemove);
                db.SaveChanges();
            }
        }

        /**
         * Returns all product options for the given product id
         **/
        [Route("{productId}/options")]
        [HttpGet]
        public IEnumerable<ProductOption> GetOptions(Guid productId)
        {
            using (ProductContext db = new ProductContext())
            {
                return db.ProductOptions.Where(p => p.ProductId == productId).ToList();
            }
        }

        /**
         * Returns the product option with the given id
         **/
        [Route("{productId}/options/{id}")]
        [HttpGet]
        public ProductOption GetOption(Guid productId, Guid id)
        {
            using (ProductContext db = new ProductContext())
            {
                List<ProductOption> productOptions = db.ProductOptions.Where(p => p.Id == id).ToList();
                // Throw exception if no product options with id found
                if ((productOptions.Count) == 0)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                else
                {
                    return productOptions.First();
                }
            }
        }

        /**
         * Creates a new product option with the given product id using the given product option
         **/
        [Route("{productId}/options")]
        [HttpPost]
        public void CreateOption(Guid productId, ProductOption option)
        {
            option.ProductId = productId;
            using (ProductContext db = new ProductContext())
            {
                db.ProductOptions.Add(option);
                db.SaveChanges();
            }
        }

        /**
         * Updates a product option with the given id using the given product option
         **/
        [Route("{productId}/options/{id}")]
        [HttpPut]
        public void UpdateOption(Guid id, ProductOption option)
        {
            ProductOption productOptionToUpdate;
            using (ProductContext db = new ProductContext())
            {
                productOptionToUpdate = db.ProductOptions.Where(p => p.Id == id).FirstOrDefault();
            }
            // Check the product option to update exists
            if (productOptionToUpdate != null)
            {
                Guid productId = productOptionToUpdate.ProductId;
                productOptionToUpdate = option;
                productOptionToUpdate.ProductId = productId;
            }
            using (ProductContext db = new ProductContext())
            {
                db.Entry(productOptionToUpdate).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        /**
         * Deletes a product option with the given id
         **/
        [Route("{productId}/options/{id}")]
        [HttpDelete]
        public void DeleteOption(Guid id)
        {
            using (ProductContext db = new ProductContext())
            {
                ProductOption productOptionToRemove = db.ProductOptions.Where(p => p.Id == id).FirstOrDefault();
                db.ProductOptions.Remove(productOptionToRemove);
                db.SaveChanges();
            }
        }
    }
}
