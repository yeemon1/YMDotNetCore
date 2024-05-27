namespace YMDotNetCore.PizzaApi.Features.Queries
{
    public class PizzaQuery
    {
        public const string PizzaOrderQuery = @"select po.*,P.Pizza,P.Price from [dbo].[Tbl_PizzaOrder] po " +
            "inner  join Tbl_Pizza p on p.PizzaId= po.pizzaId where PizzaOrderInvoiceNo = @PizzaOrderInvoiceNo;";

        public const string PizzaOrderDetailQuery = @"select pod.*,pe.PizzaExtraName,pe.Price from [dbo].[Tbl_PizzaOrderDetail] pod " +
            "inner  join Tbl_PizzaExtra pe  on pod.PizzaExtraId= pe.pizzaExtraId where PizzaOrderInvoiceNo = @PizzaOrderInvoiceNo;";
    }
}
