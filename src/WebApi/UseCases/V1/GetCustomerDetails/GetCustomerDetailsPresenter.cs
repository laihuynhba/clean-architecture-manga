namespace WebApi.UseCases.V1.GetCustomerDetails
{
    using Application.Boundaries.GetCustomerDetails;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels;
    using Customer = Domain.Customers.Customer;

    public sealed class GetCustomerDetailsPresenter : IOutputPort
    {
        public IActionResult ViewModel { get; private set; } = new NoContentResult();

        public void NotFound(string message)
        {
            this.ViewModel = new NotFoundObjectResult(message);
        }

        public void Standard(GetCustomerDetailsOutput getCustomerDetailsOutput)
        {
            var customerModel = new CustomerModel((Customer) getCustomerDetailsOutput.Customer);
            var getCustomerDetailsResponse = new GetCustomerDetailsResponse(customerModel);
            this.ViewModel = new OkObjectResult(getCustomerDetailsResponse);
        }

        public void WriteError(string message)
        {
            this.ViewModel = new BadRequestObjectResult(message);
        }
    }
}
