using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Loan_API.Application.Common.Helpers
{
    public class HeadParseUserId
    {

        public int GetUserIdFromHeadTokenDecoder(string authorization)
        {

            int userId = 0;
            if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
            {
                var scheme = headerValue.Scheme;
                var parameter = headerValue.Parameter;
                var stream = parameter;
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadToken(stream) as JwtSecurityToken;
                userId = int.Parse(token.Claims.FirstOrDefault(x => x.Type == "unique_name").Value);
            }
            return userId;

        }
    }
}
