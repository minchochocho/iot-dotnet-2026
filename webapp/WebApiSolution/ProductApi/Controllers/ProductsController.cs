using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using ProductApi.Models;

namespace ProductApi.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase {
        private readonly string connString;
        /// <summary>
        /// 상품 리스트 조회
        /// </summary>
        /// <param name="configuration"></param>

        public ProductsController(IConfiguration configuration)
        {
            connString = configuration.GetConnectionString("TestDbConnection") ?? "";
        }
        [HttpGet]   // GET 메서드 선언(없어도 기본)
        public IActionResult GetProducts()
        {
            List<Product> products = new(); // new List<Product>(); 와 동일기능

            using var conn = new MySqlConnection(connString);
            conn.Open();

            string query =
                """
            SELECT product_id, product_name, category, price, stock, created_at
            FROM products
            ORDER BY product_id DESC
            """;    // 여러줄 문자열 @" 또는 """

            using var cmd = new MySqlCommand(query, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())   // 
            {
                Product product = new Product
                {
                    ProductId = reader.GetInt32("product_id"),
                    ProductName = reader.GetString("product_name"),
                    Category = reader.IsDBNull(reader.GetOrdinal("category")) ? null : reader.GetString("category"),
                    Price = reader.GetDecimal("price"),
                    Stock = reader.GetInt32("stock"),
                    CreatedAt = reader.GetDateTime("created_at")
                };

                products.Add(product);
            }

            return Ok(products);
        }
        /// <summary>
        /// 상품 단건 조회
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]   // GET /api/products/3
        public async Task<IActionResult> GetProductAsync(int id)
        {
            using var conn = new MySqlConnection(connString);
            conn.Open();

            string query = @"
                SELECT product_id, product_name, category, price, stock, created_at
                FROM products
                WHERE product_id = @ProductId
            ";  // MySQL, SqlServer 파라미터 문법 @파라미터명

            using var cmd = new MySqlCommand(query, conn);

            // @ProductId를 매핑하는 파라미터 생성
            cmd.Parameters.AddWithValue("@ProductId", id);

            using var reader = cmd.ExecuteReader();

            if (!await reader.ReadAsync())
            {
                return NotFound($"상품번호 {id}를 찾을 수 없습니다");
            }

            // 한건 가져와서 Product 객체로 만듦
            Product product = new Product
            {
                ProductId = reader.GetInt32("product_id"),
                ProductName = reader.GetString("product_name"),
                Category = reader.IsDBNull(reader.GetOrdinal("category")) ? null : reader.GetString("category"),
                Price = reader.GetDecimal("price"),
                Stock = reader.GetInt32("stock"),
                CreatedAt = reader.GetDateTime("created_at")
            };
            return Ok(product);

        }

        /// <summary>
        /// 상품 등록
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            using var conn = new MySqlConnection(connString);
            await conn.OpenAsync();

            string query = @"INSERT INTO testdb.products
                (
	                product_name, 
	                category, 
	                price, 
	                stock
                )
                VALUES(
	                @ProductName, 
	                @Category, 
	                @Price, 
	                @Stock
                );
            ";

            using var cmd = new MySqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
            cmd.Parameters.AddWithValue("@Category", (object?)product.Category ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Price", product.Price);
            cmd.Parameters.AddWithValue("@Stock", product.Stock);

            await cmd.ExecuteNonQueryAsync();

            using var idCmd = new MySqlCommand("SELECT LAST_INSERT_ID()", conn);
            var newId = Convert.ToInt32(await idCmd.ExecuteScalarAsync());

            product.ProductId = newId;
            product.CreatedAt = DateTime.Now;

            //return CreatedAtAction(
            //    nameof(GetProductAsync),
            //    new { id = product.ProductId },
            //    product
            //    );
            return Ok(product);
        }

        /// <summary>
        /// 상품 수정
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {

            using var conn = new MySqlConnection(connString);
            await conn.OpenAsync();

            string query = @"UPDATE products
                               SET 
                                     product_name = @ProductName, 
                                     category = @Category, 
                                     price = @Price, 
                                     stock = @Stock
                             WHERE product_id = @ProductId
            ";

            using var cmd = new MySqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
            cmd.Parameters.AddWithValue("@Category", (object?)product.Category ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Price", product.Price);
            cmd.Parameters.AddWithValue("@Stock", product.Stock);
            cmd.Parameters.AddWithValue("@ProductId", id);

            int result = await cmd.ExecuteNonQueryAsync();

            if (result == 0)
            {
                return NotFound($"상품번호 {id}를 찾을 수 없습니다.");
            }

            return Ok("상품이 수정되었습니다.");
        }

        // 웹에서는 PUT 거의 안씀 RESTAPI할때는 GET, POST, PUT, PATCH, DELETE 5개를 많이 씀

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateProductStock(int id, ProductStock product)
        {

            using var conn = new MySqlConnection(connString);
            await conn.OpenAsync();

            string query = @"UPDATE products
                               SET
                                     stock = @Stock
                             WHERE product_id = @ProductId
            ";

            using var cmd = new MySqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@Stock", product.Stock);
            cmd.Parameters.AddWithValue("@ProductId", id);

            int result = await cmd.ExecuteNonQueryAsync();

            if (result == 0)
            {
                return NotFound($"상품번호 {id}를 찾을 수 없습니다.");
            }

            return Ok("상품의 재고가 수정되었습니다.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            using var conn = new MySqlConnection(connString);
            await conn.OpenAsync();

            string query = @"DELETE FROM products WHERE product_id = @ProductId";

            using var cmd = new MySqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@ProductId", id);

            int result = await cmd.ExecuteNonQueryAsync();

            if (result == 0)
            {
                return NotFound($"상품번호 {id}를 찾을 수 없습니다.");
            }

            return Ok("상품이 삭제되었습니다.");
        }

        [HttpHead("{id}")]
        public IActionResult Head(int id)
        {
            return Ok();
        }

        [HttpOptions]
        public IActionResult Options()
        {
            Response.Headers.Append(
                "Allow",
                "GET, POST, PUT, PATCH, DELETE"
                );
            return Ok();
        }

        // GET, POST 쓸모없음
    }
}