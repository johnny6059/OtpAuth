<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="OtpAuth.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainCPH" runat="server">
    <asp:MultiView ID="MultiViewPage" runat="server" ActiveViewIndex="0">
        <asp:View ID="ViewAnonymous" runat="server">
            <h2>Welcome, anonymous user!</h2>
            <ul>
                <li><a href="/Login.aspx">Login</a></li>
                <li><a href="/Register.aspx">Register</a></li>
            </ul>
        </asp:View>
        <asp:View ID="ViewAuthenticated" runat="server">
            <h2>
                <asp:Literal ID="LiteralWelcome" runat="server" Text="Welcome, {0}!" />
            </h2>
            <ul>
                <li><a href="/Logout.aspx">Logout</a></li>
            </ul>
            <h2>One-time Passwords (OTP)</h2>
            <asp:MultiView ID="MultiViewOtp" runat="server">
                <asp:View ID="ViewOtpDisabled" runat="server">
                    <p>OTP authentication is currently <b>disabled</b>.</p>
                    <asp:Button ID="OtpEnableButton" Text="Enable OTP" runat="server" CausesValidation="false" OnClick="OtpEnableButton_Click" />
                </asp:View>
                <asp:View ID="ViewOtpEnabled" runat="server">
                    <p>OTP authentication is currently <b>enabled</b>.</p>
                    <asp:Button ID="OtpDisableButton" Text="Disable OTP" runat="server" CausesValidation="false" OnClick="OtpDisableButton_Click" />
                </asp:View>
                <asp:View ID="ViewOtpSetup" runat="server">
                    <asp:Image ID="QrImage" runat="server" Style="float: right" />
                    <p>To enable OTP, scan the QR code on the right with your authentication application. Then enter the generated six-digit code below and click <b>Enable</b> button.</p>
                    <p>If you can't scan the code, enter the following secret to your generator:</p>
                    <p class="secret"><asp:Literal ID="LiteralSecret" runat="server" /></p>
                    <asp:HiddenField ID="HiddenSecret" runat="server" />
                    <table class="form">
                        <tbody>
                            <tr>
                                <th>
                                    <asp:Label runat="server" Text="Code:" AssociatedControlID="CodeTextBox" />
                                </th>
                                <td>
                                    <asp:TextBox ID="CodeTextBox" runat="server" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="CodeTextBox" Display="Dynamic" Text="*" ErrorMessage="Code is missing" />
                                    <asp:RegularExpressionValidator runat="server" ControlToValidate="CodeTextBox" Display="Dynamic" Text="*" ErrorMessage="Code is missing" ValidationExpression="[0-9]{6}" />
                                </td>
                            </tr>
                        </tbody>
                        <tfoot>
                            <tr>
                                <td colspan="2">
                                    <asp:ValidationSummary ID="ValidationSummaryForm" runat="server" />
                                    <asp:Button ID="OtpSetupButton" runat="server" Text="Enable" OnClick="OtpSetupButton_Click" />
                                </td>
                            </tr>
                        </tfoot>
                    </table>
                </asp:View>
                <asp:View ID="ViewOtpSetupFailed" runat="server">
                    <p>Sorry, OTP setup failed. The one-time password does not match. Either you entered invalid value of there is too big time skew between this server and your device.</p>
                    <asp:Button ID="Button1" Text="Try again" runat="server" CausesValidation="false" OnClick="OtpEnableButton_Click" />
                </asp:View>
            </asp:MultiView>
        </asp:View>
    </asp:MultiView>
</asp:Content>
