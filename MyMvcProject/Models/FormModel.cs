namespace MyMvcProject.Models
{
    public class FormModel
    {
        // Example fields for the form.
        public string Name { get; set; }
        public string Email { get; set; }

        // Holds the Base64-encoded signature image.
        public string SignatureData { get; set; }
    }
}
