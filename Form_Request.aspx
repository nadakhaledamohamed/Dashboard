<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Theme="Default" StylesheetTheme="Default" CodeFile="Form_Request.aspx.cs" Inherits="_RequestForm" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function DisableBackButton() {
            window.history.forward()
        }
        DisableBackButton();
        window.onload = DisableBackButton;
        window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }

        window.onunload = function () { void (0) }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server" Visible="true">
    <div id="FormRequestDiv" runat="server" style="overflow-x: scroll; overflow-y: scroll;">
        <div class="row" runat="server">
            <section class="col col-lg-12">
                <label style="font-weight: bold">Available Forms</label>
                <hr />
                <asp:CheckBoxList ID="ddForms" runat="server" Width="75%" SelectionMode="Multiple" OnSelectedIndexChanged="ddForms_SelectedIndexChanged" AutoPostBack="true"></asp:CheckBoxList>
            </section>
        </div>
        <hr />
    </div>

    <div id="FormsDiv" runat="server" visible="false">
        <div id="CommaanDataDiv" runat="server" visible="false">
            <asp:HiddenField ID="HFSchool" runat="server" />
            <div class="table-responsive">
                <table style="width: 90%">
                    <tr>
                        <td colspan="3">
                            <div class="row">
                                <section class="col col-lg-12">
                                    <label id="Label9">ID Number</label>
                                    <asp:TextBox ID="txtStudentID" runat="server" CssClass="form-control" ValidationGroup="NewCompany" Width="200px" Enabled="false" Font-Bold="true" MaxLength="9"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldVaflidator4" runat="server" ControlToValidate="txtStudentID"
                        ErrorMessage="*" InitialValue=""
                        Text="*" ValidationGroup="NewCompany" SetFocusOnError="True" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                </section>
                            </div>
                        </td>
                    </tr>
                    <tr>

                        <td style="width: 40%; align-content: center">
                            <div class="row">
                                <section class="col col-lg-12">
                                    <label id="Label22">English Name as exist in passport</label>
                                    <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" ValidationGroup="NewCompany" Font-Bold="true" Width="60%" AutoPostBack="true"></asp:TextBox>
                                    <asp:CustomValidator runat="server" ID="cust_EnglishName" ControlToValidate="txtFullName" ValidationGroup="NewCompany" SetFocusOnError="True" Style="font-weight: bold; font-size: 18px" ForeColor="Red" ErrorMessage="*" />
                                </section>
                            </div>
                        </td>
                        <td style="width: 40%; align-content: center">
                            <div class="row">
                                <section class="col col-lg-12">
                                    <label id="Label212">Arabic Name as exist in National Id</label>
                                    <asp:TextBox ID="txtStudentArName" runat="server" CssClass="form-control" ValidationGroup="NewCompany" Font-Bold="true" Width="60%" AutoPostBack="true"></asp:TextBox>
                                    <asp:CustomValidator runat="server" ID="Cust_arabicName" ControlToValidate="txtStudentArName" ValidationGroup="NewCompany" SetFocusOnError="True" Style="font-weight: bold; font-size: 18px" ForeColor="Red" ErrorMessage="*" />
                                    <asp:CustomValidator runat="server" ID="Cust_NameValidation" ValidationGroup="NewCompany" SetFocusOnError="True" Style="font-weight: bold; font-size: 18px" ForeColor="Red" OnServerValidate="Cust_NameValidation_ServerValidate" ErrorMessage="*" />
                                </section>
                            </div>
                        </td>
                        <td style="width: 30%; align-content: center">
                            <div class="row">
                                <section class="col col-lg-12">
                                    <label id="Label20">School</label>
                                    <asp:TextBox ID="txtSchoolName" runat="server" CssClass="form-control" ValidationGroup="NewCompany" Font-Bold="true" Enabled="false"></asp:TextBox>

                                </section>
                            </div>
                        </td>

                    </tr>

                    <tr>
                        <td>
                            <div class="row">
                                <section class="col col-lg-12">
                                    <asp:Label ID="Label3" runat="server" Text="Date of birth"></asp:Label>
                                    <asp:TextBox ID="txtDOB" runat="server" CssClass="form-control" ValidationGroup="NewCompany" Width="50%" Enabled="False"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator44" runat="server" ControlToValidate="txtDOB"
                                        ErrorMessage="*" InitialValue=""
                                        Text="*" ValidationGroup="NewCompany" SetFocusOnError="True" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <%-- <ajaxToolkit:CalendarExtender Format="MM/dd/yyyy" ID="CalendarExtender2" PopupButtonID="imgPopup" runat="server" TargetControlID="txtDOB"></ajaxToolkit:CalendarExtender>--%>
                                </section>
                            </div>
                        </td>
                        <td>

                            <div class="row">
                                <section class="col col-lg-12">
                                    <label id="Label21">Place Of Birth</label>
                                    <asp:TextBox ID="txtPlaceOfBirth" runat="server" CssClass="form-control" ValidationGroup="NewCompany" Font-Bold="true" Width="60%" MaxLength="250"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPlaceOfBirth"
                                        ErrorMessage="*" InitialValue=""
                                        Text="*" ValidationGroup="NewCompany" SetFocusOnError="True" ForeColor="Red"></asp:RequiredFieldValidator>
                                </section>
                            </div>

                        </td>
                        <td>
                            <div class="row">
                                <section class="col col-lg-12">
                                    <asp:Label ID="Label14" runat="server" Text="Mobile No"></asp:Label>
                                    <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" ValidationGroup="NewCompany" Width="50%" MaxLength="13"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtMobile"
                                        ErrorMessage="*" InitialValue=""
                                        Text="*" ValidationGroup="NewCompany" SetFocusOnError="True" Style="font-weight: bold; font-size: 18px" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                        ControlToValidate="txtMobile" ErrorMessage="Invalid Mobile No. Format"
                                        ValidationExpression="[1-9]\d*|0\d+" Style="font-weight: bold; font-size: 18px" ForeColor="Red"></asp:RegularExpressionValidator>
                                </section>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="row">
                                <section class="col col-lg-12">
                                    <asp:Label ID="Label4" runat="server" Text="Year"></asp:Label>
                                    <asp:TextBox ID="txtYear" runat="server" CssClass="form-control" ValidationGroup="NewCompany" Width="50%" Enabled="False"></asp:TextBox>

                                </section>
                            </div>
                        </td>
                        <td colspan="2">
                            <div class="row">
                                <section class="col col-lg-12">
                                    <asp:Label ID="Label5" runat="server" Text="Email"></asp:Label>
                                    <asp:TextBox ID="txtMail" runat="server" CssClass="form-control" ValidationGroup="NewCompany" Width="70%" MaxLength="250"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtMail"
                                        ErrorMessage="*" InitialValue=""
                                        Text="*" ValidationGroup="NewCompany" SetFocusOnError="True" Style="font-weight: bold; font-size: 18px" ForeColor="Red"></asp:RequiredFieldValidator>
                                     <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtMail" ErrorMessage="Invalid Email Format" style="font-weight:bold;font-size:18px" ForeColor="Red"></asp:RegularExpressionValidator>
                                </section>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>

        <div id="OFF_Transcriptdiv" runat="server" visible="false" data-form="3">
            <div style="border: dotted; padding: 30px">
                <label style="text-align: center; width: 100%; font-weight: bold; font-size: x-large">Official Transcript Of Records Issuing Request</label>
                <div class="row" runat="server">
                    <section class="col col-lg-12">

                        <label style="font-weight: bold;">Number OF copies : (Max. 5 copies) </label>
                        <asp:TextBox ID="OFFTranscript_NoOfCopies" runat="server" CssClass="form-control" Width="10%"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="OFFTranscript_NoOfCopies"
                            ErrorMessage="*" InitialValue=""
                            Text="*" ValidationGroup="NewCompany" SetFocusOnError="True" Style="font-weight: bold; font-size: 30px" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="OFFTranscript_NoOfCopies"
                            ErrorMessage="Enter Number less than 5" ForeColor="Red" MaximumValue="5" MinimumValue="1"
                            SetFocusOnError="True" Type=" Integer" ValidationGroup="NewCompany"></asp:RangeValidator>


                    </section>
                </div>
                <div class="row" runat="server">
                    <section class="col col-lg-12">

                        <label style="font-weight: bold;">Purpose Of Issuance (wht are you applying for this request): </label>
                        <asp:TextBox ID="OFFTranscript_txtPurpose" runat="server" CssClass="form-control" Width="100%" MaxLength="250"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ControlToValidate="OFFTranscript_txtPurpose"
                            ErrorMessage="*" InitialValue=""
                            Text="*" ValidationGroup="NewCompany" SetFocusOnError="True" Style="font-weight: bold; font-size: 18px" ForeColor="Red"></asp:RequiredFieldValidator>
                    </section>
                </div>
                <div class="row" runat="server">
                    <section class="col col-lg-12">

                        <label style="font-weight: bold;">Extra Remarks: </label>
                        <asp:TextBox ID="OFFTranscript_txtRemarks" runat="server" CssClass="form-control" Width="80%"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="OFFTranscript_txtRemarks"
                            ErrorMessage="*" InitialValue=""
                            Text="*" ValidationGroup="NewCompany" SetFocusOnError="True" Style="font-weight: bold; font-size: 18px" ForeColor="Red"></asp:RequiredFieldValidator>
                    </section>
                </div>
                <br />

            </div>
            <br />
        </div>
        <div id="UnOFF_Transcriptdiv" runat="server" visible="false" data-form="3">
            <div style="border: dotted; padding: 30px">
                <label style="text-align: center; width: 100%; font-weight: bold; font-size: x-large">UnOfficial Transcript Of Records Request</label>
                <div class="row" runat="server">
                    <section class="col col-lg-12">

                        <label style="font-weight: bold;">Number OF copies : (Max. 5 copies) </label>
                        <asp:TextBox ID="UnOFFTranscript_NoOfCopies" runat="server" CssClass="form-control" Width="10%"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="UnOFFTranscript_NoOfCopies"
                            ErrorMessage="*" InitialValue=""
                            Text="*" ValidationGroup="NewCompany" SetFocusOnError="True" Style="font-weight: bold; font-size:30" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="UnOFFTranscript_NoOfCopies"
                            ErrorMessage="Enter Number less than 5" ForeColor="Red" MaximumValue="5" MinimumValue="1"
                            SetFocusOnError="True" Type=" Integer" ValidationGroup="NewCompany"></asp:RangeValidator>


                    </section>
                </div>
                <div class="row" runat="server">
                    <section class="col col-lg-12">

                        <label style="font-weight: bold;">Purpose Of Issuance (wht are you applying for this request): </label>
                        <asp:TextBox ID="UnOFFTranscript_txtPurpose" runat="server" CssClass="form-control" Width="100%" MaxLength="250"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="UnOFFTranscript_txtPurpose"
                            ErrorMessage="*" InitialValue=""
                            Text="*" ValidationGroup="NewCompany" SetFocusOnError="True" Style="font-weight: bold; font-size: 18px" ForeColor="Red"></asp:RequiredFieldValidator>
                    </section>
                </div>
                <div class="row" runat="server">
                    <section class="col col-lg-12">

                        <label style="font-weight: bold;">Extra Remarks: </label>
                        <asp:TextBox ID="UnOFFTranscript_txtRemarks" runat="server" CssClass="form-control" Width="80%"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="UnOFFTranscript_txtRemarks"
                            ErrorMessage="*" InitialValue=""
                            Text="*" ValidationGroup="NewCompany" SetFocusOnError="True" Style="font-weight: bold; font-size: 18px" ForeColor="Red"></asp:RequiredFieldValidator>
                    </section>
                </div>
                <br />

            </div>

        </div>
        <br />
        <div id="Militartdiv" runat="server" visible="false" data-form="3">
            <div style="border: dotted; padding: 30px">
                <label style="text-align: center; width: 100%; font-weight: bold; font-size: x-large">التسجيل بتدريب التربية العسكرية</label>
                <%-- <asp:HiddenField ID="Military_Form_HFformId" runat="server" />
                <asp:HiddenField ID="Military_Form_HFTypeId" runat="server" />
                <asp:HiddenField ID="Military_Form_HFCost" runat="server" />
                <asp:HiddenField ID="Military_Form_HFFormTypeId" runat="server" />
                  <asp:HiddenField ID="Military_Form_HFTypeName" runat="server" />
                  <asp:HiddenField ID="Military_Form_FormTitle" runat="server" />--%>
                <div class="row" runat="server">
                    <section class="col col-lg-12" dir="rtl">
                        <label style="font-weight: bold">أتقدم بطلب للتسجيل في تدريب التربية العسكرية والذي يعد شرط اساسي من شروط التخرج من الجامعة</label>
                        <hr />
                        <label style="font-weight: bold">
                            ·

