namespace ProductApi.Models {
    public class Product {
        /*
         *  product_id INT NOT NULL AUTO_INCREMENT,
            product_name VARCHAR(100) NOT NULL,
            category VARCHAR(50) NULL,
            price DECIMAL(10,0) NOT NULL,
            stock INT NOT NULL,
            created_at DATETIME 
         
         */
        public int ProductId { get; set; }
        public string ProdictName { get; set; }
        public string? Category { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public DateTime CreateAt { get; set; }


    }
}
