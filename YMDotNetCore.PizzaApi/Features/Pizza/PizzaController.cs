using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YMDotNetCore.PizzaApi.Db;
using YMDotNetCore.PizzaApi.Features.Queries;
using YMDotNetCore.Share;

namespace YMDotNetCore.PizzaApi.Features.Pizza
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly DappperService _dapperService;
        public PizzaController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dapperService = new DappperService(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var lst = await _dbContext.Pizzas.ToListAsync();
            return Ok(lst);
        }

        [HttpGet("Extras")]
        public async Task<IActionResult> GetExtraAsync()
        {
            var lst = await _dbContext.PizzaExtras.ToListAsync();
            return Ok(lst);
        }
        //[HttpPost("Order/{invoiceNo}")]
        //public async Task<IActionResult> GetOrder(string invoiceNo)
        //{
        //    var item = await _dbContext.PizzaOrders.FirstOrDefaultAsync(x => x.PizzaOrderInvoiceNo == invoiceNo);

        //    var lst = await _dbContext.PizzaOrderDetails.Where(x => x.PizzaOrderInvoiceNo == invoiceNo).ToListAsync();
        //    return Ok(new
        //    {
        //        Order = item,
        //        OrderDetail = lst
        //    }
        //        );
        //}
        [HttpPost("Order/{invoiceNo}")]
        public IActionResult GetOrder(string invoiceNo)
        {
            var item = _dapperService.QueryFirstorDefault<PizzaOrderInvoiceHeadModel>
                (PizzaQuery.PizzaOrderQuery,new {PizzaOrderInvoiceNo = invoiceNo});
            var lst = _dapperService.Query<PizzaOrderInvoiceDetailModel>
                (PizzaQuery.PizzaOrderDetailQuery, new { PizzaOrderInvoiceNo = invoiceNo });

            var response = new PizzaOrderInvoiceResponse
            {
                Order = item,
                OrderDetail = lst,
            };
            return Ok(response);
        }

        [HttpPost("Order")]
        public async Task<IActionResult> OrderAsync(OrderRequest request)
        {
            var itemPizza = await _dbContext.Pizzas.FirstOrDefaultAsync(x => x.Id == request.PizzaId);
            var total = itemPizza.Price;
            if(request.Extras.Length > 0)
            {
              var  lstextra = await _dbContext.PizzaExtras.Where(x => request.Extras.Contains(x.Id)).ToListAsync();
    
              total += lstextra.Sum(x => x.Price);
            }
            var invoiceNo = DateTime.Now.ToString("yyyyMMddHHmmss");
            PizzaOrderModel pizzaOrderModel = new PizzaOrderModel 
            {
                PizzaId = request.PizzaId,
                PizzaOrderInvoiceNo = invoiceNo,
                TotalAmount = total
            };
            List<PizzaOrderDetailModel> pizzaOrderDetailModels = request.Extras.Select(extraId => new PizzaOrderDetailModel
            {
                PizzaExtraId = extraId,
                PizzaOrderInvoiceNo = invoiceNo
            }).ToList();

            await _dbContext.PizzaOrders.AddAsync(pizzaOrderModel);
            await _dbContext.PizzaOrderDetails.AddRangeAsync(pizzaOrderDetailModels);
            await _dbContext.SaveChangesAsync();  

            OrderRespose response = new OrderRespose()
            {
                InvoiceNo = invoiceNo,
                message = "Thank you",
                TotalAmount = total,
            };
            return Ok(response);
        }
        
    }
}
