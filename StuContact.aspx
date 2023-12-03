<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StuContact.aspx.cs" Inherits="_StuContact" Theme="Default" StylesheetTheme="Default" MaintainScrollPositionOnPostback="true" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="content-type" content="text/html; charset=utf-8" />
    <title>NGU Student Contact Form</title>
    <link href="plugins/jquery-ui/jquery-ui.min.css" rel="stylesheet" />
    <link href="font-awesome/css/font-awesome.css" rel="stylesheet" />
    <%--    <link href='http://fonts.googleapis.com/css?family=Righteous' rel='stylesheet' type='text/css' />--%>
    <link href="plugins/fancybox/jquery.fancybox.css" rel="stylesheet" />
    <link href="plugins/fullcalendar/fullcalendar.css" rel="stylesheet" />
    <link href="plugins/xcharts/xcharts.min.css" rel="stylesheet" />
    <link href="fonts/Helvetica.css" rel="stylesheet" />
    <link href="fonts/droidarabickufi.css" rel="stylesheet" />
    <link href="plugins/TimePicker/timedropper.css" rel="stylesheet" />
    <link href="plugins/datatables/jquery.dataTables.css" rel="stylesheet" />
    <link rel="shortcut icon" type="image/x-icon" href="favicon.ico" />
    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
				            <script src="http://getbootstrap.com/docs-assets/js/html5shiv.js"></script>
				            <script src="http://getbootstrap.com/docs-assets/js/respond.min.js"></script>
		            <![endif]-->
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <link href="/plugins/datatables/buttons.bootstrap.min.css" rel="stylesheet" />

