using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace SharedModule.Utils
{
    public static class Extension
    {
        public static JwtUserDetails GetJwtClaimUserDetails(ClaimsPrincipal claim)
        {
            JwtUserDetails jwtUserDetails = null;
            try
            {
                string claimDetails = claim.Claims.First(c => c.Type == "UserDetails").Value;
                if (claimDetails ==null || claimDetails.Length == 0)
                {
                    throw new UnauthorizedAccessException("Unauthorise Access");
                }
                jwtUserDetails = JsonConvert.DeserializeObject<JwtUserDetails>(claimDetails);


            }
            catch (Exception)
            {

                throw;
            }
            return jwtUserDetails;
        }
    }
}
