<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainForm.aspx.cs" Inherits="BarWebView.FormMain" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
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
                    <asp:MenuItem Text="Коктейли" Value="Коктейли" NavigateUrl="~/CocktailsForm.aspx"></asp:MenuItem>
                    <asp:MenuItem Text="Склады" Value="Склады" NavigateUrl="~/StoragesForm.aspx"></asp:MenuItem>
                    <asp:MenuItem Text="Сотрудники" Value="Сотрудники" NavigateUrl="~/ExecutorsForm.aspx"></asp:MenuItem>
                </asp:MenuItem>
                <asp:MenuItem Text="Пополнить склад" Value="Пополнить склад" NavigateUrl="~/OrderNewElementsForm.aspx"></asp:MenuItem>
                <asp:MenuItem Text="Отчеты" Value="Отчеты">
                    <asp:MenuItem NavigateUrl="~/SavePriceListWebForm.aspx" Text="Прайс изделий" Value="Прайс изделий"></asp:MenuItem>
                    <asp:MenuItem NavigateUrl="~/StoragesLoadWebForm.aspx" Text="Загруженность складов" Value="Загруженность складов"></asp:MenuItem>
                    <asp:MenuItem NavigateUrl="~/CustomerOrdersWebForm.aspx" Text="Заказы клиентов" Value="Заказы клиентов"></asp:MenuItem>
                </asp:MenuItem>
            </Items>
        </asp:Menu>
        <asp:Button ID="ButtonCreate" runat="server" Text="Создать заказ" OnClick="ButtonCreate_Click" />
        <asp:Button ID="ButtonTakeInWork" runat="server" Text="Отдать на выполнение" OnClick="ButtonTakeInWork_Click" />
        <asp:Button ID="ButtonReady" runat="server" Text="Заказ готов" OnClick="ButtonReady_Click" />
        <asp:Button ID="ButtonPayed" runat="server" Text="Заказ оплачен" OnClick="ButtonPayed_Click" />
        <asp:Button ID="ButtonUpd" runat="server" Text="Обновить список" OnClick="ButtonUpd_Click" />
        <asp:GridView ID="dataGridView1" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1" ShowHeaderWhenEmpty="True">
            <Columns>
                <asp:CommandField ShowSelectButton="True">
                    <ItemStyle Width="50px" />
                </asp:CommandField>
                <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" Visible="False" />
                <asp:BoundField DataField="CustomerID" HeaderText="CustomerID" SortExpression="CustomerID" Visible="False" />
                <asp:BoundField DataField="CustomerFIO" HeaderText="CustomerFIO" SortExpression="CustomerFIO">
                    <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="CocktailID" HeaderText="CocktailID" SortExpression="CocktailID" Visible="False" />
                <asp:BoundField DataField="CocktailName" HeaderText="CocktailName" SortExpression="CocktailName">
                    <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="ExecutorID" HeaderText="ExecutorID" SortExpression="ExecutorID" Visible="False" />
                <asp:BoundField DataField="ExecutorName" HeaderText="ExecutorName" SortExpression="ExecutorName">
                    <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="Count" HeaderText="Count" SortExpression="Count">
                    <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="Sum" HeaderText="Sum" SortExpression="Sum">
                    <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status">
                    <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="DateCreate" HeaderText="DateCreate" SortExpression="DateCreate">
                    <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="DateImplement" HeaderText="DateImplement" SortExpression="DateImplement">
                    <ItemStyle Width="100px" />
                </asp:BoundField>
            </Columns>
            <SelectedRowStyle BackColor="#CCCCCC" />
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DeleteMethod="PayOrder" SelectMethod="GetList" TypeName="BarService.BDImplementation.MainBD">
            <DeleteParameters>
                <asp:Parameter Name="id" Type="Int32" />
            </DeleteParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
