using ProductMicroService.DTOS;
using Week3Assignment.ExceptionHandler;

namespace CartMicroService.Helper
{
    public class ProductService
    {

        public ProductService(string token)
        {
            ApiHelper.InitializeClient(token);
        }


        public async Task<ProductSendDto> loadProductById(int Id)
        {
            try
            {

                using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("https://localhost:7018/api/product/" + Id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        ProductSendDto product = await response.Content.ReadAsAsync<ProductSendDto>();
                        return product;
                    }
                    else
                    {

                        var a = response.StatusCode;

                        if (response.StatusCode.Equals(System.Net.HttpStatusCode.NotFound))
                        {
                            return null;
                        }
                        else
                        {
                            throw new CustomException(500, "The ProductService Is Down");
                        }
                    }
                }
            }catch(HttpRequestException ex)
            {
                throw new CustomException(500,"Product Service is Down");
            }
        }

        public async Task<List<ProductSendDto>> loadAllProducts()
        {

            try
            {
                using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("https://localhost:7018/api/products"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var products = await response.Content.ReadAsAsync<List<ProductSendDto>>();
                        return products;
                    }
                    else
                    {
                        if (response.StatusCode.Equals(System.Net.HttpStatusCode.NotFound))
                        {
                            return null;
                        }
                        else
                        {
                            throw new CustomException(500, "The ProductService Is Down");
                        }
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                throw new CustomException(500, "Product Service is Down");
            }
        }


        public async Task<Boolean> ChangeQuantity(int id,int value)
        {
            try
            {
                var a = JsonContent.Create(new { askedQuantity =value });
                using (HttpResponseMessage response = await ApiHelper.ApiClient.PutAsync("https://localhost:7018/api/product/" + id + "/Accept", a))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else if (response.StatusCode.Equals(System.Net.HttpStatusCode.BadRequest))
                    {
                        throw new CustomException(400, "Sorry but present Stock can't fulfill your Request");
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                throw new CustomException(500, "Product Service is Down");
            }
        }


        

    }
}