برجاء اختيار الإمتحان المطلوب التسجيل به:</label>


                        <br />



                        <table dir="rtl" style="width: 90%" border="1">
                            <tr>
                                <td style="font-weight: bold; text-align: center">الخدمة</td>
                                <td style="font-weight: bold; text-align: center">قيمة التسجيل</td>
                            </tr>
                            <tr>
                                <td style="text-align: center">التسجيل بتدريب التربية العسكرية</td>
                                <td style="text-align: center">750

جم</td>
                            </tr>
                        </table>
                        <br />
                        <label style="font-weight: bold;">ملاحظات أخرى: </label>
                        <asp:TextBox ID="MilitaryForm_txtRemarks" runat="server" CssClass="form-control" Width="80%"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="MilitaryForm_txtRemarks"
                                        ErrorMessage="*" InitialValue=""
                                        Text="*" ValidationGroup="NewCompany" SetFocusOnError="True" ForeColor="Red"></asp:RequiredFieldValidator>

                    </section>
                </div>
                <br />
                <br />
            </div>

        </div>
        <br />
        <div id="ExcussesDiv" runat="server" visible="false" data-form="4">
            <div style="border: dotted; padding: 30px">
                <label style="text-align: center; width: 100%; font-weight: bold; font-size: x-large">Absence Excuse Request</label>
                <div class="row" runat="server">
                    <section class="col col-lg-12">

                        <asp:RadioButtonList ID="Excuse_MedicalList" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="Excuse_MedicalList_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Value="1" Selected="True">Medical</asp:ListItem>
                            <asp:ListItem Value="0">Non-Medical</asp:ListItem>

                        </asp:RadioButtonList>
                    </section>
                </div>
                <div class="row" runat="server">
                    <section class="col col-lg-12">

                        <label style="font-weight: bold;">In Case Of Non- Medical Please Specify </label>
                        <asp:TextBox ID="Excuse_ReasonDescribtion" runat="server" CssClass="form-control" Width="100%" MaxLength="250"></asp:TextBox>
                        <asp:CustomValidator runat="server" ID="Other_CustomValidator" ControlToValidate="Excuse_MedicalList" ValidationGroup="NewCompany" SetFocusOnError="True" Style="font-weight: bold; font-size: 18px" ForeColor="Red" OnServerValidate="NonMedical_ServerValidate" ErrorMessage="*" />
                    </section>
                </div>
                <div class="row" runat="server">
                    <section class="col col-lg-12">


                        <table>
                            <tr>
                                <td>Period Of Absence : </td>
                                <td style="width: 30%">

                                    <asp:Label ID="Label1" runat="server" Text="From : "></asp:Label>
                                    <asp:TextBox ID="Excuse_txtFrom" runat="server" CssClass="form-control" ValidationGroup="NewCompany" Width="80%"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="Excuse_txtFrom"
                                        ErrorMessage="*" InitialValue=""
                                        Text="*" ValidationGroup="NewCompany" SetFocusOnError="True" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:CalendarExtender Format="MM/dd/yyyy" ID="CalendarExtender1" PopupButtonID="imgPopup" runat="server" TargetControlID="Excuse_txtFrom"></ajaxToolkit:CalendarExtender>


                                </td>
                                <td style="width: 30%">
                                    <asp:Label ID="Label2" runat="server" Text="To : "></asp:Label>
                                    <asp:TextBox ID="Excuse_txtTo" runat="server" CssClass="form-control" ValidationGroup="NewCompany" Width="80%"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="Excuse_txtTo"
                                        ErrorMessage="*" InitialValue=""
                                        Text="*" ValidationGroup="NewCompany" SetFocusOnError="True" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:CalendarExtender Format="MM/dd/yyyy" ID="CalendarExtender3" PopupButtonID="imgPopup" runat="server" TargetControlID="Excuse_txtTo"></ajaxToolkit:CalendarExtender>
                                </td>
                            </tr>

                        </table>
                    </section>
                </div>
                <div class="row" runat="server" dir="rtl">
                    <section class="col col-lg-12">

                        <label style="font-weight: bold;">ملاحظات أخرى: </label>
                        <asp:TextBox ID="Excuse_txtRemarks" runat="server" CssClass="form-control" Width="80%"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="Excuse_txtRemarks"
                                        ErrorMessage="*" InitialValue=""
                                        Text="*" ValidationGroup="NewCompany" SetFocusOnError="True" ForeColor="Red"></asp:RequiredFieldValidator>
                    </section>
                </div>
                <br />
                <div class="row" runat="server">
                    <section class="col col-lg-12">

                        <label style="font-weight: bold;">Please attach excuse supporting documenst to the request form </label>
                        <br />
                        <div style="float: left">
                            <asp:FileUpload ID="Excuse_FileUploadControl" runat="server" AllowMultiple="true" ValidationGroup="NewCompany2" onchange="IsFileSelected()" CssClass="FileTest" />
                            <asp:RegularExpressionValidator ID="RegExValFileUploadFileType" runat="server"
                                ControlToValidate="Excuse_FileUploadControl" ForeColor="Red"
                                ErrorMessage="Only .jpg,.png,.jpeg,.gif , .pdf Files are allowed" Font-Bold="True" ValidationGroup="NewCompany2"
                                Font-Size="Medium"
                                ValidationExpression="(.*?)\.(pdf|jpg|jpeg|png|gif|JPG|JPEG|PNG|GIF)$"></asp:RegularExpressionValidator>
                            <asp:CustomValidator ID="FileUploadCustomValidator" runat="server" ControlToValidate="Excuse_FileUploadControl"
					OnServerValidate="FileUploadCustomValidator_ServerValidate" ErrorMessage="*File must be less then 20mb" ForeColor="Red" Display="Dynamic" ValidationGroup="NewCompany2"></asp:CustomValidator>


                        </div>
                        <div style="float: left">
                            <asp:Button CssClass="btn btn-primary removeFileClass" runat="server" ID="btnClearUpload" Text="Remove Attachments" OnClick="btnClearUpload_Click" Enabled="false" />
                            <asp:Button CssClass="btn btn-primary" runat="server" ID="UploadButton" Text="Upload" OnClick="UploadButton_Click" ValidationGroup="NewCompany2" CausesValidation="true" />
                        </div>

                        <br />
                        <br />
                        <asp:Label runat="server" ID="StatusLabel" Font-Bold="True" ForeColor="Green" Text=" Select Required files and click upload (Max.Size: 2MB )" />
                    </section>
                </div>
                <br />
                <label style="font-size: 18px; color: green; font-weight: bold">This Form Must be submitted <u>In Person </u></label>
                <br />

            </div>

        </div>

        <br />
        <div id="EnglishEnrollmentDiv" runat="server" visible="false" data-form="5">
            <div style="border: dotted; padding: 30px">
                <label style="text-align: center; width: 100%; font-weight: bold; font-size: x-large">English Enrollment Certificate Issuing Request</label>
                <div class="row" runat="server">
                    <section class="col col-lg-12">

                        <label style="font-weight: bold;">Number OF copies : </label>
                        <asp:TextBox ID="EnglishEnrollment_txtNoOfCopies" runat="server" CssClass="form-control" Width="10%" MaxLength="2"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="EnglishEnrollment_txtNoOfCopies"
                            ErrorMessage="*" InitialValue=""
                            Text="*" ValidationGroup="NewCompany" SetFocusOnError="True" Style="font-weight: bold; font-size: 18px" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="EnglishEnrollment_txtNoOfCopies"
                            ErrorMessage="Enter Number " ForeColor="Red" MaximumValue="99" MinimumValue="1"
                            SetFocusOnError="True" Type=" Integer" ValidationGroup="NewCompany"></asp:RangeValidator>


                    </section>
                </div>
                <label style="text-align: left; width: 100%;"><b>Purpose Of Issuance : </b>(Why are you applying for this request ? please Choose one of the bellow purposses)</label>
                <asp:Repeater ID="EnglishEnrollmentRepeater" runat="server">
                    <HeaderTemplate>
                        <table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkSourceType" Text='<%# Eval("title") %>' runat="server" AutoPostBack="true" OnCheckedChanged="chkSourceType_CheckedChanged" />
                                <asp:HiddenField ID="SourceTypeId" Value='<%# Eval("id") %>' runat="server" />
                                <asp:TextBox ID="txtDescribtion" runat="server" Width="100%" AutoPostBack="true" MaxLength="250" ValidationGroup="NewCompany2" />
                                <asp:CustomValidator runat="server" ID="Desc_CustomValidator" ValidationGroup="NewCompany2" SetFocusOnError="True" Style="font-weight: bold; font-size: 18px" ForeColor="Red" OnServerValidate="Fileupload_CustomValidator_ServerValidate" ErrorMessage="*" />
                            </td>
                        </tr>

                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                         
                    </FooterTemplate>
                </asp:Repeater>
                <asp:CustomValidator runat="server" ID="PresentedTo_CustomValidator" ValidationGroup="NewCompany" SetFocusOnError="True" Style="font-weight: bold; font-size: 18px" ForeColor="Red" OnServerValidate="PresentedTo_CustomValidator_ServerValidate" ErrorMessage="You should select Purpose of issuance" />


                <div class="row" runat="server">
                    <section class="col col-lg-12">

                        <label style="font-weight: bold;">Extra Remarks: </label>
                        <asp:TextBox ID="EnglishEnrollment_txtRemarks" runat="server" CssClass="form-control" Width="80%" MaxLength="250"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="EnglishEnrollment_txtRemarks"
                            ErrorMessage="*" InitialValue=""
                            Text="*" ValidationGroup="NewCompany" SetFocusOnError="True" Style="font-weight: bold; font-size: 18px" ForeColor="Red"></asp:RequiredFieldValidator>
                    </section>
                </div>
                <br />

            </div>
            <br />
        </div>

        <div id="ArabicEnrollmentDiv" runat="server" visible="false" data-form="6">
            <div style="border: dotted; padding: 30px" dir="rtl">
                <label style="text-align: center; width: 100%; font-weight: bold; font-size: x-large">طلب شهادة قيد باللغة العربية</label>
                <div class="row" runat="server">
                    <section class="col col-lg-12">
                        <label style="font-weight: bold;">عدد النسخ المطلوبة : </label>
                        <asp:TextBox ID="ArabicEnrollment_txtNoOFCopies" runat="server" CssClass="form-control" Width="10%" MaxLength="2"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ArabicEnrollment_txtNoOFCopies"
                            ErrorMessage="*" InitialValue=""
                            Text="*" ValidationGroup="NewCompany" SetFocusOnError="True" Style="font-weight: bold; font-size: 18px" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="ArabicEnrollment_txtNoOFCopies"
                            ErrorMessage="Enter Number " ForeColor="Red" MaximumValue="99" MinimumValue="1"
                            SetFocusOnError="True" Type=" Integer" ValidationGroup="NewCompany"></asp:RangeValidator>


                    </section>
                </div>

                <label style="text-align: right; width: 100%;">
                    <b>الجهة (الجهات) المقدمة اليها الشهادة  : </b>(
برجاء اختيار من القائمة ادناه)</label>
                <asp:Repeater ID="ArabicEnrollment_Repeater" runat="server">
                    <HeaderTemplate>
                        <table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:CheckBox ID="ArchkSourceType" Text='<%# Eval("title") %>' runat="server" AutoPostBack="true" OnCheckedChanged="ArchkSourceType_CheckedChanged" />
                                <asp:HiddenField ID="ArSourceTypeId" Value='<%# Eval("id") %>' runat="server" />
                                <asp:TextBox ID="ArtxtDescribtion" runat="server" Width="100%" AutoPostBack="true" MaxLength="250" />
                                <asp:CustomValidator runat="server" ID="ArDesc_CustomValidator" ValidationGroup="NewCompany" SetFocusOnError="True" Style="font-weight: bold; font-size: 18px" ForeColor="Red" OnServerValidate="Arabic_CustomValidator_ServerValidate" ErrorMessage="*" />
                            </td>
                        </tr>


                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                         
                    </FooterTemplate>
                </asp:Repeater>
                <asp:CustomValidator runat="server" ID="CustomValidator1" ValidationGroup="NewCompany" SetFocusOnError="True" Style="font-weight: bold; font-size: 18px" ForeColor="Red" OnServerValidate="ArabicPresentedTo_CustomValidator_ServerValidate" ErrorMessage="You should select Purpose of issuance" />


                <div class="row" runat="server" dir="rtl">
                    <section class="col col-lg-12">

                        <label style="font-weight: bold;">ملاحظات أخرى: </label>
                        <asp:TextBox ID="ArabicEnrollmen_txtRemarks" runat="server" CssClass="form-control" Width="80%" MaxLength="250"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="ArabicEnrollmen_txtRemarks"
                            ErrorMessage="*" InitialValue=""
                            Text="*" ValidationGroup="NewCompany" SetFocusOnError="True" Style="font-weight: bold; font-size: 18px" ForeColor="Red"></asp:RequiredFieldValidator>
                    </section>
                </div>
                <br />

            </div>
            <br />
        </div>

        <div id="AssesmentPetiton_Div" runat="server" visible="false" data-form="7">
            <div style="border: dotted; padding: 30px">
                <label style="text-align: center; width: 100%; font-weight: bold; font-size: x-large">End of year assessment - petition request </label>

                <div class="row" runat="server">
                    <section class="col col-lg-12">
                        <label style="font-weight: bold;">Petition details : </label>
                        <asp:TextBox ID="AssesmentPetiton_txtReason" runat="server" CssClass="form-control" Width="80%" TextMode="MultiLine" MaxLength="500"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator100" runat="server" ControlToValidate="AssesmentPetiton_txtReason"
                            ErrorMessage="*" InitialValue=""
                            Text="*" ValidationGroup="NewCompany" SetFocusOnError="True" Style="font-weight: bold; font-size: 18px" ForeColor="Red"></asp:RequiredFieldValidator>
                    </section>
                </div>
                <br />
            </div>
            <br />
        </div>

        <div id="RegisterationBreak_Div" runat="server" visible="false" data-form="8">
            <div style="border: dotted; padding: 30px">
                <label style="text-align: center; width: 100%; font-weight: bold; font-size: x-large">Registeration Break Request </label>
                <div class="row" runat="server">
                    <section class="col col-lg-12">
                        <label style="font-weight: bold; width: 20%">Semester to Drop      : </label>
                        <asp:DropDownList ID="RegisterationBreak_startSemester" runat="server" AutoPostBack="true" Width="60%" OnSelectedIndexChanged="RegisterationBreak_startSemester_SelectedIndexChanged"></asp:DropDownList>
                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="*" Text="*" ControlToValidate="RegisterationBreak_startSemester" ValidationGroup="b1" InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ControlToValidate="RegisterationBreak_startSemester" ID="CompareValidator2" ForeColor="Red"
                        ValidationGroup="b1" ErrorMessage="Please select Semester"
                        runat="server" Display="Dynamic"
                        Operator="NotEqual" ValueToCompare="0" Type="Integer" />--%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="*" Text="*" ValidationGroup="NewCompany" Style="font-weight: bold; font-size: 18px" ControlToValidate="RegisterationBreak_startSemester" InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
                    </section>
                </div>
                <br />
                <div class="row" runat="server">
                    <section class="col col-lg-12">
                        <label style="font-weight: bold; width: 20%">Semester to Resume: </label>
                        <asp:DropDownList ID="RegisterationBreak_EndSemester" runat="server" Width="60%"></asp:DropDownList>
                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="*" Text="*" ControlToValidate="RegisterationBreak_EndSemester" ValidationGroup="b1" InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ControlToValidate="RegisterationBreak_EndSemester" ID="CompareValidator1" ForeColor="Red"
                        ValidationGroup="b1" ErrorMessage="Please select Semester"
                        runat="server" Display="Dynamic"
                        Operator="NotEqual" ValueToCompare="0" Type="Integer" />--%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="*" Text="*" Style="font-weight: bold; font-size: 18px" ValidationGroup="NewCompany" ControlToValidate="RegisterationBreak_EndSemester" InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
                    </section>
                </div>
                <br />
                <div class="row" runat="server">
                    <section class="col col-lg-12">
                        <label style="font-weight: bold;">Reason of registrartion break :(please mention briefly the nature of circumstances) </label>
                        <asp:TextBox ID="RegisterationBreak_txtReason" runat="server" CssClass="form-control" Width="80%" TextMode="MultiLine" MaxLength="500"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="RegisterationBreak_txtReason"
                            ErrorMessage="*" InitialValue=""
                            Text="*" ValidationGroup="NewCompany" SetFocusOnError="True" Style="font-weight: bold; font-size: 18px" ForeColor="Red"></asp:RequiredFieldValidator>
                    </section>
                </div>
                <br />
                <label style="font-size: 18px; color: green; font-weight: bold">This Form Must be submitted <u>In Person </u></label>
                <br />
            </div>
            <br />
        </div>

        <div id="Withdrawal_Div" runat="server" visible="false" data-form="9">
            <div style="border: dotted; padding: 30px">
                <label style="text-align: center; width: 100%; font-weight: bold; font-size: x-large">Withdrawal Request </label>
                <div class="row" runat="server">
                    <section class="col col-lg-12">
                        <label style="font-weight: bold; width: 20%">Student's Legal Gurdian : </label>
                        <asp:TextBox ID="Withdrawal_txtGurdian" runat="server" CssClass="form-control" Width="80%" MaxLength="250"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="*" Text="*" ValidationGroup="NewCompany" Style="font-weight: bold; font-size: 18px" ControlToValidate="Withdrawal_txtGurdian" InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
                    </section>
                </div>
                <br />
                <div class="row" runat="server">
                    <section class="col col-lg-12">
                        <label style="font-weight: bold; width: 20%">Reason of Withdrawal :(Please state why you are leaving ngu) </label>
                        <asp:TextBox ID="withdrawal_txtReason" runat="server" CssClass="form-control" Width="80%" MaxLength="250" TextMode="MultiLine"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="*" Text="*" Style="font-weight: bold; font-size: 18px" ValidationGroup="NewCompany" ControlToValidate="withdrawal_txtReason" InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
                    </section>
                </div>
                <br />
                <div class="row" runat="server">
                    <section class="col col-lg-12">
                        <label style="font-weight: bold;">WithdrawalStarting</label>
                        <asp:RadioButtonList ID="Withdrwal_StartingList" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1" Selected="True">Fall 2021 </asp:ListItem>
                            <asp:ListItem Value="0">Spring 2021 </asp:ListItem>

                        </asp:RadioButtonList>
                    </section>
                </div>
                <br />
                <label style="font-size: 18px; color: green; font-weight: bold">This Form Must be submitted <u>In Person </u></label>
                <br />
            </div>

        </div>
        <div id="supplementarycourses_Div" runat="server" visible="false" data-form="11">
            <div style="border: dotted; padding: 30px">
                <label style="text-align: center; width: 100%; font-weight: bold; font-size: x-large">
                    (الثانوية العامة)  تسجيل بالإمتحانات التكميلية 
