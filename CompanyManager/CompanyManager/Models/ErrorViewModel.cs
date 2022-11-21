namespace CompanyManager.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        // Errores personalizados
        public const string InsufficientStork = "No hay stock suficiente.";
        public const string MinCharacters = "{0} no debe ser inferior a los {1} caracteres.";
        public const string MaxCharacters = "{0} no debe superar los {1} caracteres.";
        public const string PorcentRange = "{0} debe ser un valor entre 0 y 100.";
        public const string StockErrorRange = "{0} debe ser mayor a {1}.";
        public const string InvalidEmail = "El email ya está registrado.";
        public const string InvalidLogin = "Email o contraseña invalida.";
        public const string RequiredField = "{0} es obligatorio.";
    }
}