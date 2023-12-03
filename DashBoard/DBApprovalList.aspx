<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DBApprovalList.aspx.cs" Inherits="_DBAPPList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css"/>
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
    <title></title>
 <style type="text/css">
.hrDiv {
	width:50%;
    text-align:left;
    margin-left:0
	
}
.containerDiv {
	 width:80%;
     height:80%;   
    border-color:lightgrey;
     padding-left: 20px;
      padding-top: 20px;
      padding-bottom: 20px;
      padding-right: 20px;
      position:relative;
}
.MsgDiv {
	width:80%;
	border:solid;
    border-color:lightgrey;
     padding-left: 20px;
      padding-top: 20px;
      padding-bottom: 20px;
      padding-right: 20px;
}


</style>

</head>
<body>
    <form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">  
</asp:ScriptManager>  
    <div>
    
    </div>
          <div  style="width:80%;position:relative" >
                 <asp:GridView runat="server" ID="gvForms" AutoGenerateColumns="false"
                CssClass="table table-bordered table- striped table-hover table-heading table-datatable" Width="100%">
                <Columns>
                     <asp:BoundField HeaderText="Approval" DataField="ApprovalName" />
                    <asp:BoundField HeaderText="Decission" DataField="StatusName" />
                    <asp:BoundField HeaderText="Decission By" DataField="Emp_Name" />
                    <asp:BoundField HeaderText="Decission Date" DataField="DecisionDate" />                   
                    </Columns>
               </asp:GridView>
                      
             
              
          </div>
    </form>
</body>
</html>
