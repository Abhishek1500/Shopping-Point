
using CartMicroService.Data;
using CartMicroService.DTOS;
using CartMicroService.Helper;
using Week3Assignment.ExceptionHandler;

namespace CartMicroService.MiddleLayers
{
    public static class userHistoryMiddleLayer
    {

        public static async Task<List<HistorySendDto>> UserWholeRquestLayer(DataContext _dbCon,int UserId,string token)
        {
            UserService userserve = new UserService(token);

            var user = userserve.loadUserById(UserId);
            if (user == null)
            {
                throw new CustomException(404, "UserNot Found");
            }

            var requests = _dbCon.CartRequests.Where(x => x.UserId == UserId).ToList();
            ProductService prodserve = new ProductService(token);
            var products = await prodserve.loadAllProducts();

            var history = requests.Join(products, x => x.ProductId, y => y.Id, (r, p) => new HistorySendDto
            {
                RequestId = r.Id,
                ProductId = p.Id,
                ProductName = p.ProductName,
                ProductCompany = p.ProductCompany,
                Imageurl = p.Imageurl,
                Price = p.Price,
                CategoryName = p.CategoryName,
                status = r.status,
                Count = r.Count,
                Remark = r.Remark,
                LastUpdate = r.LastUpdate
            });

            return history.ToList();
        } 

    }
}
