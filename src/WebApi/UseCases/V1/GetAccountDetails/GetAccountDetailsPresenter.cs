namespace WebApi.UseCases.V1.GetAccountDetails
{
    using Application.Boundaries.GetAccountDetails;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    ///
    /// </summary>
    public sealed class GetAccountDetailsPresenter : IOutputPort
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public IActionResult ViewModel { get; private set; } = new NoContentResult();

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        public void NotFound(string message)
        {
            this.ViewModel = new NotFoundObjectResult(message);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="getAccountDetailsOutput"></param>
        public void Standard(GetAccountDetailsOutput getAccountDetailsOutput)
        {
            var getAccountDetailsResponse = new GetAccountDetailsResponse(
                getAccountDetailsOutput.Account.Id,
                getAccountDetailsOutput.Account.GetCurrentBalance());

            this.ViewModel = new OkObjectResult(getAccountDetailsResponse);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        public void WriteError(string message)
        {
            this.ViewModel = new BadRequestObjectResult(message);
        }
    }
}
