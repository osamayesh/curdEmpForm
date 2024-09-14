<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="ShoppingCart.Products" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Products</title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .card-deck {
            margin-bottom: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2 class="my-4">Available Products</h2>
            <div class="row">
                <asp:Repeater ID="rptProducts" runat="server">
                    <ItemTemplate>
                        <div class="col-md-4 mb-4">
                            <div class="card">
                                <img src='<%# Eval("IamgePath") %>' class="card-img-top" alt="Product Image">
                                <div class="card-body">
                                    <h5 class="card-title"><%# Eval("ProductName") %></h5>
                                    <p class="card-text">$<%# Eval("UnitPrice") %></p>
                                    <p class="card-text"><small class="text-muted">Category: <%# Eval("CategoryID") %></small></p>
                                    <asp:Button ID="btnAddToCart" runat="server" Text="Add to Cart" CssClass="btn btn-primary" CommandArgument='<%# Eval("ProductId") %>' OnClick="btnAddToCart_Click" />
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>

            <nav>
                <ul class="pagination">
                    <li class="page-item">
                        <a class="page-link" href="Products.aspx?page=1">1</a>
                    </li>
                    <li class="page-item">
                        <a class="page-link" href="Products.aspx?page=2">2</a>
                    </li>
                    <li class="page-item">
                        <a class="page-link" href="Products.aspx?page=3">3</a>
                    </li>
                    <!-- Add more pages as needed -->
                </ul>
            </nav>

            <a href="Cart.aspx" class="btn btn-success mt-3">View Cart</a>
        </div>
    </form>
</body>
</html>