</head>
<body>
    <style>
        .dt-buttons a {
            margin-left: 5px;
            margin-right: 5px;
            border: 1px solid #ccc;
            background-color: #808080;
            padding: 5px;
            color: white;
        }
        .auto-style1 {
            text-align: center;
            text-indent: -.25in;
            font-size: 11.0pt;
            font-family: Calibri, sans-serif;
            margin-left: .5in;
            margin-right: 0in;
            margin-top: 0in;
            margin-bottom: .0001pt;
        }
        .auto-style2 {
            color: #FF3300;
        }
    </style>
    <form id="form1" runat="server">

        <!--Start Header-->
        <div id="screensaver">
            <canvas id="canvas"></canvas>
            <i class="fa fa-lock" id="screen_unlock"></i>
        </div>
        <div id="modalbox">
            <div class="devoops-modal">
                <div class="devoops-modal-header">
                    <div class="modal-header-name">
                        <span>Basic table</span>
                    </div>
                    <div class="box-icons">
                        <a class="close-link">
                            <i class="fa fa-times"></i>
                        </a>
                    </div>
                </div>
                <div class="devoops-modal-inner">
                </div>
                <div class="devoops-modal-bottom">
                </div>
            </div>
        </div>
        <header class="navbar">
            <div class="container-fluid expanded-panel">
                <div class="row">
                    <div id="logo" class="col-xs-12 col-sm-2">
                        <asp:HyperLink ID="hlLogo" runat="server">NGU Contact Form</asp:HyperLink>
                    </div>
                    <div id="top-panel" class="col-xs-12 col-sm-10">
                        <div class="row">
                            <div class="col-xs-8 col-sm-4">
                            </div>
                            <div class="col-xs-4 col-sm-8 top-panel-right">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </header>
        <!--End Header-->
        <!--Start Container-->
        <div id="main" class="container-fluid">
            <div class="row">
                <!--Start Content-->
                <div id="content" class="col-xs-12 col-sm-12">
                    <div id="ajax-content">
                        <asp:ScriptManager ID="sm" runat="server" EnablePageMethods="true">
                            <Scripts>
                                <asp:ScriptReference Path="~/plugins/jquery/jquery-2.1.0.min.js" />
                                <asp:ScriptReference Path="~/plugins/jquery-ui/jquery-ui.min.js" />
                                <asp:ScriptReference Path="~/plugins/bootstrap/bootstrap.min.js" />
                                <asp:ScriptReference Path="~/js/Content.js" />
                                <%--<asp:ScriptReference Path="~/js/persianumber.js" />--%>

                                <asp:ScriptReference Path="~/plugins/datatables/jquery.dataTables.min.js" />
                                <asp:ScriptReference Path="~/plugins/datatables/ZeroClipboard.js" />
                                <asp:ScriptReference Path="~/plugins/datatables/TableTools.js" />
                                <asp:ScriptReference Path="~/plugins/datatables/dataTables.bootstrap.js" />
                                <asp:ScriptReference Path="~/plugins/datatables/date-dd-MMM-yyyy.js" />
                                <asp:ScriptReference Path="~/plugins/jquery-ui/i18n/jquery.ui.datepicker-ar.min.js" />
                                <asp:ScriptReference Path="~/plugins/TimePicker/timedropper.js" />
                                <asp:ScriptReference Path="~/plugins/select2/select2.min.js" />

                                <asp:ScriptReference Path="~/plugins/datatables/dataTables.buttons.min.js" />
                                <asp:ScriptReference Path="~/plugins/datatables/pdfmake.min.js" />
                                <asp:ScriptReference Path="~/plugins/datatables/vfs_fonts.js" />
                                <asp:ScriptReference Path="~/plugins/datatables/jszip.min.js" />
                                <asp:ScriptReference Path="~/plugins/datatables/buttons.html5.min.js" />
                                <asp:ScriptReference Path="~/plugins/datatables/buttons.print.min.js" />
                            </Scripts>
                        </asp:ScriptManager>
                        <script>
                            function printDiv(divID) {
                                //Get the HTML of div
                                $('.co').css("display", "none");
                                $('.noPrint').css("display", "none");
                                var divElements = document.getElementById(divID).innerHTML;
                                //Get the HTML of whole page
                                var oldPage = document.body.innerHTML;
                                var div1 = divElements.replace("class=\"box-content2\"", "class=\"box-content2\" style=\"display:none;\"").replace("class=\"box-content3\"", "class=\"box-content3\" style=\"display:none;\"").replace("class=\"box-icons\"", "class=\"box-icons\" style=\"display:none;\"").replace("class=\"row pss\"", "class=\"row pss\" style=\"display:none;\"").replace("class=\"tab-pane\"", "class=\"tab-pane\" style=\"display:none;\"").replace("class=\"row-fluid\"", "class=\"row-fluid\" style=\"display:none;\"").replace("id=\"tabs\"", "id=\"tabs\" style=\"display:none;\"").replace("<footer", "<footer style=\"display:none;\"").replace("table table-bordered table-striped table-hover table-heading table-datatable", "table-bordered table-striped")
                                //Reset the page's HTML with div's HTML only
                                document.body.innerHTML =
                                  "<html><head><title></title>" +
                                  "<link href=\"../App_Themes/App_en/bootstrap.css\" type=\"text/css\" rel=\"stylesheet\">" +
                                  "<link href=\"../App_Themes/App_en/style.css\" type=\"text/css\" rel=\"stylesheet\">" +
                                    "</head><body>" +
                                  div1 + "</body>";

                                //Print Page
                                window.print();
                                //Restore orignal HTML
                                document.body.innerHTML = oldPage;
                                $('.co').css("display", "normal");
                                $('.noPrint').css("display", "normal");


                            }

                        </script>
                        <div id="PrintArea">
                            <div class="row" id="dashboard-header">
                                <div class="col-xs-6">
                                    <div class="ScreeningPlace">
                                        <asp:Image ImageUrl="~/img/Logo.jpg" CssClass="img-rounded" runat="server" ID="imgLogo" meta:resourcekey="imgLogoResource1" Width="50px" />
                                    </div>
                                    <div>
                                        <span class="welcome" style="font-weight: bold;">
                                            <asp:Label ID="litName" runat="server" Text="NewGiza University"></asp:Label></span>
                                        <br />
                                        <span>
                                            <asp:Label ID="litScreening" runat="server" Text="Student Contact Form"></asp:Label>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-xs-6 pull-right">
                                    <span class="pull-right">
                                        <asp:Literal ID="litParticipant" runat="server" Text=""></asp:Literal></span>
                                </div>
                            </div>
                            
                                    
                                    <div class="row">
                                        <div class="col-lg-1">Student ID</div>
                                        <div class="col-lg-3">
                                            <asp:TextBox ID="txt_stuID" runat="server"></asp:TextBox>
                                        </div>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="txt_stuID" ValidationGroup="a1"></asp:RequiredFieldValidator>
                                        <div class="col-lg-2">
                                            <asp:Button ID="btn_load" runat="server" Text="Load Student Contact" ValidationGroup="a1" OnClick="btn_load_Click" />
                                        </div>
                                        <div class="col-lg-6">
                                        </div>
                                    </div>
                                    <hr />
                                    <div class="row" id="div_noData" runat="server" visible="false">
                                        <div class="row-lg-4"></div>
                                        <div class="row-lg-4 fancybox-error" style="color:red;">No Contact Saved before.</div>
                                        <div class="row-lg-4"></div>
                                    </div>
                                    <hr />
                                    <br />
                                    <asp:Panel class="row" id="div_data" runat="server" visible="false">
                                        <div class="row">
                                        <div class="col-lg-12" style="text-align:center;">
                                            <p class="MsoNormal">
                                                <b><span>Please fill in the required data in Arabic and English accurately. Then print the complete form to be signed by the student and the legal guardian at NGU campus as per the schedule mentioned in the email.
                                                <p></p>
                                                </span></b>

                                                <p>
                                                </p>
                                                <p class="MsoNormal">
                                                    <b><span>Incomplete forms will be disregarded. Please upload the following document on the link as well:<p>
                                                    </p>
                                                    </span></b>
                                                    <p>
                                                    </p>
                                                    <p class="auto-style1" style="mso-list: l0 level1 lfo1">
                                                        <![if !supportLists]><span class="auto-style2"><span>·&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span></span><![endif]><b><span><span class="auto-style2">Copy of legal guardian national ID</span><p>
                                                        </p>
                                                        </span></b>
                                                        <p>
                                                        </p>
                                                    </p>
                                                </p>

                                            </p>
                                        </div>
                                    </div>
                                        <div class="row">
                                            <div class="col-lg-2">Studetn ID</div>
                                            <div class="col-lg-3">
                                                <asp:TextBox ID="txt_stuCode" runat="server" ReadOnly="true" BackColor="Gray"></asp:TextBox>
                                            </div>
                                            <div class="col-lg-7">
                                                <asp:HiddenField ID="hdf_stuID" runat="server" />
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-lg-2">School/Major</div>
                                            <div class="col-lg-4">
                                                <asp:DropDownList ID="ddlst_school" runat="server" DataSourceID="sqlds_schools" DataTextField="schoolName" DataValueField="id"></asp:DropDownList>
                                                <asp:SqlDataSource ID="sqlds_schools" runat="server" ConnectionString="<%$ ConnectionStrings:ContactConnectionString %>" SelectCommand="SP_GetAllSchoolsForList" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="ddlst_school" ValidationGroup="a2" InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-lg-6">
                                            </div>
                                        </div>
                                        <div class="row"style="text-align:center;">
                                            <div class="col-lg-2"></div>
                                            <div class="col-lg-4">
                                                In English/ البيان بالانجليزية
                                            </div>
                                            <div class="col-lg-4">
                                                In Arabic/ البيان بالعربية
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-2">Studetn full name/اسم الطالب بالكامل <br /> (as in birth certificate)</div> 
                                            <div class="col-lg-4">
                                                <asp:TextBox ID="txt_stuNameEn" runat="server" ToolTip="Student full name in English" style="direction:ltr;"></asp:TextBox>
                                                
                                            </div>
                                            <div class="col-lg-4" style="direction:rtl;">                                                
                                                <asp:TextBox ID="txt_stuNameAr" runat="server" ToolTip="اسم الطالب بالكامل بالعربية "></asp:TextBox>
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="txt_stuNameEn" ValidationGroup="a2"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="txt_stuNameAr" ValidationGroup="a2"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <br />
<div class="row" style="text-align:center; background-color:black; color:white; border:solid; border-color:black;">
                                            <div class="col-lg-12">Required Information / معلومات مطلوبة </div> 
                                            </div>
                                        <br />
                                        <div class="row" style="text-align:center;">
                                            <div class="col-lg-2"></div>
                                            <div class="col-lg-4">
                                                In English/ البيان بالانجليزية
                                            </div>
                                            <div class="col-lg-4">
                                                In Arabic/ البيان بالعربية
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-2">Gender/النوع</div>
                                            <div class="col-lg-4">
                                                <asp:DropDownList ID="ddlst_reqGender" runat="server" DataSourceID="sqlds_reqGenders" DataTextField="genderNameEN" DataValueField="genderID"  style="direction:ltr;"></asp:DropDownList>
                                                <asp:SqlDataSource ID="sqlds_reqGenders" runat="server" ConnectionString="<%$ ConnectionStrings:ContactConnectionString %>" SelectCommand="SP_GetAllGendersForList" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                            </div>
                                           
                                            <div class="col-lg-4" style="direction:rtl;">                                                
                                                
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator015" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="ddlst_reqGender" ValidationGroup="a2" InitialValue="0"></asp:RequiredFieldValidator>
                                                
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-2">Date of birth/تاريخ الميلاد</div>
                                            <div class="col-lg-4">
                                                <%--<asp:TextBox ID="txt_reqDOB" runat="server" ToolTip="Student information - Date of birth"></asp:TextBox>--%>
                                                <telerik:RadDatePicker ID="txt_reqDOB" runat="server" ValidationGroup="a2"  style="direction:ltr;"></telerik:RadDatePicker>
                                            </div>
                                           
                                            <div class="col-lg-4" style="direction:rtl;">                                                
                                                
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatr025" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="txt_reqDOB" ValidationGroup="a2" ></asp:RequiredFieldValidator>
                                                
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-2">Citizenship /الجنسية</div>
                                            <div class="col-lg-4">
                                                <asp:TextBox ID="txt_reqnationalty1En" runat="server" ToolTip="Student Information - nationalty"  style="direction:ltr;"></asp:TextBox>
                                                
                                            </div>
                                            <div class="col-lg-4" style="direction:rtl;">                                                
                                                <asp:TextBox ID="txt_reqnationalty1Ar" runat="server" ToolTip="بيانات الطالب - الجنسية "></asp:TextBox>
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidato07" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="txt_reqnationalty1En" ValidationGroup="a2"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidato08" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="txt_reqnationalty1Ar" ValidationGroup="a2"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-2">Citizenship2 (If available) /الجنسية2 ( إن وجد)</div>
                                            <div class="col-lg-4">
                                                <asp:TextBox ID="txt_reqnationalty12En" runat="server" ToolTip="Student Information - nationalty2"  style="direction:ltr;"></asp:TextBox>
                                                
                                            </div>
                                            <div class="col-lg-4" style="direction:rtl;">                                                
                                                <asp:TextBox ID="txt_reqnationalty12Ar" runat="server" ToolTip="بيانات الطالب - الجنسية2 "></asp:TextBox>
                                            </div>
                                            <div class="col-lg-2">
                                                
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-2">Place of birth /مكان الميلاد</div>
                                            <div class="col-lg-4">
                                                <asp:TextBox ID="txt_reqplaceOBEn" runat="server" ToolTip="Student Information - place of birth"  style="direction:ltr;"></asp:TextBox>
                                                
                                            </div>
                                            <div class="col-lg-4" style="direction:rtl;">                                                
                                                <asp:TextBox ID="txt_reqplaceOBAr" runat="server" ToolTip="بيانات الطالب - مكان الميلاد "></asp:TextBox>
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidat007" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="txt_reqplaceOBEn" ValidationGroup="a2"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidat008" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="txt_reqplaceOBAr" ValidationGroup="a2"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-2">National ID /رقم الهوية</div>
                                            <div class="col-lg-4">
                                                <asp:TextBox ID="txt_reqnationalID" runat="server" ToolTip="Student Information - national id - رقم الهوية"  style="direction:ltr;"></asp:TextBox>
                                                
                                            </div>
                                            <div class="col-lg-4" style="direction:rtl;">                                                
                                               
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidat07" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="txt_reqnationalID" ValidationGroup="a2"></asp:RequiredFieldValidator>
                                               
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-2">National Identifey Type/نوع الهوية</div>
                                            <div class="col-lg-4">
                                                <asp:DropDownList ID="ddlst_reqNatinalIDType" runat="server" DataSourceID="sqlds_reqNatonalTypeID" DataTextField="typeNameEN" DataValueField="typeID"  style="direction:ltr;"></asp:DropDownList>
                                                <asp:SqlDataSource ID="sqlds_reqNatonalTypeID" runat="server" ConnectionString="<%$ ConnectionStrings:ContactConnectionString %>" SelectCommand="SP_GetAllnationalTypesForList" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                            </div>
                                           
                                            <div class="col-lg-4" style="direction:rtl;">                                                
                                                
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidato15" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="ddlst_reqNatinalIDType" ValidationGroup="a2" InitialValue="0"></asp:RequiredFieldValidator>
                                                
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-2">Secondary School Name/اسم المدرسة الثانوي</div>
                                            <div class="col-lg-4">
                                                <asp:TextBox ID="txt_reqschoolNameEn" runat="server" ToolTip="Student Information - Secondary School Name"  style="direction:ltr;"></asp:TextBox>
                                            </div>
                                           
                                            <div class="col-lg-4" style="direction:rtl;">                                                
                                                <asp:TextBox ID="txt_reqschoolNameAr" runat="server" ToolTip="Student Information - اسم المدرسة الثانوي"></asp:TextBox>
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:RequiredFieldValidator ID="RequiredFielValid015" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="txt_reqschoolNameEn" ValidationGroup="a2" ></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFielValid016" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="txt_reqschoolNameAr" ValidationGroup="a2" ></asp:RequiredFieldValidator>
                                                
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-2">School Degree/نوع الشهادة</div>
                                            <div class="col-lg-4">
                                                <asp:TextBox ID="txt_reqcertificateNameEn" runat="server" ToolTip="Student Information - School Degree"  style="direction:ltr;"></asp:TextBox>
                                            </div>
                                           
                                            <div class="col-lg-4" style="direction:rtl;">                                                
                                                <asp:TextBox ID="txt_reqcertificateNameAr" runat="server" ToolTip="Student Information - نوع الشهادة"></asp:TextBox>
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:RequiredFieldValidator ID="RequiredFielValid0015" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="txt_reqcertificateNameEn" ValidationGroup="a2" ></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFielValid0016" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="txt_reqcertificateNameAr" ValidationGroup="a2" ></asp:RequiredFieldValidator>
                                                
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row" style="text-align:center; background-color:black; color:white; border:solid; border-color:black;">
                                            <div class="col-lg-12">Personal Contact Informatio / معلومات تواصل خاصة   </div> 
                                            </div>
                                        <div class="row" style="text-align:center; background-color:lightgray; color:black;">
                                            <div class="col-lg-12"> Residency Address during studies / محل الإقامة خلال فترة الدراسة  </div> 
                                            </div>
                                        <br />
                                        <div class="row" style="text-align:center;">
                                            <div class="col-lg-2"></div>
                                            <div class="col-lg-4">
                                                In English/ البيان بالانجليزية
                                            </div>
                                            <div class="col-lg-4">
                                                In Arabic/ البيان بالعربية
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-2">Country/البلد</div>
                                            <div class="col-lg-4">
                                                <asp:TextBox ID="txt_countryEn" runat="server" ToolTip="Student address - country"  style="direction:ltr;"></asp:TextBox>
                                                
                                            </div>
                                            <div class="col-lg-4" style="direction:rtl;">                                                
                                                <asp:TextBox ID="txt_countryAr" runat="server" ToolTip="عنوان الطالب - البلد "></asp:TextBox>
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="txt_countryEn" ValidationGroup="a2"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="txt_countryAr" ValidationGroup="a2"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-2">City/المدينة</div>
                                            <div class="col-lg-4">
                                                <asp:TextBox ID="txt_cityEn" runat="server" ToolTip="Student address - city"  style="direction:ltr;"></asp:TextBox>
                                                
                                            </div>
                                            <div class="col-lg-4" style="direction:rtl;">                                                
                                                <asp:TextBox ID="txt_cityAr" runat="server" ToolTip="عنوان الطالب - المدينة "></asp:TextBox>
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="txt_cityEn" ValidationGroup="a2"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="txt_cityAr" ValidationGroup="a2"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-2">District/الحي</div>
                                            <div class="col-lg-4">
                                                <asp:TextBox ID="txt_districtEn" runat="server" ToolTip="Student address - district"></asp:TextBox>
                                                
                                            </div>
                                            <div class="col-lg-4" style="direction:rtl;">                                                
                                                <asp:TextBox ID="txt_districtAr" runat="server" ToolTip="عنوان الطالب - الحي "></asp:TextBox>
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="txt_districtEn" ValidationGroup="a2"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="txt_districtAr" ValidationGroup="a2"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-2">Street/الشارع</div>
                                            <div class="col-lg-4">
                                                <asp:TextBox ID="txt_streetEn" runat="server" ToolTip="Student address - street"></asp:TextBox>
                                                
                                            </div>
                                            <div class="col-lg-4" style="direction:rtl;">                                                
                                                <asp:TextBox ID="txt_streetAr" runat="server" ToolTip="عنوان الطالب - الشارع "></asp:TextBox>
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="txt_streetEn" ValidationGroup="a2"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="txt_streetAr" ValidationGroup="a2"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-2">Building No./رقم العقار</div>
                                            <div class="col-lg-4">
                                                <asp:TextBox ID="txt_buildingEn" runat="server" ToolTip="Student address - Building No."></asp:TextBox>
                                                
                                            </div>
                                            <div class="col-lg-4" style="direction:rtl;">                                                
                                                <asp:TextBox ID="txt_buildingAr" runat="server" ToolTip="عنوان الطالب - رقم العقار "></asp:TextBox>
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="txt_buildingEn" ValidationGroup="a2"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="txt_buildingAr" ValidationGroup="a2"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-2">Apartment No./رقم الشقة</div>
                                            <div class="col-lg-4">
                                                <asp:TextBox ID="txt_flatEn" runat="server" ToolTip="Student address - apartment No."></asp:TextBox>
                                                
                                            </div>
                                            <div class="col-lg-4" style="direction:rtl;">                                                
                                                <asp:TextBox ID="txt_flatAr" runat="server" ToolTip="عنوان الطالب - رقم الشقة "></asp:TextBox>
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="txt_flatEn" ValidationGroup="a2"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="txt_flatAr" ValidationGroup="a2"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="row" style="text-align:center; background-color:lightgray; color:black;">
                                            <div class="col-lg-12"> Other Contacts  </div> 
                                            </div>

