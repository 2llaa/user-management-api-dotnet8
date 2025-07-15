using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using user_management_api_dotnet8.Authentication;

namespace user_management_api_dotnet8.OptionsSetUp
{
    public class JwtOptionsSetUp : IConfigureOptions<JwtOptions>
    {
        private const string SectionName = "Jwt";
        private readonly IConfiguration _configure;

        public JwtOptionsSetUp(IConfiguration configure)
        {
            _configure = configure;
        }
        public void Configure(JwtOptions options)
        {
            _configure.GetSection(SectionName).Bind(options);
        }
    }
}
