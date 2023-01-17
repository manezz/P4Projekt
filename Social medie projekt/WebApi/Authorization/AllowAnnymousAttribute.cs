namespace WebApi.Authorization
{
    public class AllowAnnymousAttribute
    {
        [AttributeUsage(AttributeTargets.Method)]
        public class AllowAnnymousAttributeAttribute : Attribute { }
    }
}
