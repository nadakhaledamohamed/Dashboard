<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Pass_Default" Theme="Default" StylesheetTheme="Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <asp:HiddenField runat="server" ID="txtStatus" />
    <div class="row">
        <div class="col-xs-12">
            <div class="box">
                <div class="box-header">
                    <div class="box-name">
                        <i class="fa fa-list"></i>
                        <span>User Password</span>
                    </div>
                    <div class="box-icons">
                        <a class="collapse-link">
                            <i class="fa fa-chevron-up"></i>
                        </a>
                        <a class="expand-link">
                            <i class="fa fa-expand"></i>
                        </a>
                    </div>
                    <div class="no-move"></div>
                </div>

                <div class="box-content">
                    <div class="row">
                        <div class="col-md-2">
                            <label>UserID</label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txt_userID" runat="server" ReadOnly="true" Width="100%"></asp:TextBox>
							<asp:HiddenField ID="hdf_userID" runat="server" />
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <label>Username</label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txt_username" runat="server" ReadOnly="true" Width="100%"></asp:TextBox>
							<asp:HiddenField ID="hdf_userName" runat="server" />
                        </div>

                    </div>

                    <div class="row">
                        <div class="col-md-2">
                            <label>User Full Name</label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txt_name" runat="server" ReadOnly="true" Width="100%" ></asp:TextBox>
                        </div>

                    </div>
                    
                    <div class="row">
                        <div class="col-md-2">
                            <label>Current Password</label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txt_oldPass" runat="server" TextMode="Password" Width="100%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ValidationGroup="a1" ControlToValidate="txt_oldPass"></asp:RequiredFieldValidator>
                        </div>

                    </div>
                    
                    <div class="row">
                        <div class="col-md-2">
                            <label>New Password</label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txt_newPass" runat="server" TextMode="Password" MaxLength="20" Width="100%" ToolTip="Password at least 8 chars included at least 1 integer and any special char (~ ! @ # $ & _)"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ValidationGroup="a1" ControlToValidate="txt_newPass"></asp:RequiredFieldValidator>
                        </div>

                    </div>                    
                    
                    <div class="row">
                        <div class="col-md-2">
                            <label>New Password Confirm</label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txt_newPassConfirm" runat="server" TextMode="Password" MaxLength="20" Width="100%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ValidationGroup="a1" ControlToValidate="txt_newPassConfirm"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="*" ValidationGroup="a1" ControlToCompare="txt_newPass" ControlToValidate="txt_newPassConfirm"></asp:CompareValidator>
                        </div>

                    </div>
                    <br />
                    <br />
                    <div class="row">
					<div id="div_error" runat="server" visible ="false" class="col-md-12 has-error ui-state-error-text">                            
    <asp:Localize ID="lbl_error" runat="server"></asp:Localize>
                        </div>
                        <div class="col-md-12">                            
                            <asp:Button ID="btn_change" runat="server" Text="Change Password" ValidationGroup="a1" OnClick="btn_change_Click" />
                        </div>

                    </div>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>

