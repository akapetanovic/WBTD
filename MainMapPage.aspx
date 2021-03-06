<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MainMapPage.aspx.cs" Inherits="MapWithAutoMovingPushpins" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register Src="~/GoogleMapForASPNet.ascx" TagName="GoogleMapForASPNet" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <style type="text/css">
        .style2
        {
            width: 518px;
            height: 80px;
        }
        .style3
        {
            width: 900px;
            height: 80px;
        }
        .style4
        {
            height: 85px;
            width: 424px;
        }
        .style9
        {
            width: 424px;
        }
        .style12
        {
            width: 319px;
        }
        .style13
        {
            height: 85px;
            width: 319px;
        }
        .style14
        {
            width: 199px;
        }
        .style15
        {
            height: 85px;
            width: 199px;
        }
        .style16
        {
            font-size: small;
        }
        .style17
        {
            font-size: medium;
        }
    </style>
</head>
<body>
     <form id="form1" runat="server">
    <table>
        <tr>
            <td class="style3">
              
            <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

    <h3 class="style16"><a href="Default.aspx">Back</a></h3>
    <div>
        <asp:Panel ID="Panel1" runat="server">
        </asp:Panel>
        <uc1:GoogleMapForASPNet ID="GoogleMapForASPNet1" runat="server" />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Timer ID="Timer1" runat="server" Interval="4000" OnTick="Timer1_Tick">
            </asp:Timer>
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
   
            </td>
            <td class="style2">
                <table style="width: 300px; height: 219px; margin-left: 10px;" align="left" 
                    bgcolor="Gray" border="aaa" frame="border">
                    <tr>
                        <td class="style14">
                           
                            <asp:CheckBox ID="CheckBoxCustomMapEnabled" runat="server" 
                                oncheckedchanged="CheckBox1_CheckedChanged" Text="Custom Map" 
                                Font-Size="Small" />
                        </td>
                        <td class="style12">
                            &nbsp;</td>
                        <td class="style9">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style14">
                            <asp:CheckBox ID="CheckBox1" runat="server" ForeColor="Yellow" 
                                oncheckedchanged="CheckBox1_CheckedChanged1" Text="P Engine 1" />
                            <asp:CheckBox ID="CheckBox2" runat="server" ForeColor="Blue" 
                                oncheckedchanged="CheckBox2_CheckedChanged" Text="P Engine 2" />
                            <asp:CheckBox ID="CheckBox3" runat="server" ForeColor="Fuchsia" 
                                oncheckedchanged="CheckBox3_CheckedChanged" Text="P Engine 3" />
                        </td>
                        <td class="style12">
                            &nbsp;</td>
                        <td class="style9">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style15">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:Label ID="Label1" runat="server" Text="Update Rate (ms)" Font-Size="9pt"></asp:Label>
                                    <asp:Label ID="lblUpdateRateReadout" runat="server" Font-Size="9pt" Text="N/A"></asp:Label><br />
                                    <asp:TextBox ID="TextBoxUpdateRate" runat="server" Height="16px" Width="64px"></asp:TextBox>
                                     <asp:Button ID="BtnUpdateRate" runat="server" Height="26px" Text="Update" 
                                width="69px" onclick="BtnUpdateRate_Click" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                           
                        </td>
                        <td class="style13">
                            &nbsp;</td>
                        <td class="style4">
                            &nbsp;</td>
                    </tr>

                </table>
     
        </tr>
        </table>
         <asp:UpdatePanel ID="UpdatePanel2" runat="server">
             <ContentTemplate>
                 <h3 style="color:Red;">
                     <span class="style17">Zoom Level: </span>
                     <asp:Label ID="lblZoomLevel" runat="server" Width="200px" Font-Size="Medium"></asp:Label>
                 </h3>
             </ContentTemplate>
     </asp:UpdatePanel>
         </form>
   
</body>
</html>
