<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPager.Master" CodeBehind="Aspirantes_Alta.aspx.vb" Inherits="ProyectoWeb.Vehiculos_Alta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<div style="margin-left:20px">--%>
           <%-- <img src="Assets/linkedin_banner_image_2.png" width="1300" height="440px"/>--%>
 <%--</div>--%>
   <div class="row">
            <div class="col-xs-12">
                <div class="box box-primary">
                    <div class="box-header">
                        
                        <h3 class="box-title">Agregar Vehiculos</h3>
                    </div>
                    <div class="box-body table-responsive">
                        <div class="wrapper row-offcanvas row-offcanvas-left">
                        <br />
                        <strong>Colores </strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
    <asp:DropDownList ID="DDLColor" runat="server"></asp:DropDownList>
    <br />
                        <br />
                        <strong>Tipos </strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:DropDownList ID="DDLTipoauto" runat="server"></asp:DropDownList>
    <br />
                        <br />
                        <strong>Marcas</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:DropDownList ID="DDLMarca" runat="server"></asp:DropDownList>
<br />
                        <br />
                        <strong>Modelos </strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
    <asp:DropDownList ID="DDLModelo" runat="server"></asp:DropDownList>
<br />


    <br />
                        <strong>Descripcion&nbsp;&nbsp;
    </strong>&nbsp;<asp:TextBox ID="TxtDescripcion" runat="server"></asp:TextBox>
    
                        <br />
                        <br />
                        <strong>Precio Venta </strong> &nbsp;<asp:TextBox ID="TxtPventa" runat="server"></asp:TextBox>
    <br />
                        <br />
                        <strong>Cantidad</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="TxtCantidad" runat="server"></asp:TextBox>
    <br />
                        <br />
                         
                            
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="BtnAgregar" class="btn btn-primary" data-dismiss="modal" runat="server" Text="Agregar" />
    <br />
    <br />
               </div>                 
</asp:Content>
