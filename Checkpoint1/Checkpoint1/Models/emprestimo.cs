using System;

public class Emprestimo
{
    public Emprestimo()
    {
        Usuario = new Usuario(); // Inicializa Usuario no construtor
        LivroEmprestado = new Livro("titulo", "autor", "genero", "isbn");
    }

    public Livro LivroEmprestado { get; set; }
    public Usuario Usuario { get; set; }
    public DateTime DataEmprestimo { get; set; }
    public DateTime DataDevolucao { get; set; }
}
