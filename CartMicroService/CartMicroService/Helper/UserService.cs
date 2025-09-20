using ProductMicroService.DTOS;
using UserMicroService.DTOS;
using Week3Assignment.ExceptionHandler;

namespace CartMicroService.Helper
{
    public class UserService
    {

        public UserService(string token)
        {
            ApiHelper.InitializeClient(token);
        }


        public async Task<UserSendDto> loadUserById(int Id)
        {
            try
            {
                using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("https://localhost:7277/api/users/" + Id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        UserSendDto user = await response.Content.ReadAsAsync<UserSendDto>();
                        return user;
                    }
                    else return null;

                }
            }catch(HttpRequestException e)
            {
                throw new CustomException(500, "User Service is Down");
            }
        }


        public async Task<List<UserSendDto>> loadAllUsers()
        {
            try
            {
                using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("https://localhost:7277/api/users"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var users = await response.Content.ReadAsAsync<List<UserSendDto>>();
                        return users;
                    }
                    else
                    {
                        return null;
                    }
                }
            }catch(HttpRequestException e)
            {
                throw new CustomException(500, "User Service is Down");
            }
        }




    }
}
