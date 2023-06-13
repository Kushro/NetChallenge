namespace NetChallenge.Domain.Utils
{
    public static class ExceptionMessageGeneratorUtil
    {
        public static string PropertyCannotBeNullOrEmpty(string className, string propertyName)
        {
            return $"The {propertyName} of a {className} cannot be null or empty.";
        }
        
        public static string PropertyCannotBeLessThanOrEqualToZero(string className, string propertyName)
        {
            return $"The {propertyName} of a {className} cannot be less than or equal to zero.";
        }
        
        public static string PropertyCannotBeGreaterThan(string className, string propertyName, int value)
        {
            return $"The {propertyName} of a {className} cannot be greater than {value}.";
        }
    }
}