using DataAccess;
using DataAccess.Contracts;
using DataAccess.DTO;
using DataAccess.Factories;
using DataAccess.Repositories;
using System;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Routing;
using WebInventory.Helpers;

namespace WebInventory.Controllers.api
{
    public class ProductController : ApiController
    {
        IProductRepository _repository;
        ProductFactory _productFactory = new ProductFactory();
        const int maxPageSize = 10;

        public ProductController()
        {
            _repository = new ProductRepository(new SafetyInventoryEntities());
        }

        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IHttpActionResult Get(Guid Id)
        {
            try
            {
                var expenseGroup = _repository.GetProduct(Id);

                if (expenseGroup != null)
                {
                    return Ok(_productFactory.CreateProduct(expenseGroup));
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("api/product", Name = "ProductList")]
        public IHttpActionResult Get(string sort = "Id", string status = null, Guid userId = new Guid(),
                                     int page = 1, int pageSize = maxPageSize)
        {
            try
            {
                int statusId = -1;
                if (status != null)
                {
                    switch (status.ToLower())
                    {
                        case "open":
                            statusId = 1;
                            break;
                        case "confirmed":
                            statusId = 2;
                            break;
                        case "processed":
                            statusId = 3;
                            break;
                        default:
                            break;
                    }
                }
                
                // get expensegroups from repository
                var expenseGroups = _repository.GetProducts() 
                    .ApplySort(sort)
                    .Where(eg => (userId == null || eg.Id == userId));

                // ensure the page size isn't larger than the maximum.
                if (pageSize > maxPageSize)
                {
                    pageSize = maxPageSize;
                }

                // calculate data for metadata
                var totalCount = expenseGroups.Count();
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                var urlHelper = new UrlHelper(Request);
                var prevLink = page > 1 ? urlHelper.Link("ProductList",
                    new
                    {
                        page = page - 1,
                        pageSize = pageSize,
                        sort = sort
                        ,
                        status = status,
                        userId = userId
                    }) : "";
                var nextLink = page < totalPages ? urlHelper.Link("ProductList",
                    new
                    {
                        page = page + 1,
                        pageSize = pageSize,
                        sort = sort
                        ,
                        status = status,
                        userId = userId
                    }) : "";


                var paginationHeader = new
                {
                    currentPage = page,
                    pageSize = pageSize,
                    totalCount = totalCount,
                    totalPages = totalPages,
                    previousPageLink = prevLink,
                    nextPageLink = nextLink
                };

                HttpContext.Current.Response.Headers.Add("X-Pagination",
                    Newtonsoft.Json.JsonConvert.SerializeObject(paginationHeader));
                
                // return result
                return Ok(expenseGroups
                    .Skip(pageSize * (page - 1))
                    .Take(pageSize)
                    .ToList()
                    .Select(eg => _productFactory.CreateProduct(eg)));
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("api/product")]
        public IHttpActionResult Post([FromBody]ProductDto product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest();
                }

                // try mapping & saving
                var eg = _productFactory.CreateProduct(product);

                var result = _repository.InsertProduct(eg);
                if (result.Status == RepositoryActionStatus.Created)
                {
                    // map to dto
                    var newProduct = _productFactory.CreateProduct(result.Entity);
                    return Created<ProductDto>(Request.RequestUri
                                                     + "/" + newProduct.Id.ToString(), newProduct);
                }

                return BadRequest();

            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpPut]
        public IHttpActionResult Put(Guid id, [FromBody]ProductDto product)
        {
            try
            {
                if (product == null)
                    return BadRequest();

                // map
                var eg = _productFactory.CreateProduct(product);

                var result = _repository.UpdateProduct(eg);
                if (result.Status == RepositoryActionStatus.Updated)
                {
                    // map to dto
                    var updatedProduct = _productFactory
                        .CreateProduct(result.Entity);
                    return Ok(updatedProduct);
                }
                else if (result.Status == RepositoryActionStatus.NotFound)
                {
                    return NotFound();
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(Guid id)
        {
            try
            {
                var result = _repository.DeleteProduct(id);

                if (result.Status == RepositoryActionStatus.Deleted)
                {
                    return StatusCode(HttpStatusCode.NoContent);
                }
                else if (result.Status == RepositoryActionStatus.NotFound)
                {
                    return NotFound();
                }

                return BadRequest();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}
