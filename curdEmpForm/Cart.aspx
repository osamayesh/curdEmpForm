<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="ShoppingCart.Cart" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Shopping Cart</title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2 class="my-4">Shopping Cart</h2>

            <asp:Repeater ID="rptCart" runat="server">
                <HeaderTemplate>
                    <table class="table table-bordered">
                        <thead>
                            <tr>
                                <th>Product</th>
                                <th>Price</th>
                                <th>Quantity</th>
                                <th>Total</th>
                                <th>Action</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                            <tr>
                                <td><%# Eval("ProductName") %></td>
                                <td>$<%# Eval("Price") %></td>
                                <td>
                                    <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" Text='<%# Eval("Quantity") %>' Width="50px" />
                                </td>
                                <td>$<%# (Convert.ToDouble(Eval("Price")) * Convert.ToInt32(Eval("Quantity"))) %></td>
                                <td>
                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CommandArgument='<%# Eval("ProductId") %>' CssClass="btn btn-secondary" />
                                    <asp:Button ID="btnRemove" runat="server" Text="Remove" CommandArgument='<%# Eval("ProductId") %>'  CssClass="btn btn-danger" />
                                </td>
                            </tr>
                </ItemTemplate>
                <FooterTemplate>
                        </tbody>
                    </table>
                </FooterTemplate>
            </asp:Repeater>

            <asp:Label ID="lblTotalPrice" runat="server" CssClass="h4"></asp:Label>
            <br />
            <asp:Button ID="btnCheckout" runat="server" Text="Checkout" CssClass="btn btn-success"  />
        </div>
    </form>
</body>
</html>
