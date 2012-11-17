<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MVCVenta.ViewModels.ProductoList>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Details</h2>

    <table>
        <tr>
           <td>
               <img alt="<%: item.DescripcionProducto %>"  src="../../Imagenes/Productos/<%: item.IdProducto%>.png"
                                width="70" height="50" /></td> 
           <td></td>
        </tr>
    
    </table>

</asp:Content>
