using curdEmpForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace ShoppingCart
{
    public partial class Cart : System.Web.UI.Page
    {
        sampleEmpDBEntities db=new sampleEmpDBEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCart();
            }
        }

        // Method to bind the cart items
        private void BindCart()
        {
            List<CartItem> cart = Session["Cart"] as List<CartItem> ?? new List<CartItem>();

            rptCart.DataSource = cart.Select(c => new
            {
                c.ProductId,
                c.ProductName,
                c.Price,
                c.Quantity,
                Subtotal = c.Price * c.Quantity // Calculate the subtotal
            }).ToList();

            rptCart.DataBind();

            // Update the total price label
            lblTotalPrice.Text = cart.Sum(c => c.Price * c.Quantity).ToString("0.00");
        }

        // Remove item from the cart
        protected void btnRemove_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            int productId = Convert.ToInt32(button.CommandArgument);

            List<CartItem> cart = Session["Cart"] as List<CartItem>;

            if (cart != null)
            {
                // Find the cart item and remove it
                var itemToRemove = cart.FirstOrDefault(c => c.ProductId == productId);
                if (itemToRemove != null)
                {
                    cart.Remove(itemToRemove);
                    Session["Cart"] = cart; // Update the session
                }
            }

            // Rebind the cart to refresh the items
            BindCart();
        }

        // Handle checkout logic (empty cart after checkout for demo)
        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            // Implement checkout logic here (e.g., save to database)

            // Empty the cart after checkout
            Session["Cart"] = null;
            BindCart();

            lblTotalPrice.Text = "0.00"; // Reset total price
        }
    }

    // Cart item class representing items in the user's cart
    
}
