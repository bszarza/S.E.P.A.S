﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="ProyectoWeb.Login" %>

<!DOCTYPE html>
  <html lang="en">
    <head>
       <meta charset="UTF-8" />
         <link rel="icon" type="image/x-icon" href="/assets/logo-vt.svg" />
           <meta http-equiv="X-UA-Compatible" content="IE=edge" />
             <meta name="viewport" content="width=device-width, initial-scale=1.0" />
                 <title>S.E.P.A.S</title>
                 <link
              href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.0/dist/css/bootstrap.min.css"
              rel="stylesheet"
             integrity="sha384-gH2yIJqKdNHPEq0n4Mqa/HGKIhSkIHeL5AyhkYV8i59U5AR6csBvApHHNl/vI1Bx"
             crossorigin="anonymous"/>
         </head>
               <body class="bg-info d-flex justify-content-center align-items-center vh-100">
                  <form id="form1" runat="server">
                      <div
                       class="bg-white p-5 rounded-5 text-secondary shadow"
                    style="width: 25rem">
                           
      <div class="d-flex justify-content-center">
          <img src="Assets/image_processing20210525-31447-15d8lag.gif" alt="login-icon"
             style="height: 14rem"/>
         
              </div>
          <div class="text-center fs-1 fw-bold">S.E.P.A.S</div>
                          
                         
      <div class="input-group mt-4">
        <div class="input-group-text bg-info">
          <img
            src="/assets/username-icon.svg"
            alt="username-icon"
            style="height: 1rem"
          />
        </div>
          
        <input <asp:textbox id="txtusuario" runat="server"
          class="form-control bg-light"
          type="text"
          placeholder="Username"
        />
            
      </div>
      <div class="input-group mt-1">
        <div class="input-group-text bg-info">
          <img
            src="/assets/password-icon.svg"
            alt="password-icon"
            style="height: 1rem"
          />
        </div>
          <input <asp:textbox ID="txtclave" runat="server"
          class="form-control bg-light"
          type="password"
          placeholder="Password"/>
         
           
   </div>
        <div>
            <center></center>
        <asp:Button ID="BtnIngresar" runat="server" Text="INICIAR SESION" class="btn btn-info text-white w-100 mt-4 fw-semibold shadow-sm"/>
    
            </center>
     </div>
     

 
    </div>
          </form>
  </body>

            
</html>
