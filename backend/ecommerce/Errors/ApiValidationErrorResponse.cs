namespace ecommerce.Errors
{
    public class ApiValidationErrorResponse :ApiException
    {
        public ApiValidationErrorResponse():base(400)
        {

        }

        public IEnumerable<String> Errors { get; set; }
    }
}
