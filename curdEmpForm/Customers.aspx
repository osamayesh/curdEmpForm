<%@ Page Title="Customers" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Customers.aspx.cs" Inherits="curdEmpForm.Customers" %>
   <div class="container">
    <h2>File Upload and Download</h2>
    
    <!-- File Upload Section -->
    <div class="row mb-4">
        <div class="col-md-6">
            <h3>Upload Files</h3>
            <asp:FileUpload ID="fileUpload" runat="server" AllowMultiple="true" CssClass="form-control-file" />
            <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" CssClass="btn btn-primary mt-2" />
            <asp:Label ID="lblUploadStatus" runat="server" CssClass="mt-2"></asp:Label>
        </div>
    </div>

    <!-- File Download Section -->
    <div class="row">
        <div class="col-md-6">
            <h3>Download Files</h3>
            <asp:GridView ID="gvFiles" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" OnRowCommand="gvFiles_RowCommand">
                <Columns>
                    <asp:BoundField DataField="FileName" HeaderText="File Name" />
                    <asp:BoundField DataField="FileSize" HeaderText="Size (bytes)" />
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" CommandName="DownloadFile" CommandArgument='<%# Eval("FileName") %>' CssClass="btn btn-sm btn-secondary" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</div>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     
    <asp:Button ID="txtordr" runat="server" OnClick="Button2_Click" Text="order" />
  
    <asp:Button ID="Button1" runat="server" Height="27px" OnClick="Button1_Click" Text="showsomecol" Width="108px" />
    <asp:Button ID="txtsreach" runat="server" Text="search" OnClick="sreach_Click" />
    <asp:TextBox ID="txtsearching" runat="server"></asp:TextBox>
    <asp:GridView ID="grd" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
    <AlternatingRowStyle BackColor="White" />
    <EditRowStyle BackColor="#2461BF" />
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
    <RowStyle BackColor="#EFF3FB" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    <SortedAscendingCellStyle BackColor="#F5F7FB" />
    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
    <SortedDescendingCellStyle BackColor="#E9EBEF" />
    <SortedDescendingHeaderStyle BackColor="#4870BE" />
</asp:GridView>
</asp:Content>
