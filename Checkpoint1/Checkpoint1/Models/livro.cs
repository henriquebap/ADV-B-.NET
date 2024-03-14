using System;

public class Livro
{
    public Livro(string titulo, string autor, string genero, string isbn)
    {
        Titulo = titulo ?? throw new ArgumentNullException(nameof(titulo));
        Autor = autor ?? throw new ArgumentNullException(nameof(autor));
        Genero = genero ?? throw new ArgumentNullException(nameof(genero));
        ISBN = isbn ?? throw new ArgumentNullException(nameof(isbn));
        Identificador = isbn ?? throw new ArgumentNullException(nameof(isbn));
    }

    public string Titulo { get; set; }
    public string Autor { get; set; }
    public string Genero { get; set; }
    public string ISBN { get; set; }
    public string Identificador { get; set; }

    // Método ToString sobrescrito
    public override string ToString()
    {
        return $"{Titulo} - {Autor} ({Genero})";
    }
}