<div class="row">
                                            <div class="col-lg-2">Landline No.</div>
                                            <div class="col-lg-4">
                                                <asp:TextBox ID="txt_perLandLine" runat="server" ToolTip="Landline No."></asp:TextBox>
                                                
                                            </div>
                                            <div class="col-lg-4">                                                
                                                
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="txt_perLandLine" ValidationGroup="a2"></asp:RequiredFieldValidator>
                                                
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-2">Cellphone No.</div>
                                            <div class="col-lg-4">
                                                <asp:TextBox ID="txt_perCellphone" runat="server" ToolTip="Cellphone No."></asp:TextBox>
                                                
                                            </div>
                                            <div class="col-lg-4">                                                
                                                
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="txt_perCellphone" ValidationGroup="a2"></asp:RequiredFieldValidator>
                                                
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-2">Personal E-mail.</div>
                                            <div class="col-lg-4">
                                                <asp:TextBox ID="txt_permail" runat="server" ToolTip="Personal E-mail"></asp:TextBox>
                                                
                                            </div>
                                            <div class="col-lg-4">                                                
                                                
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="txt_permail" ValidationGroup="a2"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_permail" ValidationGroup="a2" ErrorMessage="*" ForeColor="#FF3300" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                        <br />
                                         <div class="row" style="text-align:center; background-color:black; color:white; border:solid; border-color:black;">
                                            <div class="col-lg-12">Legal Guardian Information / معلومات ولى أمر الطالب 
