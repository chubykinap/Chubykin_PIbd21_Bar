<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainForm.aspx.cs" Inherits="PlumbingRepairWebView.FormMain" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        #form1 {
            height: 666px;
            width: 1067px;
        }
    </style>
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Menu ID="Menu" runat="server" BackColor="White" ForeColor="Black" Height="150px">
            <Items>
                <asp:MenuItem Text="Справочники" Value="Справочники">
                    <asp:MenuItem Text="Клиенты" Value="Клиенты" NavigateUrl="~/CustomersForm.aspx"></asp:MenuItem>
                    <asp:MenuItem Text="Компоненты" Value="Компоненты" NavigateUrl="~/ElementsForm.aspx"></asp:MenuItem>
                    <asp:MenuItem Text="Услуги" Value="Коктейли" NavigateUrl="~/CocktailsForm.aspx"></asp:MenuItem>
                    <asp:MenuItem Text="Склады" Value="Склады" NavigateUrl="~/StoragesForm.aspx"></asp:MenuItem>
                    <asp:MenuItem Text="Сотрудники" Value="Сотрудники" NavigateUrl="~/ExecutorsForm.aspx"></asp:MenuItem>
                </asp:MenuItem>
                <asp:MenuItem Text="Пополнить склад" Value="Пополнить склад" NavigateUrl="~/OrderNewElementsForm.aspx"></asp:MenuItem>
            </Items>
        </asp:Menu>
        <asp:Button ID="ButtonCreateIndent" runat="server" Text="Создать заказ" OnClick="ButtonCreate_Click" />
        <asp:Button ID="ButtonTakeIndentInWork" runat="server" Text="Отдать на выполнение" OnClick="ButtonTakeInWork_Click" />
        <asp:Button ID="ButtonIndentReady" runat="server" Text="Заказ готов" OnClick="ButtonReady_Click" />
        <asp:Button ID="ButtonIndentPayed" runat="server" Text="Заказ оплачен" OnClick="ButtonPayed_Click" />
        <asp:Button ID="ButtonUpd" runat="server" Text="Обновить список" OnClick="ButtonUpd_Click" />
        <asp:GridView ID="dataGridView1" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1" ShowHeaderWhenEmpty="True">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
                <asp:CommandField ShowSelectButton="true" SelectText=">>" />
                <asp:BoundField DataField="CustomerName" HeaderText="CustomerName" SortExpression="CustomerName" />
                <asp:BoundField DataField="ServiceName" HeaderText="ServiceName" SortExpression="ServiceName" />
                <asp:BoundField DataField="PerformerName" HeaderText="PerformerName" SortExpression="PerformerName" />
                <asp:BoundField DataField="Count" HeaderText="Count" SortExpression="Count" />
                <asp:BoundField DataField="Sum" HeaderText="Sum" SortExpression="Sum" />
                <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                <asp:BoundField DataField="DateCreate" HeaderText="DateCreate" SortExpression="DateCreate" />
                <asp:BoundField DataField="DatePerforme" HeaderText="DatePerforme" SortExpression="DatePerforme" />
            </Columns>
            <SelectedRowStyle BackColor="#CCCCCC" />
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="PlumbingRepairService.BindingModels.IndentBindingModel" DeleteMethod="PayOrder" InsertMethod="CreateOrder" SelectMethod="GetList" TypeName="PlumbingRepairService.ImplementationsList.MainServiceList" UpdateMethod="TakeOrderInWork">
            <DeleteParameters>
                <asp:Parameter Name="id" Type="Int32" />
            </DeleteParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
