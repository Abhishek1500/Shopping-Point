using CartMicroService.Data;
using CartMicroService.DTOS;
using CartMicroService.Helper;
using MediatR;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;
using Week3Assignment.ExceptionHandler;

namespace CartMicroService.Commands
{
    public class RespondToCartRequestCommand : IRequest<Unit>
    {

        public RespondToRequestDto NewStatus;
        public int Id;
        public string Token;

        public RespondToCartRequestCommand(int id,RespondToRequestDto newStatus,string token)
        {
            Id = id;
            NewStatus = newStatus;
            Token = token;
        }
    }


    public class RespondToCartRequestCommandHandler : MediatR.IRequestHandler<RespondToCartRequestCommand, Unit>
    {
        public DataContext _dbCon;
        public IConfiguration _config;
        public RespondToCartRequestCommandHandler(DataContext dbCon,IConfiguration config)
        {
            _dbCon = dbCon;
            _config = config;
        }
        public async Task<Unit> Handle(RespondToCartRequestCommand request, CancellationToken cancellationToken)
        {
            var req = await _dbCon.CartRequests.FindAsync(request.Id);
            if (req == null)
            {
                throw new CustomException(404, "Request Not Found");
            }
            
            ProductService prodserve = new ProductService(request.Token);

            var product = await prodserve.loadProductById(req.ProductId);
            if (product == null)
            {
                throw new CustomException(400, "Request is Dead as Product is Deleted");
            }
            UserService userserve = new UserService(request.Token);

            var user = await userserve.loadUserById(req.UserId);
            if (user == null)
            {
                throw new CustomException(400, "Request is Dead as User is no longer in Db");
            }

            if (req.status == "carted")
            {
                throw new CustomException(400, "Not allowed to Respond to this request");
            }

            if (req.status != "pending")
            {
                throw new CustomException(400, "The Request is already responded and status Code is " + req.status);
            }
            if (request.NewStatus.NewStatus == "approved")
            {
                var res = await prodserve.ChangeQuantity(req.ProductId, req.Count);
                if (res == false)
                {
                    throw new CustomException(500, "Problem With the microServices");
                }
            }

            req.status = request.NewStatus.NewStatus;
            req.Remark = request.NewStatus.Remark;















            var message = "";
            if (req.status == "approved")
            {
                message = "Your Request is approved for product: " + product.ProductName + "<br> delivery Date: " + (DateTime.Today.AddDays(1)).Date;
            }
            else
            {
                if (req.Remark != "")
                    message = "Your Request is rejected for product: " + product.ProductName + "<br> Reason: " + req.Remark;
                else
                    message = "Your Request is rejected for product: " + product.ProductName;
            }


            Configuration.Default.ApiKey["api-key"] = "key";

            var apiInstance = new TransactionalEmailsApi();
            string SenderName = "Electornic Commerce";
            string SenderEmail = "ecom@ecom.com";
            SendSmtpEmailSender emailSender = new SendSmtpEmailSender(SenderName, SenderEmail);
            
            SendSmtpEmailTo smtpEmailTo = new SendSmtpEmailTo(SenderEmail, SenderName);
            List<SendSmtpEmailTo> To = new List<SendSmtpEmailTo>();
            To.Add(smtpEmailTo);
            
            
            string HtmlContent = message;
            string TextContent = null;
            try
            {
                var sendSmtpEmail = new SendSmtpEmail(emailSender, To, null, null, HtmlContent, TextContent, "Response For Requested product");
                CreateSmtpEmail result = apiInstance.SendTransacEmail(sendSmtpEmail);
                
            }
            catch (Exception)
            {
                throw new CustomException(500, "Our Email Service is not working just approve this request later");
            }



            req.LastUpdate = DateTime.Now;

            await _dbCon.SaveChangesAsync();

            return Unit.Value;


        }
    }

}


