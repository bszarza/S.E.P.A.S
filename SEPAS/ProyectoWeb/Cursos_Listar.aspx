<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPager.Master" CodeBehind="Cursos_Listar.aspx.vb" Inherits="ProyectoWeb.TipoModelo_Listar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <%--  <center>
        <h1 class="mt-4">Auto-Latino</h1>
<ol class="breadcrumb mb-4">
    
</ol>
</center>--%>
    <%--<img src="Assets/linkedin_banner_image_2.png" width="1300" height="440px"/>--%>


   <div class="row">
            <div class="col-xs-12">
                <div class="box box-primary">
                    <div class="box-header">
                        <h3 class="box-title">Lista de Cursos</h3>
                    </div>
                    <div class="box-body table-responsive">
                          <div class="col-lg-3">
                                        <button type="button" class="btn btn-primary" data-dismiss="modal" onclick="fnAgregar()">Agregar Cursos</button>
                                
                                         </div>

                         <asp:GridView ID="GdvTipoModelo" runat="server"  AllowPaging="True" AutoGenerateColumns="False" CssClass="grid aej_all table table-striped table-bordered dt-responsive dataTable no-footer" 
                              PageSize="90" Width="100%" EnableModelValidation="True"   EmptyDataText="No se encontraron Registros.">
                      <%--  <Columns>  

                 <asp:GridView ID="GdvColores" AutoGenerateColumns="false" CssClass="table table-active table-bordered" runat="server">--%>

                     <Columns>

                             <asp:ButtonField   HeaderText="Eliminar" Text="<i class='fa fa-ban icon-large fa-1x' style='font-size:18px;color:red'></i>"  CommandName="Eliminar" />
             
                           <asp:TemplateField Visible="FALSE" HeaderText="Nro">
                                 <ItemTemplate>
                                          <asp:Label ID="ID" runat="server" Text='<%# Eval("ID") %>' />
                                 </ItemTemplate>
                       </asp:TemplateField>

                           <asp:TemplateField Visible="TRUE" HeaderText="Cursos">
                                 <ItemTemplate>
                                         <asp:Label ID="descripcion" runat="server" Text='<%# Eval("DESCRIPCION") %>' />
                                 </ItemTemplate>
                       </asp:TemplateField>

                         
                      <asp:ButtonField  HeaderText="Editar"     Text="<i class='fa fa-cog icon-large fa-1x'></i>" CommandName="Editar"/>                                            
                                
                     </Columns>

                     
                 </asp:GridView>

            </div>

           
        </div>
    </div>
       </div>

    

    <div class="modal fade" id="mdlAgregarColor" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalLabelAgregar">Agregar Modelo</h4>
                </div>
                <div class="modal-body">
                    
                   
                    <div class="form-group">
                        <label>Descripcion</label>
                    </div>
                    <div class="form-group">
                        <asp:TextBox ID="TxtColorAgregar" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>                   
                    <asp:Button runat ="server" ID = "BtnAgregarColor"  tabIndex="12"  CssClass="btn btn-primary dropdown-toggle btn-group-lg" text="Agregar"  ></asp:Button>
      
                </div>

            </div>
        </div>
    </div>


    <div class="modal fade" id="mdlEditarColor" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalLabel">Editar registro</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label>Id</label>
                    </div>
                    <div class="form-group">
                        <asp:TextBox ID="txtIdColorEditar" runat="server" Text="" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Descripcion</label>
                    </div>
                    <div class="form-group">
                        <asp:TextBox ID="TxtdescripcionColorEditar" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>                   
                    <asp:Button runat ="server" ID = "BtnEditarColor"  tabIndex="12"  CssClass="btn btn-primary dropdown-toggle btn-group-lg" text="Actualizar"  ></asp:Button>
      
                </div>

            </div>
        </div>
    </div>


     <div class="modal inmodal fade in" id="mdlEliminarColor" tabindex="-1" role="dialog" aria-hidden="true">
         <div class="modal-dialog modal-md">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                     
                 

                    <div class="panel-heading" style="background-color: #1ab394; color: white;">Eliminar Color
                     <br />   <i class="fa fa-trash-o fa-2x fa-lg" style="color:#FFFFFF;"></i>
                    </div>


                </div>
                <div class="modal-body">
                     <div class="row" runat="server" id="Div5">

                        <div class="col-sm-12">                        
                         <asp:Label ID="lblALIQUIDACION_ELIMINAR" CssClass="label-danger -error alert alert-info btn-block"  runat="server" Text="" ></asp:Label>         
                        </div> 
                        

                         <div class="col-lg-12">
                                        <div class="form-group">
                                          <asp:TextBox ID="ColorEliminar" clientIdmode="static"  CssClass="form-control"  runat="server" ReadOnly="true" visible="TRUE"></asp:TextBox>
                                       
                                            <label for="dgdsg" class="control-label">Modelo</label>
                                                
                                           
                                        </div>
                                    </div>
                        
                           
                  </div>      
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>                   
                    <asp:Button runat ="server" ID = "BtnEliminarColor"  tabIndex="12"  CssClass="btn btn-primary dropdown-toggle btn-group-lg" text="Eliminar"  ></asp:Button>
      
                </div>
            </div>
        </div>
    </div>
        
 

    <script>


       

        function fnAgregar() { $('#mdlAgregarColor').modal({ backdrop: 'static', keyboard: false }); };

        function fnEditarColor() { $('#mdlEditarColor').modal({ backdrop: 'static', keyboard: false }); };


        function fnEliminarColor() { $('#mdlEliminarColor').modal({ backdrop: 'static', keyboard: false }); };


    </script>
</asp:Content>
