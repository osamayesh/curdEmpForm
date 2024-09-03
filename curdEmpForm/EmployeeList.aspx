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
        <div class="modal fade" id="mymodal" data-backdrop="false" role="dialog">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Add New Employee</h4>
                        <asp:Label ID="lblmsg" Text="" runat="server"></asp:Label>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>
                    <div class="modal-body">
                        <label>EmployeeId:</label>
                        <asp:TextBox ID="TxtId" CssClass="form-control" Placeholder="EmployeeId" runat="server" />

                        <label>EmployeeName:</label>
                        <asp:TextBox ID="txtEmpName" CssClass="form-control" Placeholder="EmployeeName" runat="server" />

                        <label>EmployeeNumber:</label>
                        <asp:TextBox ID="txtEmpNumber" CssClass="form-control" Placeholder="EmployeeNumber" runat="server" />

                        <label>Date of Birth:</label>
                        <asp:TextBox ID="txtDateOfBirth" CssClass="form-control" Placeholder="YYYY-MM-DD" runat="server" />

                        <label for="ddlGender">Gender</label>
                        <asp:DropDownList ID="ddlGender" runat="server">
                            <asp:ListItem Text="Male" Value="Male" />
                            <asp:ListItem Text="Female" Value="Female" />
                        </asp:DropDownList>
                        <br />

                        <label> Salary:</label>
                        <asp:TextBox ID="TxtSalary" CssClass="form-control" Placeholder="Salary" runat="server" />

                        <label>Position:</label>
                        <asp:TextBox ID="TxtPosition" CssClass="form-control" Placeholder="Position" runat="server" />
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                        <asp:Button ID="btnSave" CssClass="btn btn-primary" OnClick="btnsave_Click" Text="Save" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Section for Searching -->
    <section id="section">
        <div class="row match-height">
            <div class="col-12">
                <div class="card-header">
                    <asp:Button Text="Open Modal" ID="modal" CssClass="btn btn-primary" OnClick="modal_Click" runat="server" />
                    <asp:TextBox ID="txtSearch" CssClass="form-control" Placeholder="Search by Name or ID" runat="server" />
                    <asp:Button Text="Search" ID="btnSearch" CssClass="btn btn-primary" OnClick="btnSearch_Click" runat="server" />
                </div>
                <div class="card-content">
                    <div class="card-body">
                        <div class="row">
                            <div class="col col-md-12 col-12">
                                <asp:GridView ID="gvResults" runat="server" CssClass="table table-striped" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="Id" HeaderText="EmployeeId" />
                                        <asp:BoundField DataField="EmpName" HeaderText="EmployeeName" />
                                        <asp:BoundField DataField="EmpNumber" HeaderText="EmployeeNumber" />
                                        <asp:BoundField DataField="Gender" HeaderText="Gender" />
                                        <asp:BoundField DataField="Date_of_birth" HeaderText="Date of Birth" DataFormatString="{0:yyyy-MM-dd}" />
                                        <asp:BoundField DataField="Salary" HeaderText="Salary" />
                                        <asp:BoundField DataField="Position" HeaderText="Position" />
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnupdate" CommandName="Update" OnCommand="btnupdate_Command" CommandArgument='<%#Eval("Id") %>' CssClass="btn btn-sm btn-primary" runat="server">
                                                    <i class="fas fa-pencil-alt"></i>
                                                </asp:LinkButton>
                                                <asp:LinkButton CommandName="Delete" ID="btndlt" CommandArgument='<%#Eval("Id") %>' OnCommand="btndlt_Command" OnClientClick="return confirm('Are you sure you want to delete this record?');" CssClass="btn btn-sm btn-danger" runat="server">
                                                    <i class="fas fa-trash-alt"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <asp:SqlDataSource ID="ds1" ConnectionString="<%$ConnectionStrings:connection_ %>" runat="server" SelectCommand="SELECT * FROM Employee" />

    <!-- Load jQuery first -->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.bundle.min.js"></script>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Include Font Awesome -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" rel="stylesheet" />
</asp:Content>