<br />
                                                (person responsible for paying university tuition fees) 
   </div> 
                                            </div>
                                        <br />
<div class="row">
                                            <div class="col-lg-2">Guardian Relation / صلة ولي الامر</div>
                                            <div class="col-lg-4">
                                                <asp:DropDownList ID="ddlst_guardRelation" runat="server" DataSourceID="sqlds_guardianrelation" DataTextField="RelationshipEn" DataValueField="id"></asp:DropDownList>
                                                
                                                <asp:SqlDataSource ID="sqlds_guardianrelation" runat="server" ConnectionString="<%$ ConnectionStrings:ContactConnectionString %>" SelectCommand="SP_GetAllRelationForList" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                                
                                            </div>
                                            <div class="col-lg-4">                                                
                                                
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="ddlst_guardRelation" ValidationGroup="a2" InitialValue="0"></asp:RequiredFieldValidator>
                                                
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row"style="text-align:center;">
                                            <div class="col-lg-2"></div>
                                            <div class="col-lg-4">
                                                In English/ البيان بالانجليزية
                                            </div>
                                            <div class="col-lg-4">
                                                In Arabic/ البيان بالعربية
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-2">Guardian full name./اسم ولي الامر بالكامل</div>
                                            <div class="col-lg-4">
                                                <asp:TextBox ID="txt_gurNameEn" runat="server" ToolTip="Guardian info - full name."></asp:TextBox>
                                                
                                            </div>
                                            <div class="col-lg-4" style="direction:rtl;">                                                
                                                <asp:TextBox ID="txt_gurNameAr" runat="server" ToolTip="بيان ولي الامر - الاسم بالكامل "></asp:TextBox>
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="txt_gurNameEn" ValidationGroup="a2"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="txt_gurNameAr" ValidationGroup="a2"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-2">Occupation / الوظيفة</div>
                                            <div class="col-lg-4">
                                                <asp:TextBox ID="txt_gurJobEn" runat="server" ToolTip="Guardian info - Occupation."></asp:TextBox>
                                                
                                            </div>
                                            <div class="col-lg-4" style="direction:rtl;">                                                
                                                <asp:TextBox ID="txt_gurJobAr" runat="server" ToolTip="بيان ولي الامر - الوظيفة "></asp:TextBox>
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="txt_gurJobEn" ValidationGroup="a2"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="txt_gurJobAr" ValidationGroup="a2"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-2">Full Contact Home Address / عنوان السكن كامل</div>
                                            <div class="col-lg-4">
                                                <asp:TextBox ID="txt_gurAddressEn" runat="server" ToolTip="Guardian info - Home Address." TextMode="MultiLine" Width="100%" Height="70px"></asp:TextBox>
                                                
                                            </div>
                                            <div class="col-lg-4" style="direction:rtl;">                                                
                                                <asp:TextBox ID="txt_gurAddressAr" runat="server" ToolTip="بيان ولي الامر - عنوان السكن " TextMode="MultiLine" Width="100%" Height="70px"></asp:TextBox>
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="txt_gurAddressEn" ValidationGroup="a2"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="txt_gurAddressAr" ValidationGroup="a2"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-2">Full Contact Work Address / عنوان العمل كامل</div>
                                            <div class="col-lg-4">
                                                <asp:TextBox ID="txt_gurWorkAddressEn" runat="server" ToolTip="Guardian info - Work Address." TextMode="MultiLine" Width="100%" Height="70px"></asp:TextBox>
                                                
                                            </div>
                                            <div class="col-lg-4" style="direction:rtl;">                                                
                                                <asp:TextBox ID="txt_gurWorkAddressAr" runat="server" ToolTip="بيان ولي الامر - عنوان العمل " TextMode="MultiLine" Width="100%" Height="70px"></asp:TextBox>
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator025" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="txt_gurWorkAddressEn" ValidationGroup="a2"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator026" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="txt_gurWorkAddressAr" ValidationGroup="a2"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-2">Landline No.</div>
                                            <div class="col-lg-4">
                                                <asp:TextBox ID="txt_gurLandLine" runat="server" ToolTip="Landline No."></asp:TextBox>
                                                
                                            </div>
                                            <div class="col-lg-4">                                                
                                                
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="txt_gurLandLine" ValidationGroup="a2"></asp:RequiredFieldValidator>
                                                
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-2">Cellphone No.</div>
                                            <div class="col-lg-4">
                                                <asp:TextBox ID="txt_gurCellphone" runat="server" ToolTip="Cellphone No."></asp:TextBox>
                                                
                                            </div>
                                            <div class="col-lg-4">                                                
                                                
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="txt_gurCellphone" ValidationGroup="a2"></asp:RequiredFieldValidator>
                                                
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-2">E-mail.</div>
                                            <div class="col-lg-4">
                                                <asp:TextBox ID="txt_gurmail" runat="server" ToolTip="E-mail"></asp:TextBox>
                                                
                                            </div>
                                            <div class="col-lg-4">                                                
                                                
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="txt_gurmail" ValidationGroup="a2"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txt_gurmail" ValidationGroup="a2" ErrorMessage="*" ForeColor="#FF3300" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                        <br />
                                         <div class="row" style="text-align:center; background-color:black; color:white; border:solid; border-color:black;">
                                            <div class="col-lg-12">Emergency Contact Informationجهة الاتصال فى حالة الطوارىء 
