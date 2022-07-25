using TestCache.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace TestCache.Permissions
{
    public class TestCachePermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(TestCachePermissions.GroupName);

            //Define your own permissions here. Example:
            //myGroup.AddPermission(TestCachePermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<TestCacheResource>(name);
        }
    }
}
