<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MVCVenta.ViewModels.Transaccion>" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Comprar</h2>


     <% using (Html.BeginForm("Create", "Compra", FormMethod.Post))
       {%>
        <%: Html.ValidationSummary(true) %>

         <fieldset>
            <legend>Compra</legend>
            
           
          
            <div class="editor-label">
               Numero de Tarjeta
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.NumeroTarjeta) %>
                <%: Html.ValidationMessageFor(model => model.NumeroTarjeta)%>
            </div>
            
            <div class="editor-label">
                 Codigo de Tarjeta
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.CodigoTarjeta) %>
                <%: Html.ValidationMessageFor(model => model.CodigoTarjeta)%>
            </div>


             <div id="Combo">
                    <select id='dropDownTipoPago' name='dropDownTipoPago' style="width: 150px">
                        <option value="1" selected="selected">Soles</option>
                        <option value="2">Dolares</option>
                    </select>
                </div>
            
            <div class="editor-label">
                Monto Total
            </div>
            <div class="editor-label">
             <%-- <%: Html.TextBoxFor(model => model.MontoTotal) %>
                <%: Html.ValidationMessageFor(model => model.MontoTotal)%>--%>

                <%=  ViewData["MontoCarrito"]%>
             
            </div>
            
              
            <p>
                <input type="submit" value="Comprar" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%: Html.ActionLink("Regresar", "Index/1","Producto") %>
    </div>

</asp:Content>