<br />
-	Must be other than legal guardian -

   </div> 
                                            </div>
                                        <br />
<div class="row">
                                            <div class="col-lg-2">Specify relation with contact / برجاء تحديد صلة القرابة .</div>
                                            <div class="col-lg-4">
                                                <asp:DropDownList ID="ddlst_emergincyRelation" runat="server" DataSourceID="sqlds_emergincyRelation" DataTextField="RelationshipEn" DataValueField="id"></asp:DropDownList>
                                                
                                                <asp:SqlDataSource ID="sqlds_emergincyRelation" runat="server" ConnectionString="<%$ ConnectionStrings:ContactConnectionString %>" SelectCommand="SP_GetAllRelationForList" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                                
                                            </div>
                                            <div class="col-lg-4">                                                
                                                
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="ddlst_emergincyRelation" ValidationGroup="a2" InitialValue="0"></asp:RequiredFieldValidator>
                                                
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row"style="text-align:center;">
                                            <div class="col-lg-2"></div>
                                            <div class="col-lg-4">
                                                In English/ البيان بالانجليزية
                                            </div>
                                            <div class="col-lg-4">
                                                In Arabic/ البيان بالعربية
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-2">Full name./الاسم  بالكامل</div>
                                            <div class="col-lg-4">
                                                <asp:TextBox ID="txt_emrNameEn" runat="server" ToolTip="emergency info - full name."></asp:TextBox>
                                                
                                            </div>
                                            <div class="col-lg-4" style="direction:rtl;">                                                
                                                <asp:TextBox ID="txt_emrNameAr" runat="server" ToolTip="بيان الطوارئ - الاسم بالكامل "></asp:TextBox>
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="txt_emrNameEn" ValidationGroup="a2"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="txt_emrNameEn" ValidationGroup="a2"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-2">Job./الوظيفة</div>
                                            <div class="col-lg-4">
                                                <asp:TextBox ID="txt_emrJobEn" runat="server" ToolTip="emergency info - job name."></asp:TextBox>
                                                
                                            </div>
                                            <div class="col-lg-4" style="direction:rtl;">                                                
                                                <asp:TextBox ID="txt_emrJobAr" runat="server" ToolTip="بيان الطوارئ - الوظيفة "></asp:TextBox>
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator031" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="txt_emrJobEn" ValidationGroup="a2"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator032" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="txt_emrJobAr" ValidationGroup="a2"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-2">Address / العنوان</div>
                                            <div class="col-lg-4">
                                                <asp:TextBox ID="txt_emrAddressEn" runat="server" ToolTip="emergency info - Address." TextMode="MultiLine" Width="100%" Height="70px"></asp:TextBox>
                                                
                                            </div>
                                            <div class="col-lg-4" style="direction:rtl;">                                                
                                                <asp:TextBox ID="txt_emrAddressAr" runat="server" ToolTip="بيان الطوارئ - العنوان  " TextMode="MultiLine" Width="100%" Height="70px"></asp:TextBox>
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator0025" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="txt_emrAddressEn" ValidationGroup="a2"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator0026" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="txt_emrAddressAr" ValidationGroup="a2"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-2">Landline No.</div>
                                            <div class="col-lg-4">
                                                <asp:TextBox ID="txt_emrLandline" runat="server" ToolTip="Landline No."></asp:TextBox>
                                                
                                            </div>
                                            <div class="col-lg-4">                                                
                                                
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator033" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="txt_emrLandline" ValidationGroup="a2"></asp:RequiredFieldValidator>
                                                
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-2">Cellphone No.</div>
                                            <div class="col-lg-4">
                                                <asp:TextBox ID="txt_emrCellphone" runat="server" ToolTip="Cellphone No."></asp:TextBox>
                                                
                                            </div>
                                            <div class="col-lg-4">                                                
                                                
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="txt_emrCellphone" ValidationGroup="a2"></asp:RequiredFieldValidator>
                                                
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-2">E-mail.</div>
                                            <div class="col-lg-4">
                                                <asp:TextBox ID="txt_emrmail" runat="server" ToolTip="E-mail"></asp:TextBox>
                                                
                                            </div>
                                            <div class="col-lg-4">                                                
                                                
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="txt_emrmail" ValidationGroup="a2"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txt_emrmail" ValidationGroup="a2" ErrorMessage="*" ForeColor="#FF3300" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                                            </div>
                                        </div>

                                        <br />
                                        <div class="row" style="text-align:center; background-color:black; color:white; border:solid; border-color:black;">
                                            <div class="col-lg-12">
                                                Upload a copy of legal guardian national ID 
                                                <br />
                                                تحميل نسخة من بطاقة ولي الامر
   </div> 
                                            </div>
                                       <br />

