<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="curdEmpForm.Admin" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Admin - Manage Users</title>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Manage Users</h2>

        <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="UserName" HeaderText="Username" />
                <asp:BoundField DataField="UserEmail" HeaderText="Email" />
                <asp:TemplateField HeaderText="Roles">
                    <ItemTemplate>
                        <%# String.Join(", ", Eval("Roles").ToString().Split(',')) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <asp:Button ID="btnAssignRole" runat="server" Text="Assign Role" CommandArgument='<%# Eval("UserId") %>' OnClick="btnAssignRole_Click" />
                     <asp:Button ID="btnRemoveRole" Text="Remove Role" CommandArgument='<%# Eval("UserId") %>' OnClick="btnRemoveRole_Click" runat="server" />
                        </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <asp:DropDownList ID="ddlRoles" runat="server">
        </asp:DropDownList>
    </form>
</body>
</html>


