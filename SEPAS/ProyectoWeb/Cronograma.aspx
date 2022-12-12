<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPager.Master" CodeBehind="Cronograma.aspx.vb" Inherits="ProyectoWeb.Cronograma" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class="row">
            <div class="col-xs-12">
                <div class="box box-primary">
                    <div class="box-header">
                        <h3 class="box-title">Lista de Aspirantes</h3>
                    </div>
                    <div class="box-body table-responsive">
                        <%--<div class="col-lg-3">
                                        <button type="button" class="btn btn-primary" data-dismiss="modal" onclick="fnAgregar()">Agregar Horario</button>
                                
                                         </div>--%>
                            <div class="form-group">
                        <label>Filtrar por Curso</label>
                        <asp:DropDownList ID="DDLCursoFil" CssClass="btn btn-primary dropdown-toggle" runat="server" AutoPostBack="true"></asp:DropDownList>
   
                    </div>
                      <%--  <div class="col-lg-3">
                         
                                       <asp:Button runat ="server" ID = "BtnCronogramaFil"  tabIndex="12"  CssClass="btn btn-primary dropdown-toggle btn-group-lg" text="Aux Mecanico"  ></asp:Button> 
                                         </div>
                           <div class="col-lg-3">
                         
                                       <asp:Button runat ="server" ID = "BtnCronogramaProg"  tabIndex="12"  CssClass="btn btn-primary dropdown-toggle btn-group-lg" text="Aux Programador"  ></asp:Button> 
                                         </div>--%>

                         <asp:GridView ID="GdvCronograma" runat="server"  AllowPaging="True" AutoGenerateColumns="False" CssClass="grid aej_all table table-striped table-bordered dt-responsive dataTable no-footer" 
                              PageSize="90" Width="100%" EnableModelValidation="True"   EmptyDataText="No se encontraron Registros.">

                                                  
                     <Columns>
                        
                           
                           <asp:TemplateField Visible="FALSE" HeaderText="Nro">
                                 <ItemTemplate>
                                          <asp:Label ID="ID" runat="server" Text='<%# Eval("ID") %>' />
                                 </ItemTemplate>
                       </asp:TemplateField>

                           <asp:TemplateField Visible="TRUE" HeaderText="Horarios">
                                 <ItemTemplate>
                                         <asp:Label ID="Horario" runat="server" Text='<%# Eval("Horario") %>' />
                                 </ItemTemplate>
                       </asp:TemplateField>
                            <asp:TemplateField Visible="TRUE" HeaderText="Lunes">
                                 <ItemTemplate>
                                          <asp:Label ID="Lunes" runat="server" Text='<%# Eval("Lunes") %>' />
                                 </ItemTemplate>
                       </asp:TemplateField>

                           <asp:TemplateField Visible="TRUE" HeaderText="Martes">
                                 <ItemTemplate>
                                         <asp:Label ID="Martes" runat="server" Text='<%# Eval("Martes") %>' />
                                 </ItemTemplate>
                       </asp:TemplateField>
                         
                            <asp:TemplateField Visible="TRUE" HeaderText="Miercoles">
                                 <ItemTemplate>
                                          <asp:Label ID="Miercoles" runat="server" Text='<%# Eval("Miercoles") %>' />
                                 </ItemTemplate>
                       </asp:TemplateField>

                           <asp:TemplateField Visible="TRUE" HeaderText="Jueves">
                                 <ItemTemplate>
                                         <asp:Label ID="Jueves" runat="server" Text='<%# Eval("Jueves") %>' />
                                 </ItemTemplate>
                       </asp:TemplateField>
                         
                            <asp:TemplateField Visible="TRUE" HeaderText="Viernes">
                                 <ItemTemplate>
                                          <asp:Label ID="Viernes" runat="server" Text='<%# Eval("Viernes") %>' />
                                 </ItemTemplate>
                       </asp:TemplateField>

                         
                      <asp:ButtonField  HeaderText="Editar" Text="<i class='fa fa-cog' style='font-size:28px;color:primary'></i>" CommandName="Editar"/>                                            
                                
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
                    <h4 class="modal-title" id="myModalLabelAgregar">Agregar Horario</h4>
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
                        <asp:DropDownList ID="DDLGenero" runat="server"></asp:DropDownList>
   
                    </div>
                     <div class="form-group">
                        <label>Curso</label>
                      <asp:DropDownList ID="DDLCurso" runat="server"></asp:DropDownList>
                    </div>
                  
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>                   
                    <asp:Button runat ="server" ID = "BtnAgregarColor"  tabIndex="12"  CssClass="btn btn-primary dropdown-toggle btn-group-lg" text="Agregar"  ></asp:Button>
      
                </div>

            </div>
        </div>
    </div>


    

    <div class="modal fade" id="mdlEditarCronograma" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
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
                        <asp:TextBox ID="txtIdEditar" runat="server"  Text="" CssClass="form-control" Enabled="false" Visible="false"></asp:TextBox>
      
                    <div class="form-group">
                        <label>Horario</label>
                        <asp:TextBox ID="TxtHorario" runat="server" ReadOnly="true" Text="" CssClass="form-control"></asp:TextBox>
                    </div>
                     <div class="form-group">
                        <label>Lunes</label>
                        <asp:TextBox ID="TxtLunes" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                    </div>
                     <div class="form-group">
                        <label>Martes</label>
                        <asp:TextBox ID="TxtMartes" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                    </div>
                     <div class="form-group">
                        <label>Miercoles</label>
                        <asp:TextBox ID="TxtMiercoles" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                    </div>
                        <div class="form-group">
                        <label>Jueves</label>
                        <asp:TextBox ID="TxtJueves" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                    </div>
                        <div class="form-group">
                        <label>Viernes</label>
                        <asp:TextBox ID="TxtViernes" runat="server" Text="" CssClass="form-control"></asp:TextBox>
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


       

        function fnAgregar() { $('#mdlAgregarColor').modal({ backdrop: 'static', keyboard: false }); };

        function fnEditarCronograma() { $('#mdlEditarCronograma').modal({ backdrop: 'static', keyboard: false }); };


        function fnEliminarColor() { $('#mdlEliminarColor').modal({ backdrop: 'static', keyboard: false }); };


    </script>
</asp:Content>


