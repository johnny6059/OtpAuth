<%@ Page Title="Register New User" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="OtpAuth.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainCPH" runat="server">
    <h2>Register New User</h2>
    <asp:MultiView ID="MultiViewPage" ActiveViewIndex="0" runat="server">
        <asp:View ID="ViewForm" runat="server">
            <table class="form">
                <tbody>
                    <tr>
                        <th>
                            <asp:Label runat="server" Text="User name:" AssociatedControlID="UserNameTextBox" />
                        </th>
                        <td>
                            <asp:TextBox ID="UserNameTextBox" runat="server" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="UserNameTextBox" Display="Dynamic" Text="*" ErrorMessage="User name is missing" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <asp:Label runat="server" Text="Password:" AssociatedControlID="PasswordTextBox" />
                        </th>
                        <td>
                            <asp:TextBox ID="PasswordTextBox" runat="server" TextMode="Password" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="PasswordTextBox" Display="Dynamic" Text="*" ErrorMessage="Password is missing" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <asp:Label runat="server" Text="Password confirmation:" AssociatedControlID="PasswordConfirmationTextBox" />
                        </th>
                        <td>
                            <asp:TextBox ID="PasswordConfirmationTextBox" runat="server" TextMode="Password" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="PasswordConfirmationTextBox" Display="Dynamic" Text="*" ErrorMessage="Password confirmation is missing" />
                            <asp:CompareValidator runat="server" ControlToCompare="PasswordTextBox" Operator="Equal" ControlToValidate="PasswordConfirmationTextBox" Display="Dynamic" Text="*" ErrorMessage="Password confirmation does not match the password" />
                        </td>
                    </tr>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="2">
                            <asp:ValidationSummary ID="ValidationSummaryForm" runat="server" />
                            <asp:Button ID="ButtonFormSubmit" runat="server" Text="Submit" OnClick="ButtonFormSubmit_Click" />
                        </td>
                    </tr>
                </tfoot>
            </table>
        </asp:View>
        <asp:View ID="ViewResult" runat="server">
            <p>
                <asp:Literal ID="LiteralMessage" runat="server" />
            </p>
        </asp:View>
    </asp:MultiView>
</asp:Content>
