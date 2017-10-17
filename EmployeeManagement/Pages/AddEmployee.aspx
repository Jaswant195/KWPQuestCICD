<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEmployee.aspx.cs" Inherits="EmployeeManagement.Pages.AddEmployee" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Text=""></asp:Label><br />
            <br />
            Name :
            <asp:TextBox ID="txtName" runat="server"></asp:TextBox><br />
            <br />
            Age :
            <asp:TextBox ID="txtAge" runat="server"></asp:TextBox><br />
            <br />
            Salary :
            <asp:TextBox ID="txtSalary" runat="server"></asp:TextBox><br />
            <br />
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="SaveEmployee" />

        </div>
    </form>
</body>
</html>
