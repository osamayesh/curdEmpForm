using curdEmpForm;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Linq;

namespace ShoppingCart
{
    public partial class Products : System.Web.UI.Page
    {
        sampleEmpDBEntities db = new sampleEmpDBEntities();
        // Dummy product list


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProducts();
            }
        }

        private void BindProducts()
        {
            var products = db.Products.ToList();
            rptProducts.DataSource = db.Products.ToList(); ;
            rptProducts.DataBind();
        }

        // Add product to cart
        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            int productId = Convert.ToInt32(button.CommandArgument);
            AddToCart(productId);
        }

        // Add selected product to cart and save in Session
        private void AddToCart(int productId)
        {
            // Fetch product from the database
            var product = db.Products.FirstOrDefault(p => p.ProductId == productId);

            if (product != null)
            {
                // Get cart from session, or create a new cart if it doesn't exist
                List<CartItem> cart = Session["Cart"] as List<CartItem> ?? new List<CartItem>();

                // Find if the product already exists in the cart
                var cartItem = cart.FirstOrDefault(c => c.ProductId == productId);
                if (cartItem != null)
                {
                    // If product exists, increase the quantity
                    cartItem.Quantity++;
                }
                else
                {
                    // Otherwise, add a new product to the cart
                    cart.Add(new CartItem
                    {
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        Price = product.Price,
                        Quantity = 1
                    });
                }

                // Save the cart back to the session
                Session["Cart"] = cart;
            }
        }
    }

    // Cart item class to represent items in the cart
    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}

    