<div class="row">
                                            <div class="col-lg-2">Guardian National ID/ نسخة من بطاقة ولي الامر</div>
                                            <div class="col-lg-4">
                                                <asp:FileUpload ID="fil_gurIdPic" runat="server" />
                                            </div>
                                            <div class="col-lg-4">                                                
                                                
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:RequiredFieldValidator ID="req_pic" runat="server" ErrorMessage="*" Text="*" ForeColor="Red" ControlToValidate="fil_gurIdPic" ValidationGroup="a2"></asp:RequiredFieldValidator>
                                                
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-2"></div>
                                            <div class="col-lg-4">
                                                <asp:Image ID="img_gurID" runat="server"  style="width:100%;"/>
                                            </div>
                                            <div class="col-lg-4">                                                
                                                <asp:HiddenField ID="hdf_gurIDPic" runat="server" />
                                            </div>
                                            <div class="col-lg-2">
                                                
                                                
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-4">
                                                
                                            </div>
                                            <div class="col-lg-4">
                                                <asp:Button ID="btn_save" runat="server" Text="Save/Update Contact" ValidationGroup="a2" style="height:40px; background-color:brown; color:white;" OnClick="btn_save_Click"/>
                                            </div>
                                            <div class="col-lg-4">
                                                
                                            </div>
                                        </div>

                                    </asp:Panel>
                                
                        </div>
                    </div>
                </div>
                <!--End Content-->
            </div>
        </div>
        <!--End Container-->
        <script type="text/javascript">
            function CreateSelect() {
                $('select').select2();
                $('.dataTables_filter').each(function () {
                    $(this).find('label input[type=text]').attr('placeholder', 'Search');
                });
            }

            function pageLoad(sender, args) {
                CreateSelect();
            }
            $(document).ready(function () {
                //$('#main').persiaNumber('ar');
                $.datepicker.setDefaults($.datepicker.regional['ar']);
                $('.show-sidebar').on('click', function (e) {
                    e.preventDefault();
                    $('div#main').toggleClass('sidebar-show');
                    setTimeout(MessagesMenuWidth, 250);
                });
                var ajax_url = location.hash.replace(/^#/, '');
                if (ajax_url.length < 1) {
                    ajax_url = 'ajax/dashboard.html';
                }
                //LoadAjaxContent(ajax_url);
                $('.main-menu').on('click', 'a', function (e) {
                    var parents = $(this).parents('li');
                    var li = $(this).closest('li.dropdown');
                    var another_items = $('.main-menu li').not(parents);
                    another_items.find('a').removeClass('active');
                    another_items.find('a').removeClass('active-parent');
                    if ($(this).hasClass('dropdown-toggle') || $(this).closest('li').find('ul').length == 0) {
                        $(this).addClass('active-parent');
                        var current = $(this).next();
                        if (current.is(':visible')) {
                            li.find("ul.dropdown-menu").slideUp('fast');
                            li.find("ul.dropdown-menu a").removeClass('active')
                        }
                        else {
                            another_items.find("ul.dropdown-menu").slideUp('fast');
                            current.slideDown('fast');
                        }
                    }
                    else {
                        if (li.find('a.dropdown-toggle').hasClass('active-parent')) {
                            var pre = $(this).closest('ul.dropdown-menu');
                            pre.find("li.dropdown").not($(this).closest('li')).find('ul.dropdown-menu').slideUp('fast');
                        }
                    }
                    if ($(this).hasClass('active') == false) {
                        $(this).parents("ul.dropdown-menu").find('a').removeClass('active');
                        $(this).addClass('active')
                    }
                    if ($(this).hasClass('ajax-link')) {
                        e.preventDefault();
                        if ($(this).hasClass('add-full')) {
                            $('#content').addClass('full-content');
                        }
                        else {
                            $('#content').removeClass('full-content');
                        }
                        var url = $(this).attr('href');
                        window.location.assign(url);
                        //LoadAjaxContent(url);
                    }
                    if ($(this).attr('href') == '#') {
                        e.preventDefault();
                    }
                });

                var height = window.innerHeight - 49;
                $('#main').css('min-height', height)
                    .on('click', '.expand-link', function (e) {
                        var body = $('body');
                        e.preventDefault();
                        var box = $(this).closest('div.box');
                        var button = $(this).find('i');
                        button.toggleClass('fa-expand').toggleClass('fa-compress');
                        box.toggleClass('expanded');
                        body.toggleClass('body-expanded');
                        var timeout = 0;
                        if (body.hasClass('body-expanded')) {
                            timeout = 100;
                        }
                        setTimeout(function () {
                            box.toggleClass('expanded-padding');
                        }, timeout);
                        setTimeout(function () {
                            box.resize();
                            box.find('[id^=map-]').resize();
                        }, timeout + 50);
                    })
                    .on('click', '.collapse-link', function (e) {
                        e.preventDefault();
                        var box = $(this).closest('div.box');
                        var button = $(this).find('i');
                        var content = box.find('div.box-content');
                        content.slideToggle('fast');
                        button.toggleClass('fa-chevron-up').toggleClass('fa-chevron-down');
                        setTimeout(function () {
                            box.resize();
                            box.find('[id^=map-]').resize();
                        }, 50);
                    })
                    .on('click', '.close-link', function (e) {
                        e.preventDefault();
                        var content = $(this).closest('div.box');
                        content.remove();
                    });
                $('#locked-screen').on('click', function (e) {
                    e.preventDefault();
                    $('body').addClass('body-screensaver');
                    $('#screensaver').addClass("show");
                    ScreenSaver();
                });
                $('body').on('click', 'a.close-link', function (e) {
                    e.preventDefault();
                    CloseModalBox();
                });
                $('#top-panel').on('click', 'a', function (e) {
                    if ($(this).hasClass('ajax-link')) {
                        e.preventDefault();
                        if ($(this).hasClass('add-full')) {
                            $('#content').addClass('full-content');
                        }
                        else {
                            $('#content').removeClass('full-content');
                        }
                        var url = $(this).attr('href');
                        window.location.hash = url;
                        LoadAjaxContent(url);
                    }
                });
                $('#screen_unlock').on('mouseover', function () {
                    var header = 'Enter current username and password';
                    var form = $('<div class="form-group"><label class="control-label">Username</label><input type="text" class="form-control" name="username" /></div>' +
                                '<div class="form-group"><label class="control-label">Password</label><input type="password" class="form-control" name="password" /></div>');
                    var button = $('<div class="text-center"><a href="Default.aspx" class="btn btn-primary">Unlock</a></div>');
                    OpenModalBox(header, form, button);
                });
            });
        </script>

    </form>
</body>
</html>
