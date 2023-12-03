<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage2.master" AutoEventWireup="true" Theme="Default" StylesheetTheme="Default" CodeFile="FormTypeSetting.aspx.cs" Inherits="Finance_Request" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .Background {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .Popup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 50%;
            height: 80%;
        }

        .lbl {
            font-size: 16px;
            font-style: italic;
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server" Visible="true">
    <div id="RequestDiv" runat="server" visible="false">
        <div class="box-content" style="overflow-x: scroll; overflow-y: scroll;">
            <asp:GridView runat="server" ID="gvForms" AutoGenerateColumns="false"
                CssClass="table table-bordered table-striped table-hover table-heading table-datatable" Width="100%"
                OnRowCommand="gvTypes_RowCommand" OnRowDataBound="gvForms_RowDataBound">
                <Columns>
        
                    <asp:TemplateField HeaderText="Forms">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddForms" runat="server"></asp:DropDownList>
                             <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator122" runat="server" ErrorMessage="*" Text="*" ValidationGroup="NewCompany" Style="font-weight: bold; font-size: 18px" ControlToValidate="ddForms" InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                        </ItemTemplate>

                        <ItemStyle Width="75px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Types">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddTypes" runat="server"></asp:DropDownList>
                             <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="*" Text="*" ValidationGroup="NewCompany" Style="font-weight: bold; font-size: 18px" ControlToValidate="ddTypes" InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                        </ItemTemplate>

                        <ItemStyle Width="75px" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Duration / Working Days ">
                        <ItemTemplate>
                            <asp:TextBox ID="txtDurarion" runat="server" Type="Number" Text='<%# Eval("DurationInDays") %>'></asp:TextBox>
                        </ItemTemplate>

                        <ItemStyle Width="75px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Fees ">
                        <ItemTemplate>
                            <asp:TextBox ID="txtfees" runat="server" Type="Number" Text='<%# Eval("Cost") %>'></asp:TextBox>
                        </ItemTemplate>

                        <ItemStyle Width="75px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Active ">
                        <ItemTemplate>
                            <asp:CheckBox ID="checkActive" runat="server" ></asp:CheckBox>
                        </ItemTemplate>

                        <ItemStyle Width="75px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Save">
                        <ItemTemplate>
                            <asp:LinkButton CssClass="loadiframe" CommandName="save" CommandArgument='<%# Eval("FormTypeId") %>' runat="server" ID="btnSave" CausesValidation="true" ValidationGroup="NewCompany"> <i class="fa fa-save"></i>
                            </asp:LinkButton>
                            <asp:LinkButton CssClass="loadiframe" CommandName="modify" CommandArgument='<%# Eval("FormTypeId") %>' runat="server" ID="btnEdit" CausesValidation="true" ValidationGroup="NewCompany"> <i class="fa fa-edit"></i>
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="75px" />
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </div>
    </div>
    <script>
        function clickbtn(ctrl) {
            debugger;
            var Detail = ctrl.getAttribute("data-Detailid");
            debugger;
            //var src = "http://localhost:64132//RequestReport.aspx?detid=" + Detail
            var src = location.href.substring(0, location.href.lastIndexOf("/") + 1) + "RequestReport.aspx?detid=" + Detail
            $('iframe').attr('src', src);
            // $('#irm1').attr('src', src);
        }
    </script>

</asp:Content>

