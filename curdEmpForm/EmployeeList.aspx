<%@ Page Title="Employee List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmployeeList.aspx.cs" Inherits="curdEmpForm.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<style>
    table{
        border-collapse:collapse;
        width:100%;
    }
    th, td {
        text-align:left;
        padding:8px;
    }
    th {
        background-color:#4CAF50;
        color:white;
    }
    tr:nth-child(even) {
        background-color:#f2f2f2;
    }
    tr.separator {
        border-top:1px solid #ddd;
        border-bottom:1px solid #ddd;
    }
</style>
    <div class="container">
        <div class="modal fade" id="employeeModal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="employeeModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="employeeModalLabel">Employee Details</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <asp:HiddenField ID="hdnEmployeeId" runat="server" />
                        
                        <div class="form-group">
                            <label for="txtEmpName">Employee Name:</label>
                            <asp:TextBox ID="txtEmpName" runat="server" CssClass="form-control" />
                        </div>
                        
                        <div class="form-group">
                            <label for="txtEmpNumber">Employee Number:</label>
                            <asp:TextBox ID="txtEmpNumber" runat="server" CssClass="form-control" />
                        </div>
                        
                        <div class="form-group">
                            <label for="ddlGender">Gender:</label>
                            <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Male" Value="Male" />
                                <asp:ListItem Text="Female" Value="Female" />
                            </asp:DropDownList>
                        </div>
                        
                        <div class="form-group">
                            <label for="txtDateOfBirth">Date of Birth:</label>
                            <asp:TextBox ID="txtDateOfBirth" runat="server" CssClass="form-control" TextMode="Date" />
                        </div>
                        
                        <div class="form-group">
                            <label for="txtSalary">Salary:</label>
                            <asp:TextBox ID="txtSalary" runat="server" CssClass="form-control" TextMode="Number" Step="0.01" />
                        </div>
                        
                        <div class="form-group">
                            <label for="txtPosition">Position:</label>
                            <asp:TextBox ID="txtPosition" runat="server" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        <asp:Button ID="btnSaveEmployee" runat="server" Text="Save Changes" CssClass="btn btn-primary" OnClick="btnSaveEmployee_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="container mt-4">
        <h2>Employee List</h2>
        <!-- Search functionality -->
        <div class="row mb-3">
            <div class="col-md-6">
                <div class="input-group">
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search by ID or Name"></asp:TextBox>
                    <div class="input-group-append">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                    </div>
                </div>
            </div>
            <div class="col-md-6 text-right">
            </div>
        </div>
        <asp:Button ID="btnAddNewEmployee" runat="server" Text="Add New Employee" CssClass="btn btn-success mb-3" OnClick="btnAddNewEmployee_Click" />
        
        <asp:GridView ID="gvEmployees" runat="server" CssClass="table table-striped" AutoGenerateColumns="False" DataKeyNames="Id">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="ID" />
                <asp:BoundField DataField="EmpName" HeaderText="Name" />
                <asp:BoundField DataField="EmpNumber" HeaderText="Employee Number" />
                <asp:BoundField DataField="Gender" HeaderText="Gender" />
                <asp:BoundField DataField="Date_of_birth" HeaderText="Date of Birth" DataFormatString="{0:d}" />
                <asp:BoundField DataField="Salary" HeaderText="Salary" DataFormatString="{0:C}" />
                <asp:BoundField DataField="Position" HeaderText="Position" />
                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkUpdate" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lnkUpdate_Click" CommandArgument='<%# Eval("Id") %>'>
                            <i class="fas fa-edit"></i> Update
                        </asp:LinkButton>
                        <asp:LinkButton ID="lnkDelete" runat="server" CssClass="btn btn-danger btn-sm" OnClick="lnkDelete_Click" CommandArgument='<%# Eval("Id") %>' OnClientClick="return confirm('Are you sure you want to delete this employee?');">
                            <i class="fas fa-trash-alt"></i> Delete
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

    <script type="text/javascript">
        function openEmployeeModal() {
            $('#employeeModal').modal('show');
        }
    </script>
     <!-- Load jQuery first -->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.bundle.min.js"></script>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Include Font Awesome -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" rel="stylesheet" />
</asp:Content>