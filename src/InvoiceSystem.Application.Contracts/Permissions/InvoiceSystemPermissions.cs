namespace InvoiceSystem.Permissions;

public static class InvoiceSystemPermissions
{
    public const string GroupName = "InvoiceSystem";


    



    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";
    public static class Invoices
    {
        public const string Default = GroupName + ".Invoice";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
        public const string RestoreDelete = Default + ".RestoreDelete";

    }

}
