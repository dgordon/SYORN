namespace SYORN.Models
{
    /// <remarks>
    /// Since the Systems.Drawing.Imaging.PropertyItem class is sealed.
    /// This class is used to as a DTO to eliminate Mocking limitations with sealed classes
    /// </remarks>
    public class PropertyItemEncoded
    {
        public PropertyItemEncoded(int id , short type, int length, byte[] value)
        {
            Id = id;
            Type = type;
            Length = length;
            Value = value;
        }

        public virtual int Id { get; set; }
        public virtual short Type { get; set; }
        public virtual int Length { get; set; }
        public virtual byte[] Value { get; set; }
    }
}