وزارة التعليم العالي -المجلس الأعلى للجامعات</label>

                <div class="row" runat="server">
                    <section class="col col-lg-12" dir="rtl">
                        <label style="font-weight: bold">
                            أتقدم بطلب للتسجيل بالإمتحانات التكميلية ( اللغة العربية والتربية الدينية) من خلال جامعة الجيزة الجديدة و المقررة

من قبل المجلس بالمجلس الأعلى للجامعات - وزارة التعليم العالي كشرط اساسي من شروط التخرج من الجامعة</label>
                        <hr />

                        <div class="row" runat="server">
                            <section class="col col-lg-12">
                                <label style="font-weight: bold">برجاء اختيار الإمتحان المطلوب التسجيل به</label>
                                <hr />
                                <asp:CheckBoxList ID="supplementarycourses_list" runat="server" Width="75%" SelectionMode="Multiple"></asp:CheckBoxList>
                                <asp:CustomValidator runat="server" ID="cvmodulelist" ClientValidationFunction="ValidateModuleList" ValidationGroup="NewCompany" ForeColor="Red"
                                    ErrorMessage="Please Select Atleast one Course"></asp:CustomValidator>
                            </section>
                        </div>

                        <div class="row" runat="server">
                            <section class="col col-lg-12">
                                <label style="font-weight: bold;">ملاحظات أخرى: </label>
                                <asp:TextBox ID="supplementarycourses_txtremark" runat="server" CssClass="form-control" Width="80%"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="supplementarycourses_txtremark"
                            ErrorMessage="*" InitialValue=""
                            Text="*" ValidationGroup="NewCompany" SetFocusOnError="True" Style="font-weight: bold; font-size: 18px" ForeColor="Red"></asp:RequiredFieldValidator>
                            </section>
                        </div>

                    </section>
                </div>
                <br />
                <br />
            </div>

        </div>
        <div id="InvalidFormsDiv" runat="server" visible="false">
            <label id="ErrorLbl" runat="server"></label>
        </div>
    </div>
    <div id="ReviseDiv" runat="server" visible="false">
        <div class="box-content">
            <asp:GridView runat="server" ID="gvForms" AutoGenerateColumns="false"
                CssClass="table table-bordered table-striped table-hover table-heading table-datatable" Width="100%"
                OnRowCommand="gvTypes_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:LinkButton CommandName="del" CommandArgument='<%# Eval("FormTypeId") %>' runat="server" ID="lkbDelete" OnClientClick="return confirm ('You are going to delete this item..');"> <i class="fa fa-trash-o"></i>
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="75px" />
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Form" DataField="FormTitle" />
                    <asp:BoundField HeaderText="Fees / L.E" DataField="Cost" />
                    <asp:BoundField HeaderText="No. Of Copies" DataField="NoOFCopies" />
                    <asp:BoundField HeaderText="Total Fees" DataField="TotalCost" />

                </Columns>
            </asp:GridView>
        </div>
    </div>
    <footer class="modal-footer" id="divfooter" runat="server">
        <asp:Button runat="server" CssClass="btn btn-primary" Style="margin-left: 5px;" ValidationGroup="NewCompany" ID="NextBtn" Text="Next" OnClick="nextBtn_Click" UseSubmitBehavior="false" Visible="false"></asp:Button>
        <asp:Button runat="server" CssClass="btn btn-primary" Style="margin-left: 5px;" ValidationGroup="NewCompany" ID="PrevBtn" Text="Previous" OnClick="PrevBtn_Click" CausesValidation="false" Visible="false"></asp:Button>
        <asp:Button runat="server" CssClass="btn btn-primary" Style="margin-left: 5px; height: 24px;" ID="cancelType" Text="Cancel" OnClick="cancelType_Click" CausesValidation="false"></asp:Button>

    </footer>
<%--    <div id="divSucess" runat="server" style="color:#2980B9" visible="false">

      <b>Your request has been added succesfully , it will be revised by registrar </b>

    </div>--%>
    <script>


        // A $( document ).ready() block.
        $(document).ready(function () {
            console.log("ready!");
            debugger;
            var xx = $('.FileTest')[0]
            //  xx.addEventListener("change", IsFileSelected);
        });
        function IsFileSelected() {
            debugger;
            var element = $('.removeFileClass')[0];
            var filescntrl = $(".FileTest")[0]
            if (filescntrl.files.length > 0) {
                element.disabled = false;

            }
            else {
                element.disabled = true;
            }

        }
        function ValidateModuleList(source, args) {

            debugger;
            var chkListModules = document.getElementById('<%= supplementarycourses_list.ClientID %>');
            var chkListinputs = chkListModules.getElementsByTagName("input");
            for (var i = 0; i < chkListinputs.length; i++) {
                if (chkListinputs[i].checked) {
                    args.IsValid = true;
                    return;
                }
            }
            args.IsValid = false;
        }
    </script>
</asp:Content>

