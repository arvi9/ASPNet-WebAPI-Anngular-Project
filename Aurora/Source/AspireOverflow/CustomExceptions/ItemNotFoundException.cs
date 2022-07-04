namespace AspireOverflow.CustomExceptions
{
    [Serializable]
    public class ItemNotFoundException : Exception
    {

        protected ItemNotFoundException() : base("Item not found") { }

        public ItemNotFoundException(string message) : base(message) { }

        public ItemNotFoundException(string message, Exception innerException) : base(message, innerException) { }

    }
}