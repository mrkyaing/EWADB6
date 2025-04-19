using Microsoft.AspNetCore.Authorization;

namespace CloudHRMS.WebAPIs.ConfigSwaggerOptions {
    public class DynamicRoleAuthorizeAttribute : AuthorizeAttribute {
        public DynamicRoleAuthorizeAttribute(string roleKey) {
            Policy = $"RolePolicy:{roleKey}";
        }
    }
}
