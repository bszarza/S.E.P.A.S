<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPager.Master" CodeBehind="Usuarios_Listar.aspx.vb" Inherits="ProyectoWeb.Usuarios_Listar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

       <div class="row">
            <div class="col-xs-12">
                <div class="box box-primary">
                    <div class="box-header">
                        <h3 class="box-title">Listado</h3>
                    </div>
                    <div class="box-body table-responsive">
                        <div class="col-lg-3">
                                <button type="button" class="btn btn-primary" data-dismiss="modal" onclick="fnAgregar()">Agregar</button>                                
                        </div>
                        <div class="form-group">
                                <label>Filtrar por Curso</label>
                                <asp:DropDownList ID="DrpCursos" runat="server" AutoPostBack="true" CssClass="btn btn-primary dropdown-toggle"></asp:DropDownList>
                        </div>
                        <asp:GridView ID="GdvAspirantes" runat="server"  AllowPaging="True" AutoGenerateColumns="False" CssClass="grid aej_all table table-striped table-bordered dt-responsive dataTable no-footer" 
                              PageSize="90" Width="100%" EnableModelValidation="True"   EmptyDataText="No se encontraron Registros.">
                                <Columns>
                                    <asp:ButtonField   HeaderText="Eliminar"     Text="<i class='fa fa-ban icon-large fa-1x' style='font-size:18px;color:red'></i>" CommandName="Eliminar" />             
                                    
                                    <asp:TemplateField Visible="FALSE" HeaderText="Nro">
                                            <ItemTemplate>
                                                     <asp:Label ID="IDEliminar" runat="server" Text='<%# Eval("ID") %>' />
                                            </ItemTemplate>
                                    </asp:TemplateField>

                                    <%--   <asp:TemplateField Visible="TRUE" HeaderText="Nro">
                                              <ItemTemplate>
                                                       <asp:Label ID="descripcion" runat="server" Text='<%# Eval("descripcion") %>' />
                                              </ItemTemplate>
                                    </asp:TemplateField>--%>

                                
                                </Columns>
                                <Columns>

                                    <%--<asp:ButtonField   HeaderText="Vender"     Text="<i class='fa fa-shopping-cart' style='font-size:28px;color:GREEN'></i>" CommandName="Eliminar" />--%>
             
                                    <asp:TemplateField Visible="FALSE" HeaderText="Nro">
                                            <ItemTemplate>
                                                    <asp:Label ID="ID" runat="server" Text='<%# Eval("ID") %>' />
                                            </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField Visible="TRUE" HeaderText="Nombre">
                                            <ItemTemplate>
                                                    <asp:Label ID="NOMBRE" runat="server" Text='<%# Eval("Nombre") %>' />
                                            </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="TRUE" HeaderText="Apellido">
                                            <ItemTemplate>
                                                     <asp:Label ID="APELLIDO" runat="server" Text='<%# Eval("Apellido") %>' />
                                            </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField Visible="TRUE" HeaderText="Dni">
                                            <ItemTemplate>
                                                    <asp:Label ID="DNI" runat="server" Text='<%# Eval("Dni") %>' />
                                            </ItemTemplate>
                                    </asp:TemplateField>
                         
                                    <asp:TemplateField Visible="TRUE" HeaderText="CE">
                                            <ItemTemplate>
                                                     <asp:Label ID="CE" runat="server" Text='<%# Eval("CE") %>' />
                                            </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField Visible="TRUE" HeaderText="Genero">
                                            <ItemTemplate>
                                                    <asp:Label ID="GENERO" runat="server" Text='<%# Eval("Genero") %>' />
                                            </ItemTemplate>
                                    </asp:TemplateField>
                         
                                    <asp:TemplateField Visible="TRUE" HeaderText="Curso">
                                            <ItemTemplate>
                                                     <asp:Label ID="CURSO" runat="server" Text='<%# Eval("Curso") %>' />
                                            </ItemTemplate>
                                    </asp:TemplateField>

                                    <%--<asp:TemplateField Visible="TRUE" HeaderText="Cantidad">
                                            <ItemTemplate>
                                                    <asp:Label ID="Cantidad" runat="server" Text='<%# Eval("Cantidad") %>' />
                                            </ItemTemplate>
                                    </asp:TemplateField>--%>      
                                    <asp:ButtonField  HeaderText="Editar" Text="<i class='fa fa-user' style='font-size:28px;color:primary'></i>" CommandName="Editar"/>                                                                            
                               
                                </Columns>
                            
                        </asp:GridView>

                    </div>

           
                </div>
            </div>
   </div>

    

    <div class="modal fade" id="mdlAgregar" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalLabelAgregar">Agregar</h4>
                </div>
                <div class="modal-body">
                    
                   
                    <div class="form-group">
                        <label>Nombre</label>
                        <asp:TextBox ID="TxtNombre" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                    </div>
                     <div class="form-group">
                        <label>Apellido</label>
                        <asp:TextBox ID="TxtApellido" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                    </div>
                     <div class="form-group">
                        <label>Dni</label>
                        <asp:TextBox ID="TxtDni" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                    </div>
                     <div class="form-group">
                        <label>CE</label>     
                        <asp:TextBox ID="TxtCe" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                    </div>
                     <div class="form-group">
                        <label>Genero</label>
                        <asp:DropDownList ID="DDLGenero" runat="server" CssClass="btn btn-info dropdown-toggle"></asp:DropDownList>
                    </div>
                     <div class="form-group">
                        <label>Curso</label>
                      <asp:DropDownList ID="DDLCurso" runat="server" CssClass="btn btn-info dropdown-toggle"></asp:DropDownList>
                    </div>
                  
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>                   
                    <asp:Button runat ="server" ID = "BtnAgregar"  tabIndex="12"  CssClass="btn btn-primary dropdown-toggle btn-group-lg" text="Agregar"  ></asp:Button>
      
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
                        
                    </div>
                    <div class="modal-body">
                    <div class="form-group">
                        <asp:TextBox ID="txtId" runat="server" Text="" CssClass="form-control" Enabled="false" Visible="false"></asp:TextBox>
      
                    <div class="form-group">
                        <label>Nombre</label>
                        <asp:TextBox ID="TxtNombreEditar" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                    </div>
                     <div class="form-group">
                        <label>Apellido</label>
                        <asp:TextBox ID="TxtApellidoEditar" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                    </div>
                     <div class="form-group">
                        <label>Dni</label>
                        <asp:TextBox ID="TxtDniEditar" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                    </div>
                     <div class="form-group">
                        <label>CE</label>
                        <asp:TextBox ID="TxtCeEditar" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                    </div>
                     <div class="form-group">
                        <label>Genero</label>
                        <asp:DropDownList ID="DDLGeneroEditar" runat="server" CssClass="btn btn-info dropdown-toggle"></asp:DropDownList>

                        <%--<asp:TextBox ID="TextBox4" runat="server" Text="" CssClass="form-control"></asp:TextBox>--%>
                    </div>
                     <div class="form-group">
                        <label>Curso</label>
                      <asp:DropDownList ID="DDLCursoEditar" runat="server" CssClass="btn btn-info dropdown-toggle"></asp:DropDownList>

                    </div>
                  
                </div>
                   
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>                   
                    <asp:Button runat ="server" ID = "BtnEditarColor"  tabIndex="12"  CssClass="btn btn-primary dropdown-toggle btn-group-lg" text="Actualizar"  ></asp:Button>
      
                </div>

            </div>
        </div>
    </div>
         </div>


     <div class="modal inmodal fade in" id="mdlEliminarColor" tabindex="-1" role="dialog" aria-hidden="true">
         <div class="modal-dialog modal-md">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                     
                 

                    <div class="panel-heading" style="background-color: #1ab394; color: white;">Eliminar Registro
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
                                          <asp:TextBox ID="ColorEliminar" clientIdmode="static"  CssClass="form-control"  runat="server" ReadOnly="true" visible="FALSE"></asp:TextBox>
                                       
                                            <label for="dgdsg" class="control-label">Aspirante</label>
                                                 <div class="form-group">
                        <asp:TextBox ID="TxtApellidoEliminar" runat="server" ReadOnly="true" Text="" CssClass="form-control"></asp:TextBox>
                    </div>
                                           
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


       

        function fnAgregar() { $('#mdlAgregar').modal({ backdrop: 'static', keyboard: false }); };

        function fnEditarColor() { $('#mdlEditarColor').modal({ backdrop: 'static', keyboard: false }); };


        function fnEliminarColor() { $('#mdlEliminarColor').modal({ backdrop: 'static', keyboard: false }); };


    </script>

</asp:Content>
