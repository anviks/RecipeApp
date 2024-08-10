using Base.Resources;

namespace App.BLL.Exceptions;

public class MissingImageException() : Exception(ValidationErrors.MissingImageFile);