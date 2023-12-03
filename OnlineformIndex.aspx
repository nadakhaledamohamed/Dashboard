<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="OnlineformIndex.aspx.cs" Inherits="NotAllow" Theme="Default" StylesheetTheme="Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.2/css/all.css" />
    <style type="text/css">  
        .download  
        {  
            font-size:x-large;
            font-weight:bold;
        }  
  
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align:center; vertical-align:middle; width:100%;">
      <label style="font-size:xx-large;color:black">This document shows how to use online form  , please read carefully </label>
  <asp:LinkButton ID="helppdf" runat="server" OnClick="helppdf_Click" CssClass="download"><p>download file</p></asp:LinkButton>
    </div>
</asp:Content>

