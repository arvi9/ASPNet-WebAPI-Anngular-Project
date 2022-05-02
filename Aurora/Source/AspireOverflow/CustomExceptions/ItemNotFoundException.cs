namespace AspireOverflow.CustomExceptions
{
    public class ItemNotFoundException : Exception
    {

        public ItemNotFoundException() : base("Item not found") { }

        public ItemNotFoundException(string message) : base(message) { }

        public ItemNotFoundException(string message, Exception innerException) : base(message, innerException) { }

    }
}