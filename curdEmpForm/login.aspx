<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="curdEmpForm.login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-5">
        <div class="row align-items-center">
            <div class="col-lg-7 mb-5 mb-lg-0">
                <div class="pr-lg-5">
                    <img src="Helper/img/illustration.svg" alt="Illustration" class="img-fluid" />
                </div>
            </div>
            <div class="col-lg-5">
                <h1 class="text-primary text-uppercase mb-4">Sign Up</h1>
                <h2 class="mb-4">Welcome!</h2>
                


                <div class="form-group">
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Email" TextMode="Email" />
                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" 
                        ErrorMessage="Email is required" Display="Dynamic" CssClass="text-danger" />
                    <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" 
                        ErrorMessage="Invalid Email format" Display="Dynamic" CssClass="text-danger" 
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                </div>

                <div class="form-group">
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" placeholder="Password" TextMode="Password" />
                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" 
                        ErrorMessage="Password is required" Display="Dynamic" CssClass="text-danger" />
                </div>

                <div class="form-group">
                    <div class="custom-control custom-checkbox">
                        <asp:CheckBox ID="chkRememberMe" runat="server" Text="&nbsp;Remember Me" CssClass="custom-control-input" />
                    </div>
                </div>

                <div class="form-group">
                    <p class="mb-0">dont  have an account? <a href="sginup.aspx" class="text-primary">Sign up here</a></p>
                    <a href="ResetPassword.aspx" class="text-primary">Forgot Password?</a>
                </div>

                <asp:Button ID="btnlogin" runat="server" Text="login" CssClass="btn btn-primary btn-block" OnClick="btnlogin_Click"  />
                                   <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>

                </div>
        </div>
    </div>

    <footer class="bg-light py-3 mt-5">
        <div class="container">
            <div class="row">
                <div class="col-md-6 text-center text-md-right">
                </div>
            </div>
        </div>
    </footer>
</asp:Content>