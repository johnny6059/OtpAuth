<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="OtpAuth.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainCPH" runat="server">
    <h2>Login</h2>
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
                        <td>
                            :)
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
                        <td>
                            :(
                        </td>
                    </tr>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="2">
                            <asp:ValidationSummary runat="server" />
                            <asp:Button ID="ButtonFormSubmit" runat="server" Text="Submit" OnClick="ButtonFormSubmit_Click" />
                        </td>
                    </tr>
                </tfoot>
            </table>
        </asp:View>
        <asp:View ID="ViewOtp" runat="server">
            <table class="form">
                <tbody>
                    <tr>
                        <th>
                            <asp:Label runat="server" Text="Code:" AssociatedControlID="CodeTextBox" />
                        </th>
                        <td>
                            <asp:TextBox ID="CodeTextBox" runat="server" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="CodeTextBox" Display="Dynamic" Text="*" ErrorMessage="Code is missing" />
                            <asp:regularexpressionValidator runat="server" ControlToValidate="CodeTextBox" Display="Dynamic" Text="*" ErrorMessage="Code is invalid" ValidationExpression="[0-9]{6}" />
                        </td>
                    </tr>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="2">
                            <asp:ValidationSummary runat="server" />
                            <asp:Button ID="ButtonOtpFormSubmit" runat="server" Text="Submit" OnClick="ButtonOtpFormSubmit_Click" />
                        </td>
                    </tr>
                </tfoot>
            </table>
        </asp:View>
    </asp:MultiView>
</asp:Content>
